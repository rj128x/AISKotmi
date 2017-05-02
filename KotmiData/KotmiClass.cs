using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxScdSys;

namespace KotmiData
{
	public class ArcField
	{
		public int ID { get; set; }
		public bool PTI { get; set; }
		public string Code { get; set; }

		public ArcField(string name) {
			string[] arr = name.Split('_');
			ID = Int32.Parse(arr[1]);
			PTI = arr[0] == "PTI";
			Code = name;
		}
	}


	public delegate void OnFinishReadDelegate(SortedList<DateTime, double> Data);
	public class KotmiClass
	{
		public AxScadaCli Client { get; protected set; }
		public AxScadaAbo Abo { get; set; }
		public SortedList<DateTime, double> Data;
		public event OnFinishReadDelegate OnFinishRead;
		protected bool AllSent = false;
		protected bool Break = false;
		
		public static KotmiClass Single { get; protected set; }

		protected KotmiClass() { }

		public static void init(AxScadaCli client, AxScadaAbo abo) {
			Single = new KotmiClass();
			Single.Client = client;
			Single.Abo = abo;
			abo.CliHWnd = client.hWnd;
			Single.init();
		}

		protected void init() {
			Abo.OnRowValue += Abo_OnRowValue;
			Abo.OnBlockEnd += Abo_OnBlockEnd;
		}

		private void Abo_OnBlockEnd(object sender, IScadaAboEvents_OnBlockEndEvent e) {
			Logger.info("Завершение блока");
			if ((Break||AllSent) && OnFinishRead != null) {
				Logger.info("Завершение запроса");
				OnFinishRead(Data);
			}
		}

		private void Abo_OnRowValue(object sender, IScadaAboEvents_OnRowValueEvent e) {
			Object dt = e.recData.FieldValue["DT"];
			Object dt2=Client.get_TimeSecToOle(Convert.ToInt32(dt));
			DateTime date = Convert.ToDateTime(dt2);
			double val= Convert.ToDouble(e.recData.FieldValue["VAL"]);
			if (!Data.ContainsKey(date)) {
				Data.Add(date, val);
			}
		}

		protected bool _Connect() {
			Client.SrvAddress = Settings.Single.Server;
			Client.UserName = Settings.Single.User;
			Client.UserPassword = Settings.Single.Password;
			Client.Open();
			return Client.CliActive;
		}

		public static bool Connect() {
			if (!Single.Client.CliActive)
				return Single._Connect();
			return ISConnected();
		}

		public static bool ISConnected() {
			return Single.Client.CliActive;
		}

		public void ReadVals(DateTime dateStart, DateTime dateEnd,List<DateTime>sentData, ArcField field,int stepSeconds) {			
			if (Connect()) {
				Break = false;
				Data = new SortedList<DateTime, double>();

				bool needStart = true;
				AllSent = false;

				int cnt = 0;
				DateTime date = dateStart.AddHours(0);
				DateTime dateS = date.AddHours(0);

				while (date <= dateEnd && !Break&&!AllSent) {

					if (needStart) {
						dateS = date.AddHours(0);
						Logger.info(String.Format("Старт блока дата [{0}]",date));
						Abo.BlockBegin();
						needStart = false;
					}
					cnt++;
					Abo.RequestPrmSet("PROC", "READ_ARCH");
					Abo.RequestPrmSet("TABLE_NAME", field.PTI? "T_ARCH_PTI":"T_ARCH_TI");
					Abo.Proc();

					Abo.FieldValue("ID", ScdSys.EFieldType.eftInt, field.ID);
					int time = Client.get_TimeOleToSec(date);
					Abo.FieldValue("DT", ScdSys.EFieldType.eftUnixDT, time);
					sentData.Add(date);
					date = date.AddSeconds(stepSeconds);
					Abo.Post();				
					
					if (Break||cnt == 1000 ||date>dateEnd) {
						AllSent = date >= dateEnd;
						cnt = 0;
						needStart = true;
						Logger.info(String.Format("Отправка запроса по измерению {0} [{1}]-[{2}]", field.Code, dateS,date));
						Abo.BlockEnd(true, true);
						
					}
				}
				
				
			}
			
		}

		public Dictionary<DateTime,double> getFullData(SortedList<DateTime,double> data,List<DateTime> sentData) {
			//Logger.info(String.Format("Обработка входного массива дат: \r\n Отправлены: {0} \r\n Получены: {1}", String.Join(" , ", sentData), String.Join(" , ", data.Keys)));
			Dictionary<DateTime, double> resDT = new Dictionary<DateTime, double>();
			List<DateTime> missedDates = new List<DateTime>();
			foreach (DateTime date in sentData) {
				try {
					//resDT.Add(date, 0);
					DateTime dt1 = data.Keys.Last(d => d <= date);
					resDT.Add(date, data[dt1]);
				}catch (Exception e) {
					//Logger.info(String.Format("Ошибка при обработке данных КОТМИ нет даты {0}: {1} " ,date,e.ToString()));
					missedDates.Add(date);
					resDT.Add(date, double.NaN);
				}
			}
			Logger.info("Пропущены даты: " + string.Join(", ", missedDates));
			return resDT;
		}

		public static void Close() {
			try {
				Single.Client.Close();
			} catch  {

			}
		}

		public static void BreakRead() {
			Logger.info("Прерывание запроса");
			Single.Break = true;
		}

	}
}
