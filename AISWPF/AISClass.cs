using AISWPF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISWPF
{
	public class SourceInfo
	{
		public int Obj { get; set; }
		public int ObjType { get; set; }
		public int Item { get; set; }


		public SourceInfo() {

		}

		public SourceInfo(string info) {
			string[] arr = info.Split('_');
			Obj = Convert.ToInt32(arr[0]);
			ObjType = Convert.ToInt32(arr[1]);
		}

		public static SourceInfo getFullInfo(string info) {
			SourceInfo res = new SourceInfo();
			string[] arr = info.Split('_');
			res.Obj = Convert.ToInt32(arr[0]);
			res.ObjType = Convert.ToInt32(arr[1]);
			res.Item = Convert.ToInt32(arr[2]);
			return res;
		}

	}

	public class DataRecord
	{
		public string Name { get; set; }
		public Dictionary<DateTime, string> Values { get; set; }
		public SourceInfo Source { get; set; }
		public string Key { get; set; }
	}

	public class AISClass
	{
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";
		public int CurrentSeason = -1;

		public static SqlConnection getConnection() {
			String str = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Trusted_Connection=False;",
				Settings.Single.Server, Settings.Single.DBName, Settings.Single.User, Settings.Single.Password);
			return new SqlConnection(str);
		}

		public SourceInfo Source { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public Dictionary<string, string> Names { get; set; }
		public Dictionary<string, DataRecord> Data { get; set; }
		public List<DateTime> Dates { get; set; }

		public AISClass(String source, DateTime dateStart) {
			Source = new SourceInfo(source);

			DateStart = dateStart.Date;
			DateEnd = DateStart.AddHours(24);
			Dates = new List<DateTime>();
			DateTime date = DateStart.AddMinutes(30);
			while (date <= DateEnd) {
				Dates.Add(date);
				date = date.AddMinutes(30);
			}
		}

		public void ReadNames() {
			Logger.info(String.Format("Чтение наименований за {0} по объекту [{1}-{2}]", DateStart.ToString("dd.MM.yyyy"), Source.Obj, Source.ObjType));
			Names = new Dictionary<string, string>();
			Data = new Dictionary<string, DataRecord>();
			/*for (int i = 0; i <= 50; i++) {
				string name = "Parametr" + i.ToString();
				string codeStr = String.Format("{0}_{1}_{2}", Source.Obj, Source.ObjType, i);
				Names.Add(codeStr, name);

				DataRecord rec = new DataRecord();
				rec.Key = codeStr;
				rec.Name = name;
				rec.Source = SourceInfo.getFullInfo(codeStr);
				rec.Values = new Dictionary<DateTime, string>();
				foreach (DateTime date in Dates) {
					rec.Values.Add(date, "---");
				}
				Data.Add(codeStr, rec);
			}
			return;*/
			try {
				SqlConnection con = getConnection();
				con.Open();
				string comSTR = "";
				if (Source.ObjType == 0) {
					int stationID = Source.Obj == 8737 ? 2 : Source.Obj == 8738 ? 1 : Source.Obj == 8739 ? 3 : Source.Obj == 8740 ? 4 : -1;
					comSTR = String.Format("SELECT NAME,CODE FROM SENSORS WHERE STATIONID={0} order by code", stationID);
				} else {
					comSTR = String.Format("SELECT NAME,CODE FROM CLIENTS order by code");
				}
				SqlCommand command = new SqlCommand(comSTR, con);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()) {
					string name = reader.GetString(0);
					int code = reader.GetInt32(1);
					name = String.Format("{0}: {1}", code, name);
					string codeStr = String.Format("{0}_{1}_{2}", Source.Obj, Source.ObjType, code);
					Names.Add(codeStr, name);

					DataRecord rec = new DataRecord();
					rec.Key = codeStr;
					rec.Name = name;
					rec.Source = SourceInfo.getFullInfo(codeStr);
					rec.Values = new Dictionary<DateTime, string>();
					foreach (DateTime date in Dates) {
						rec.Values.Add(date, "---");
					}
					Data.Add(codeStr, rec);

				}
				reader.Close();
				con.Close();
			} catch (Exception e) {
				Logger.info("Ошибка при чтении наименований источника " + e.ToString());
			}
			
		}

		public void ReadData() {
			Logger.info(String.Format("Чтение данных за {0} по объекту [{1}-{2}]", DateStart.ToString("dd.MM.yyyy"), Source.Obj, Source.ObjType));
			foreach (KeyValuePair<string, string> de in Names) {
				try {
					SqlConnection con = getConnection();
					con.Open();
					string comSTR = "";
					comSTR = String.Format("SELECT DATA_DATE,VALUE0, SEASON FROM DATA WHERE OBJECT={0} AND OBJTYPE={1} AND ITEM={2} AND PARNUMBER=12 AND DATA_DATE>'{3}' AND DATA_DATE<='{4}'",
						Data[de.Key].Source.Obj, Data[de.Key].Source.ObjType, Data[de.Key].Source.Item, DateStart.ToString(DateFormat), DateEnd.ToString(DateFormat));
					SqlCommand command = new SqlCommand(comSTR, con);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read()) {
						//Object dt=
						DateTime date = reader.GetDateTime(0);
						double val = reader.GetDouble(1);
						int season = reader.GetInt32(2);
						if (season >= CurrentSeason)
							CurrentSeason = season;
						try {
							Data[de.Key].Values[date] = String.Format("{0:0.##}", val).Replace(",", ".");
						} catch { }
					}
					reader.Close();
					con.Close();
				} catch (Exception e) {
					Logger.info("Ошибка при данных" + e.ToString());
				}
			}
		}



		public bool changeVal(string key, int hh, int min, string newVal, ref string changedVal, ref string message) {
			Logger.info(String.Format("Изменение значения по объекту [{0}] [{1}:{2}]:", key, hh, min));
			DateTime date = DateStart.AddMinutes(min).AddHours(hh);
			DataRecord rec = Data[key];
			string oldVal = rec.Values[date];


			Logger.info(String.Format("==={0}: Дата: {1} OldVal={2} newVal={3}", rec.Name, date.ToString("dd.MM.yyyy HH:mm"), oldVal, newVal));

			bool result = true;
			/*changedVal = newVal;
			message = String.Format("Изменено значение [{0}] [{1}] ====== OldVal={2} ======  newVal={3} ",
					date.ToString("dd.MM.yyyy HH:mm"), rec.Name, oldVal, newVal);
			return result;*/

			SqlConnection con = getConnection();
			try {
				con.Open();
				SqlTransaction trans = con.BeginTransaction();
				Logger.info(String.Format("======Удаление значения"));
				string comSTR = "";
				comSTR = String.Format("DELETE FROM DATA WHERE OBJECT={0} AND OBJTYPE={1} AND ITEM={2} AND PARNUMBER=12 AND DATA_DATE='{3}'",
					rec.Source.Obj, rec.Source.ObjType, rec.Source.Item, date.ToString(DateFormat));
				Logger.info(comSTR);
				SqlCommand command = new SqlCommand(comSTR, con);
				command.Transaction = trans;
				command.ExecuteNonQuery();
				Logger.info(String.Format("=========OK"));

				if (newVal != "---") {
					Logger.info(String.Format("======Вставка значения"));
					comSTR = String.Format("INSERT INTO Data (parnumber, object, objtype, item, value0, value1, data_date, rcvstamp, season, appid, p2kstatus, p2kstatush) SELECT	12, {0}, {1}, {2}, {3}, {3}, '{4}', '{4}', {5}, 2, 0, 0",
						rec.Source.Obj, rec.Source.ObjType, rec.Source.Item, newVal.Replace(",", "."), date.ToString(DateFormat), CurrentSeason);
					Logger.info(comSTR);
					command = new SqlCommand(comSTR, con);
					command.Transaction = trans;
					command.ExecuteNonQuery();
					Logger.info(String.Format("=========OK"));
				}
				Logger.info(String.Format("======Применение транзакции"));
				trans.Commit();
				Logger.info(String.Format("=========OK"));
				message = String.Format("Изменено значение [{0}] [{1}] ====== OldVal={2} ======  newVal={3} ",
					date.ToString("dd.MM.yyyy HH:mm"), rec.Name, oldVal, newVal);
			} catch (Exception e) {
				Logger.info(String.Format("===Ошибка в транзакции, значение не изменено "+e.ToString()));
				con.Close();
				result = false;
				changedVal = rec.Values[date];
			}

			try {
				Logger.info(String.Format("======Чтение значения"));
				if (con.State!=System.Data.ConnectionState.Open)
					con.Open();
				string comSTR = "";
				comSTR = String.Format("SELECT VALUE0 FROM DATA WHERE OBJECT={0} AND OBJTYPE={1} AND ITEM={2} AND PARNUMBER=12 AND DATA_DATE='{3}'",
					rec.Source.Obj, rec.Source.ObjType, rec.Source.Item, date.ToString(DateFormat));
				SqlCommand command = new SqlCommand(comSTR, con);
				object nv = command.ExecuteScalar();
				if (nv == null) {
					changedVal = "---";
					rec.Values[date] = "---";
					Logger.info(String.Format("========значение не найдено: {0}", changedVal));
				} else {
					double val = Convert.ToDouble(nv);
					rec.Values[date] = String.Format("{0:0.##}", val).Replace(",", ".");
					changedVal = rec.Values[date];
					Logger.info(String.Format("========получено значение: {0}", changedVal));
				}
				con.Close();
			} catch (Exception e) {
				Logger.info("===Ошибка при чтении нового значения " + e.ToString());
			}
			try {
				if (con.State == System.Data.ConnectionState.Open)
					con.Close();
			} catch { }

			return result;


		}


	}
}
