using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace AISWPF
{
	public class XMLSer<T>
	{
		public static void toXML(T obj, string fileName) {
			XmlSerializer mySerializer = new XmlSerializer(typeof(T));
			// To write to a file, create a StreamWriter object.
			StreamWriter myWriter = new StreamWriter(fileName);
			mySerializer.Serialize(myWriter, obj);
			myWriter.Close();
		}

		public static T fromXML(string fileName) {
			try {
				XmlSerializer mySerializer = new XmlSerializer(typeof(T));
				// To read the file, create a FileStream.
				FileStream myFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				// Call the Deserialize method and cast to the object type.
				T data = (T)mySerializer.Deserialize(myFileStream);
				myFileStream.Close();
				return data;
			} catch (Exception e) {
				return default(T);
			}
		}
	}

	public class Settings
	{
		public String Server { get; set; }
		public String User { get; set; }
		public string Password { get; set; }
		public string DBName { get; set; }
		public string SMTPServer { get; set; }
		public string SMTPUser { get; set; }
		public string SMTPFrom { get; set; }
		public string SMTPTo { get; set; }
		public string SMTPDomain { get; set; }
		public string SMTPPassword { get; set; }
		public int SMTPPort { get; set; }
		public List<string> Sources { get; set; }

		public static Settings Single { get; protected set; }
		public static void init(string filename) {
			try {
				Settings single = XMLSer<Settings>.fromXML(filename);
				Single = single;

			} catch (Exception e) {
				Logger.error("Ошибка при чтении файла настроек " + e, Logger.LoggerSource.server);
			}
		}
	}

	public class MailClass
	{
		public static void sendMail(string message,string header) {
			try {
				string body = message;


				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(Settings.Single.SMTPFrom);
				mess.Subject = header;
				mess.Body = body;

				string[] addr = Settings.Single.SMTPTo.Split(new char[] { ';' });
				foreach (string add in addr) {
					try {
						mess.To.Add(add);
					} catch { }
				}

				mess.SubjectEncoding = System.Text.Encoding.UTF8;
				mess.BodyEncoding = System.Text.Encoding.UTF8;
				mess.IsBodyHtml = true;
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Settings.Single.SMTPServer, Settings.Single.SMTPPort);
				client.EnableSsl = true;
				if (string.IsNullOrEmpty(Settings.Single.SMTPUser)) {
					client.UseDefaultCredentials = true;
				} else {
					client.Credentials = new System.Net.NetworkCredential(Settings.Single.SMTPUser, Settings.Single.SMTPPassword, Settings.Single.SMTPDomain);
				}
				// Отправляем письмо
				client.Send(mess);

			} catch (Exception e) {
				Logger.info(String.Format("Ошибка при отправке почты: {0}", e.ToString()), Logger.LoggerSource.server);
			}
		}
	}

	public static class Logger
	{
		public enum LoggerSource { server, client, ordersContext, objectsContext, usersContext, service }
		public static log4net.ILog logger;
		public static TextBox rbText;
		static Logger() {

		}
		public static void init(string path, string name) {
			string fileName = String.Format("{0}/{1}_{2}.txt", path, name, DateTime.Now.ToShortDateString().Replace(":", "_").Replace("/", "_").Replace(".", "_"));
			PatternLayout layout = new PatternLayout(@"[%d] %-10p %m%n");
			FileAppender appender = new FileAppender();
			appender.Layout = layout;
			appender.File = fileName;
			appender.AppendToFile = true;
			BasicConfigurator.Configure(appender);
			appender.ActivateOptions();
			logger = LogManager.GetLogger(name);
		}

		public static string createMessage(string message, LoggerSource source) {
			string msg = String.Format("{1}", source.ToString(), message);
			try {
				rbText.Text = msg + "\r\n" + rbText.Text;
				rbText.InvalidateArrange();
			} catch { }
			return msg;
		}

		public static void info(string str, LoggerSource source = LoggerSource.server) {
			logger.Info(createMessage(str, source));
		}

		public static void error(string str, LoggerSource source = LoggerSource.server) {
			logger.Error(createMessage(str, source));
		}

		public static void debug(string str, LoggerSource source = LoggerSource.server) {
			logger.Debug(createMessage(str, source));
		}

	}
}
