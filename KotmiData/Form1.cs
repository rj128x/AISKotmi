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
		protected int currentTI = 0;
		protected int currentStep = 0;
		public Form1() {
			InitializeComponent();
			Logger.init(Directory.GetCurrentDirectory().ToString()+"/logs", "log");
			Logger.info("Старт приложения");
			Settings.init(Application.StartupPath+"/Data/MainSettings.xml");
			
			KotmiClass.init(axScadaCli1, axScadaAbo1);
			KotmiClass.Single.OnFinishRead += Single_OnFinishRead;
			DTPEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:00"));
			DTPStart.Value = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy 00:00"));
			KotmiFields = new Dictionary<int, string>();
			foreach (string field in Settings.Single.KotmiFields) {
				string[] data = field.Split('=');
				int val = Convert.ToInt32(data[0]);
				string name = data[1];
				KotmiFields.Add(val, String.Format("[{0}] [{1}]",val,name));
			}
			lbItems.DataSource = new BindingSource(KotmiFields,null);
			lbItems.DisplayMember = "Value";
			lbItems.ValueMember = "Key";
			
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
				getPage(DataStr, true);
			}

			if (chbHH.Checked) {
				getPage(DataStr, false);
			}
		}

		protected TabPage getPage(Dictionary<string, string> data,bool step) {
			string name = "";
			if (KotmiFields.ContainsKey(currentTI)) {
				name = KotmiFields[currentTI];
			} else {
				name = currentTI.ToString();
			}
			DataGridView grid = new DataGridView();
			TabPage page = new TabPage((!step?"HH_":"") + name);
			tcKotmiData.TabPages.Add(page);
			grid.DataSource = (from entry in data
												 orderby entry.Key
												 select new { entry.Key, entry.Value }).ToList();
			grid.Parent = page;
			grid.Dock = DockStyle.Fill;
						
			return page;
		}
		

		private void button1_Click(object sender, EventArgs e) {
			bool ok=KotmiClass.Connect();					
			if (ok) {
				sentData = new List<DateTime>();
				int TI = Convert.ToInt32(txtTI.Text);
				if (TI == 0) {
					try {
						KeyValuePair<int, string> sel = (KeyValuePair<int, string>)lbItems.SelectedItem;
						TI = sel.Key;
					}catch (Exception ex) {
						TI = 0;
					}
				}
				currentStep = Convert.ToInt32(txtStep.Text);
				currentTI = TI;
				KotmiClass.Single.ReadVals(Convert.ToDateTime(DTPStart.Value.ToString("dd.MM.yyyy HH:mm")), Convert.ToDateTime(DTPEnd.Value.ToString("dd.MM.yyyy HH:mm")), 
					sentData,currentTI,currentStep);
			} else {
				MessageBox.Show("Не удалось подключиться");
			}
		}

		private void tcKotmiData_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				try {
					tcKotmiData.TabPages.Remove(tcKotmiData.SelectedTab);
				} catch { }
			}
		}


	}
}
