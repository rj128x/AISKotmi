using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxScdSys;

namespace KotmiData
{

	public delegate void OnFinishReadDelegate(Dictionary<DateTime, double> Data);
	public class KotmiClass
	{
		public AxScadaCli Client { get; protected set; }
		public AxScadaAbo Abo { get; set; }
		public Dictionary<DateTime, double> Data;
		public event OnFinishReadDelegate OnFinishRead;
		
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
			Abo.OnRowValue += Abo_OnRowValue1;
			Abo.OnBlockBegin += Abo_OnBlockBegin;
			Abo.OnBlockEnd += Abo_OnBlockEnd;
		}

		private void Abo_OnBlockEnd(object sender, IScadaAboEvents_OnBlockEndEvent e) {
			if (OnFinishRead != null)
				OnFinishRead(Data);
		}

		private void Abo_OnBlockBegin(object sender, EventArgs e) {
			Data = new Dictionary<DateTime, double>();
		}

		private void Abo_OnRowValue1(object sender, IScadaAboEvents_OnRowValueEvent e) {
			Object dt = e.recData.FieldValue["DT"];
			Object dt2=Client.get_TimeSecToOle(Convert.ToInt32(dt));
			DateTime date = Convert.ToDateTime(dt2);
			double val= Convert.ToDouble(e.recData.FieldValue["VAL"]);
			if (!Data.ContainsKey(date)) {
				Data.Add(date, val);
			}
		}

		protected bool _Connect() {
			Client.SrvAddress = "192.168.141.251";
			Client.UserName = "EVA";
			Client.UserPassword = "q1234e";
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

		public void ReadVals(DateTime dateStart, DateTime dateEnd,List<DateTime>sentData, int TI,int stepSeconds) {			
			if (Connect()) {
				Data = new Dictionary<DateTime, double>();
				
				Abo.BlockBegin();				
				DateTime date = dateStart.AddHours(0);
				while (date <= dateEnd) {
					Abo.RequestPrmSet("PROC", "READ_ARCH");
					Abo.RequestPrmSet("TABLE_NAME", "T_ARCH_TI");
					Abo.Proc();

					Abo.FieldValue("ID", ScdSys.EFieldType.eftInt, TI);
					int time = Client.get_TimeOleToSec(date);
					Abo.FieldValue("DT", ScdSys.EFieldType.eftUnixDT, time);
					sentData.Add(date);
					date = date.AddSeconds(stepSeconds);
					Abo.Post();										
				}
				
				Abo.BlockEnd(true, true);
			}
		}

		public Dictionary<DateTime,double> getFullData(Dictionary<DateTime,double> data,List<DateTime> sentData) {
			Dictionary<DateTime, double> resDT = new Dictionary<DateTime, double>();
			foreach (DateTime date in sentData) {
				try {
					//resDT.Add(date, 0);
					DateTime dt1 = data.Keys.Last(d => d <= date);
					resDT.Add(date, data[dt1]);
				}catch (Exception e) {
					Logger.info("Ошибка при получении данных КОТМИ " + e.ToString());
				}
			}
			return resDT;
		}

		public static void Close() {
			try {
				Single.Client.Close();
			} catch  {

			}
		}

	}
}
