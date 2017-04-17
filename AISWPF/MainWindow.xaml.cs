using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AISWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		TextBlock CurrentBlock;
		TextBox CurrentTB;
		Border CurrentBorder;



		public List<string> WorkingLog { get; set; }

		public Dictionary<string, string> Sources;
		AISClass AIS;
		public Dictionary<string, TextBlock> TextBlocks;

		public MainWindow() {
			InitializeComponent();
			Logger.init(Directory.GetCurrentDirectory().ToString() + "/logs", "log");
			Logger.rbText = Log;
			Logger.info("Старт приложения");

			//string folder = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location);
			Settings.init(Directory.GetCurrentDirectory().ToString() + "/Data/MainSettings.xml");

			Sources = new Dictionary<string, string>();
			foreach (string field in Settings.Single.Sources) {
				string[] data = field.Split('=');
				string val = data[0];
				string name = data[1];
				Sources.Add(val, String.Format("[{0}] [{1}]", val, name));
			}
			lbSources.ItemsSource = Sources;
			lbSources.DisplayMemberPath = "Value";
			lbSources.SelectedValuePath = "Key";
			WorkingLog = new List<string>();
			selDate.SelectedDate = DateTime.Now.Date;

		}

		protected Border getText(string text, string name, int width, Brush brush) {

			Border brd = new Border();
			brd.Width = width;
			brd.Height = 25;
			brd.Background = brush;
			brd.BorderThickness = new Thickness(1);
			brd.BorderBrush = Brushes.Gray;

			TextBlock blk = new TextBlock(); blk.Text = text; blk.TextAlignment = TextAlignment.Center;
			blk.FontFamily = new FontFamily("Courier");
			blk.HorizontalAlignment = HorizontalAlignment.Center;
			blk.VerticalAlignment = VerticalAlignment.Center;
			if (!string.IsNullOrEmpty(name)) {
				blk.Name = name;
				blk.MouseRightButtonUp += Blk_MouseRightButtonUp;
			}

			blk.HorizontalAlignment = HorizontalAlignment.Center;
			brd.Child = blk;
			return brd;
		}

		private void Blk_MouseRightButtonUp(object sender, MouseButtonEventArgs e) {
			try {
				CurrentBorder.Child = CurrentBlock;
			} catch { }

			TextBlock blk = e.Source as TextBlock;
			CurrentBlock = blk;
			CurrentBorder = (blk.Parent as Border);
			CurrentBorder.Focus();

			Button btn = new Button();
			btn.Width = 15;
			btn.Content = "V";
			btn.Click += Btn_Click;

			StackPanel pnl = new StackPanel();

			pnl.Orientation = Orientation.Horizontal;
			TextBox txt = new TextBox();
			txt.Text = blk.Text;
			txt.Width = CurrentBorder.Width - 18;
			txt.Height = 20;
			txt.VerticalAlignment = VerticalAlignment.Center;
			txt.FontSize = 10;
			txt.GotFocus += Txt_GotFocus;
			CurrentTB = txt;
			pnl.Children.Add(txt);
			pnl.Children.Add(btn);

			CurrentBorder.Child = pnl;
			txt.Focus();
		}

		private void Txt_GotFocus(object sender, RoutedEventArgs e) {
			scrNames.ScrollToVerticalOffset(scrRes.VerticalOffset);
			scrDates.ScrollToHorizontalOffset(scrRes.HorizontalOffset);
		}

		private void Btn_Click(object sender, RoutedEventArgs e) {
			try {
				CurrentBorder.Child = CurrentBlock;
			} catch { }

			Button btn = e.Source as Button;
			TextBox box = CurrentTB;
			string name = CurrentBlock.Name;
			string[] args = name.Split('L');
			string key = args[1];
			string[] tm = args[2].Split('_');
			int hh = Convert.ToInt32(tm[0]);
			int min = Convert.ToInt32(tm[1]);
			if (hh == 00 && min == 0)
				hh = 24;
			string newVal = "???";
			string message = "";
			bool ok = AIS.changeVal(key, hh, min, box.Text, ref newVal, ref message);

			if (ok) {
				CurrentBlock.Foreground = Brushes.Green;
				WorkingLog.Add(message);
				lbWorkingLog.Items.Add(message);
				CurrentBlock.Text = newVal;
			} else {
				CurrentBlock.Foreground = Brushes.Red;
				CurrentBlock.Text = newVal;
			}
		}

		private void btnShow_Click(object sender, RoutedEventArgs e) {
			if (lbSources.SelectedIndex >= 0 && selDate.SelectedDate.HasValue) {
				TextBlocks = new Dictionary<string, TextBlock>();

				AIS = new AISClass(lbSources.SelectedValue as string, selDate.SelectedDate.Value.Date);
				AIS.ReadNames();
				AIS.ReadData();

				pnlFull.Children.Clear();
				pnlNames.Children.Clear();
				pnlDates.Children.Clear();

				foreach (DateTime date in AIS.Dates) {
					pnlDates.Children.Add(getText(date.ToString("HH:mm"), "", 70, Brushes.LightSteelBlue));
				}

				int i = 0;
				foreach (String name in AIS.Names.Values) {
					i++;
					pnlNames.Children.Add(getText(name, "", 200, i % 2 == 0 ? Brushes.LightGray : Brushes.LightSkyBlue));
				}

				i = 0;
				foreach (DataRecord record in AIS.Data.Values) {
					i++;

					StackPanel pnlVals = new StackPanel();
					pnlVals.Orientation = Orientation.Horizontal;
					foreach (DateTime date in AIS.Dates) {
						string name = String.Format("txtL{0}L{1}", record.Key.Replace("-", "_"), date.ToString("HH_mm"));
						pnlVals.Children.Add(getText(record.Values[date], name, 70, i % 2 == 0 ? Brushes.LightGray : Brushes.LightSkyBlue));
					}
					pnlFull.Children.Add(pnlVals);
				}


			}
		}


		private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e) {
			scrNames.ScrollToVerticalOffset(e.VerticalOffset);
			scrDates.ScrollToHorizontalOffset(e.HorizontalOffset);
		}

		private void scrDates_ScrollChanged(object sender, ScrollChangedEventArgs e) {
			if (scrRes.HorizontalOffset > 0 && scrDates.HorizontalOffset == 0)
				scrDates.ScrollToHorizontalOffset(scrRes.HorizontalOffset);
		}

		private void scrNames_ScrollChanged(object sender, ScrollChangedEventArgs e) {
			if (scrRes.VerticalOffset > 0 && scrNames.VerticalOffset == 0)
				scrNames.ScrollToVerticalOffset(scrRes.VerticalOffset);
		}

		private void btnSendMail_Click(object sender, RoutedEventArgs e) {
			sendMail();
		}

		protected void sendMail() {
			if (WorkingLog.Count > 0) {
				MailClass.sendMail(String.Join("<br/>", WorkingLog), "Замена значение в БД");
			}
		}
	}
}
