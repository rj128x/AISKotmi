using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AISKUEDATA
{
	public partial class Form1 : Form
	{
		public Dictionary<string, string> Sources;
		
		public Form1() {
			InitializeComponent();
			Logger.init(Directory.GetCurrentDirectory().ToString() + "/logs", "log");
			Logger.info("Старт приложения");
			Settings.init(Application.StartupPath + "/Data/MainSettings.xml");

			Sources = new Dictionary<string, string>();
			foreach (string field in Settings.Single.Sources) {
				string[] data = field.Split('=');
				string val = data[0];
				string name = data[1];
				Sources.Add(val, String.Format("[{0}] [{1}]", val, name));
			}
			lbSource.DataSource = new BindingSource(Sources, null);
			lbSource.DisplayMember = "Value";
			lbSource.ValueMember = "Key";
		}

		private void button1_Click(object sender, EventArgs e) {
			
		}
	}
}
