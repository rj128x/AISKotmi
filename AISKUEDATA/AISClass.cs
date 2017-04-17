using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISKUEDATA
{
	public class SourceInfo
	{
		public int Obj { get; set; }
		public int ObjType { get; set; }
		public int Parnumber { get; set; }
		public int Item { get; set; }
		
		public SourceInfo() {

		}

		public SourceInfo(string info) {
			string[] arr = info.Split('~');
			Obj = Convert.ToInt32(arr[0]);
			ObjType = Convert.ToInt32(arr[1]);
			Parnumber = Convert.ToInt32(arr[2]);
		}

		public static SourceInfo getFullInfo(string info) {
			SourceInfo res = new SourceInfo();
			string[] arr = info.Split('-');
			res.Obj = Convert.ToInt32(arr[0]);
			res.ObjType = Convert.ToInt32(arr[1]);
			res.Item = Convert.ToInt32(arr[2]);
			res.Parnumber = Convert.ToInt32(arr[3]);
			return res;
		}
	}

	public class AISClass {
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

		public static SqlConnection getConnection() {
			String str= String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Trusted_Connection=False;",
				Settings.Single.Server, Settings.Single.DBName, Settings.Single.User, Settings.Single.Password);
			return new SqlConnection(str);
		}

		public SourceInfo Source { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public Dictionary <string,string> Names { get; set; }
		
		public AISClass(String source, DateTime dateStart, DateTime dateEnd) {
			Source = new SourceInfo(source);
			DateStart = dateStart;
			DateEnd = dateEnd;
		}

		public void ReadNames() {
			Names = new Dictionary<string, string>();
			try {
				SqlConnection con = getConnection();
				con.Open();
				string comSTR = "";
				if (Source.ObjType == 0) {
					int stationID = Source.Obj == 8737 ? 2 : Source.Obj == 8738 ? 1 : Source.Obj == 8738 ? 3 : Source.Obj == 8740 ? 4 : -1;
					comSTR = String.Format("SELECT NAME,CODE FROM SENSORS WHERE STATIONID={0}", stationID);					
				} else {
					comSTR = String.Format("SELECT NAME,CODE FROM CLIENTS");					
				}
				SqlCommand command = new SqlCommand(comSTR, con);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read()) {
					string name = reader.GetString(0);
					int code = reader.GetInt32(1);
					string codeStr = String.Format("{0}-{1}-{2}-{3}", Source.Obj, Source.ObjType, code, Source.Parnumber);
					Names.Add(codeStr, name);
				}
				reader.Close();
				con.Close();
			} catch (Exception e) {
				Logger.info("Ошибка при чтении наименований источника "+e.ToString());
			}
		}


	}
}
