using log4net;
using log4net.Config;

namespace Ncels.Helpers{
	public class LogHelper{

		private static readonly ILog log = LogManager.GetLogger(typeof(LogHelper));

		public static ILog Log{
			get { return log; }
		}

		public static void InitLogger(){
			XmlConfigurator.Configure();
		}

	}
}
