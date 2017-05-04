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
			read();
		}

		public static void read() {
			Logger.init(Directory.GetCurrentDirectory().ToString() + "/logs", "log");
			Logger.info("Старт приложения");
			Settings.init(Directory.GetCurrentDirectory().ToString() + "/Data/MainSettings.xml");
			KotmiClass.init();
			KotmiClass.Single.OnFinishRead += Single_OnFinishRead;
			ArcField field = new ArcField("TI_997");
			KotmiClass.Single.ReadVals(DateTime.Now.AddHours(-1), DateTime.Now, field, 1);
		}

		private static void Single_OnFinishRead(SortedList<DateTime, double> Data) {
			Console.WriteLine(Data.Count.ToString());
			Console.ReadLine();
		}
	}
}
