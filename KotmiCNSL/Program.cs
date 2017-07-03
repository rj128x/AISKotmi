using KotmiLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KotmiCNSL
{	
	class Program
	{
		[STAThread]
		static void Main(string[] args) {
			Logger.init(Directory.GetCurrentDirectory().ToString() + "/logs", "log");
			Logger.info("Старт приложения");
			Settings.init(Directory.GetCurrentDirectory().ToString() + "/Data/MainSettings.xml");
			KotmiClass.init();

			if (args.Count() > 0) {
				string com = args[0];
				switch (com) {
					case "avrchm": 
						int dayStart = Int32.Parse(args[1]);
						int dayEnd = Int32.Parse(args[2]);
						Logger.info("Получение АВРЧМ ");
						readAVRCHM(DateTime.Now.Date.AddDays(-dayStart), DateTime.Now.Date.AddDays(-dayEnd));
						break;
				}
			}
		}

		public static void readAVRCHM(DateTime dateStart,DateTime dateEnd) {
			Logger.info(String.Format("{0} {1}", dateStart, dateEnd));

			DateTime date = dateStart.Date;
			while (date <= dateEnd) {
				AVRCHMReport report = new AVRCHMReport(date, date.AddDays(1), 1);
				report.ReadData(2);
				report.WriteToDB();
				AVRCHMResult result = report.Result;
				for (int ga = 1; ga <= 10; ga++) {
					Logger.info(String.Format("GA {0}: WORK: {1:0.00} STOP: {2:0.00} AVRCHM: {3:0.00} ", ga, result.TimeWork[ga] / 60, result.TimeStop[ga] / 60, result.TimeAVRCHM[ga] / 60));
				}
				date = date.AddDays(1);
			}
			Console.ReadLine();
		}

		private static void Single_OnFinishRead(SortedList<DateTime, double> Data) {

		}
	}
}
