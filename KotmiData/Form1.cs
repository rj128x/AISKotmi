using AxScdSys;
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
		protected Dictionary<string, string> KotmiFields;
		protected Dictionary<string, string> VisibleFields;
		protected ArcField currentField = null;
		protected int currentStep = 0;
		public Form1() {
			InitializeComponent();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

			Logger.init(Directory.GetCurrentDirectory().ToString()+"/logs", "log");
			Logger.TxtLog = txtLog;
			Logger.info("Старт приложения");
			Settings.init(Application.StartupPath+"/Data/MainSettings.xml");

			AxScadaCli cli = new AxScadaCli();
			AxScadaAbo abo = new AxScadaAbo();
			((System.ComponentModel.ISupportInitialize)(cli)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(abo)).BeginInit();
			cli.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cli.OcxState")));
			abo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("abo.OcxState")));
			this.Controls.Add(cli);
			this.Controls.Add(abo);
			((System.ComponentModel.ISupportInitialize)(cli)).EndInit();
			((System.ComponentModel.ISupportInitialize)(abo)).EndInit();

			KotmiClass.init(cli,abo);
			KotmiClass.Single.OnFinishRead += Single_OnFinishRead;
			DTPEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy HH:00"));
			DTPStart.Value = Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yyyy 00:00"));
			KotmiFields = new Dictionary<string, string>();

			VisibleFields = new Dictionary<string, string>();
			foreach (string field in Settings.Single.KotmiFields) {
				string[] data = field.Split('=');
				string[] codeArr = data[0].Split('_');
				string val = data[0];				
				string name = data[1];
				KotmiFields.Add(val, String.Format("{0,-8} {1}",val,name));
				VisibleFields.Add(val, String.Format("{0,-8} {1}", val, name));
			}
			lbItems.DataSource = new BindingSource(VisibleFields,null);
			lbItems.DisplayMember = "Value";
			lbItems.ValueMember = "Key";
			
		}

		

		private void Single_OnFinishRead(SortedList<DateTime, double> Data) {
			pnlLeft.Enabled = true;
			Dictionary<string, string> DataStr = new Dictionary<string, string>();
			Dictionary<string, string> HHDataStr = new Dictionary<string, string>();
			SortedList<DateTime, double> FullData = Data;
			double sum = 0;
			double sumHH = 0;
			double cntHH = 0;
			double sumHHPlus = 0;
			double sumHHMinus = 0;
			double cntHHPlus = 1;
			double cntHHMinus = 1;
			double min = double.MaxValue;
			double max = double.MinValue;
			foreach (DateTime date in FullData.Keys) {
				double val = FullData[date];
				DataStr.Add(date.ToString("dd.MM.yyyy HH:mm:ss"), String.Format("{0:0.000}",val));
				sum += val;
				sumHH += val;
				cntHH += 1;
				if (val > 0) {
					sumHHPlus += val;
					cntHHPlus++;
				} else {
					sumHHMinus += val;
					cntHHMinus++;
				}

				if (val < min)
					min = val;
				if (val > max)
					max = val;

				if (date.Second==0 && (date.Minute==0 || date.Minute == 30)) {
					string str = String.Format("{0,15:0.000}", sumHH / cntHH);
					if (chbNegPos.Checked)
						str += String.Format("{0,15:0.000}{1,15:0.000}",  sumHHPlus / cntHH, sumHHMinus / cntHH);
					if (chbMinMax.Checked)
						str += String.Format("{0,15:0.000}{1,15:0.000}", min, max);
					HHDataStr.Add(date.ToString("dd.MM.yyyy HH:mm:ss"), str);
					sumHH = 0;
					sumHHPlus = 0;
					sumHHMinus = 0;
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
				getPage(HHDataStr, false);
			}
			//KotmiClass.Close();
		}

		protected TabPage getPage(Dictionary<string, string> data,bool step) {
			string name = "";			
			if (KotmiFields.ContainsKey(currentField.Code)) {
				name = KotmiFields[currentField.Code];
			} else {
				name = currentField.Code;
			}
			DataGridView grid = new DataGridView();
			grid.Font = new Font("Courier new", 8);			

			TabPage page = new TabPage((!step?"HH_":"") + name);
			tcKotmiData.TabPages.Add(page);
			tcKotmiData.SelectedTab = page;
			grid.DataSource = (from entry in data
												 orderby entry.Key
												 select new { entry.Key, entry.Value }).ToList();
			grid.Parent = page;
			grid.Dock = DockStyle.Fill;

			grid.Columns[0].Width = 150;
			grid.Columns[1].Width = 400;
			if (!step) {
				string header= String.Format("{0,15}", "AVG"); 
				if (chbNegPos.Checked)
					header+=  String.Format("{0,15}{1,15}",  "PosAVG", "NegAVG");
				if (chbMinMax.Checked)
					header+= String.Format("{0,15}{1,15}", "Min", "Max");
				grid.Columns[1].HeaderText = header;
			}
			return page;
		}
		

		private void button1_Click(object sender, EventArgs e) {
			if ((chbHH.Checked||chbStep.Checked)&&lbItems.SelectedIndex>=0) {
				ArcField field = null;
				try {
					field = new ArcField(txtTI.Text);
				} catch { }

				if (field == null) {
					try {
						KeyValuePair<string, string> sel = (KeyValuePair<string, string>)lbItems.SelectedItem;
						field = new ArcField(sel.Key);
					}catch (Exception ex) {	}
				}
				currentStep = Convert.ToInt32(txtStep.Text);
				currentField = field;
				if (field == null)
					return;
				pnlLeft.Enabled = false;
				btnBreak.Enabled = true;
				KotmiClass.Single.ReadVals(Convert.ToDateTime(DTPStart.Value.ToString("dd.MM.yyyy HH:mm")), Convert.ToDateTime(DTPEnd.Value.ToString("dd.MM.yyyy HH:mm")), 
					currentField,currentStep);
			} 
		}

		private void tcKotmiData_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				try {
					tcKotmiData.TabPages.Remove(tcKotmiData.SelectedTab);
				} catch { }
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {
			VisibleFields.Clear();
			foreach (KeyValuePair<string,string> de in KotmiFields) {
				if (de.Value.ToLower().Contains(textBox1.Text.ToLower())){
					VisibleFields.Add(de.Key, de.Value);
				}
			}
			lbItems.DataSource = new BindingSource(VisibleFields, null);
			lbItems.DisplayMember = "Value";
			lbItems.ValueMember = "Key";
		}

		private void lbItems_SelectedIndexChanged(object sender, EventArgs e) {
			ArcField field = null;
			try {
				KeyValuePair<string, string> sel = (KeyValuePair<string, string>)lbItems.SelectedItem;
				field = new ArcField(sel.Key);
			} catch (Exception ex) {
			}
			txtTI.Text = field.Code;
		}

		private void btnBreak_Click(object sender, EventArgs e) {
			KotmiClass.BreakRead();
		}
	}
}
