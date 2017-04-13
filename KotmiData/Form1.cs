using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KotmiData
{
	public partial class Form1 : Form
	{
		protected List<DateTime> sentData;
		protected Dictionary<int, string> KotmiFields;
		public Form1() {
			InitializeComponent();
			Logger.init(Directory.GetCurrentDirectory().ToString()+"/logs", "log");
			Logger.info("Старт приложения");
			Settings.init("/Data/MainSettings.xml");
			
			KotmiClass.init(axScadaCli1, axScadaAbo1);
			KotmiClass.Single.OnFinishRead += Single_OnFinishRead;
			DTPEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:00"));
			DTPStart.Value = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy 00:00"));
			KotmiFields = new Dictionary<int, string>();
			foreach (string field in Settings.Single.KotmiFields) {
				string[] data = field.Split('=');
				int val = Convert.ToInt32(data[0]);
				string name = data[1];
				KotmiFields.Add(val, name);
			}
			lbItems.DataSource = KotmiFields;
			
		}

		private void Single_OnFinishRead(Dictionary<DateTime, double> Data) {			
			Dictionary<string, string> DataStr = new Dictionary<string, string>();
			Dictionary<string, string> HHDataStr = new Dictionary<string, string>();
			Dictionary<DateTime, double> FullData = KotmiClass.Single.getFullData(Data, sentData);
			double sum = 0;
			double sumHH = 0;
			double cntHH = 0;
			foreach (DateTime date in FullData.Keys) {
				double val = FullData[date];
				DataStr.Add(date.ToString("dd.MM.yyyy HH:mm:ss"), String.Format("{0:0.000}",val));
				sum += val;
				sumHH += val;
				cntHH += 1;

				if (date.Second==0 && (date.Minute==0 || date.Minute == 30)) {
					HHDataStr.Add(date.ToString("dd.MM.yyyy HH:mm:ss"), String.Format("{0:0.000}", sumHH / cntHH));
					sumHH = 0;
					cntHH = 0;
				}
			}
			DataStr.Add("AVG", String.Format("{0:0.000}", sum / FullData.Count));
			DateTime ds = FullData.Keys.First();
			DateTime de = FullData.Keys.Last();
			TimeSpan ts = de - ds;

			double vyr = sum / FullData.Count * ts.TotalSeconds / 60 / 60;
			DataStr.Add("Vyr", String.Format("{0:0.000}", vyr));


			DataGridView grid;
			TabPage page;

			if (chbStep.Checked) {
				grid= new DataGridView();
				page = new TabPage((tcKotmiData.TabCount + 1).ToString());
				tcKotmiData.TabPages.Add(page);
				grid.DataSource = (from entry in DataStr
													 orderby entry.Key
													 select new { entry.Key, entry.Value }).ToList();
				grid.Parent = page;
				grid.Dock = DockStyle.Fill;
				/*grid.Columns[0].Width = 150;
				grid.Columns[1].Width = 100;*/
			}

			if (chbHH.Checked) {
				grid = new DataGridView();
				page = new TabPage((tcKotmiData.TabCount + 1).ToString());
				tcKotmiData.TabPages.Add(page);
				grid.DataSource = (from entry in HHDataStr
													 orderby entry.Key
													 select new { entry.Key, entry.Value }).ToList();
				grid.Parent = page;
				grid.Dock = DockStyle.Fill;
				/*grid.Columns[0].Width = 150;
				grid.Columns[1].Width = 100;*/
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			bool ok=KotmiClass.Connect();					
			if (ok) {
				sentData = new List<DateTime>();
				KotmiClass.Single.ReadVals(Convert.ToDateTime(DTPStart.Value.ToString("dd.MM.yyyy HH:mm")), Convert.ToDateTime(DTPEnd.Value.ToString("dd.MM.yyyy HH:mm")), 
					sentData,Convert.ToInt32(txtTI.Text),Convert.ToInt32(txtStep.Text));
			} else {
				MessageBox.Show("Не удалось подключиться");
			}
		}


	}
}
