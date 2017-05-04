using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KotmiLib
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
		public List<String> KotmiFields { get; set; }
		
		
		public static Settings Single { get; protected set; }
		public static void init(string filename) {
			try {
				Settings single = XMLSer<Settings>.fromXML( filename);
				Single = single;
				//single.KotmiFields = new List<string>();
				/*single.KotmiFields.Add("214235345");
				XMLSer<Settings>.toXML(single, "C:/test.xml");*/
			} catch (Exception e) {
				Logger.error("Ошибка при чтении файла настроек " + e, Logger.LoggerSource.server);
			}
		}
	}

	public static class Logger
	{
		public enum LoggerSource { server, client, ordersContext, objectsContext, usersContext, service }
		public static log4net.ILog logger;
		public static RichTextBox TxtLog { get; set; }
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
			string msg= String.Format("{0,-20} {1}", DateTime.Now.ToString(), message);
			try {
				TxtLog.Text = msg + "\r\n" + TxtLog.Text;
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
