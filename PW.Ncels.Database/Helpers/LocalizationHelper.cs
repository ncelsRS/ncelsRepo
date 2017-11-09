using System.Threading;

namespace PW.Ncels.Database.Helpers {
	public class LocalizationHelper {


		public static string GetString(string nameRu, string nameKz) {

			if (Thread.CurrentThread.CurrentUICulture.Name == "ru-RU") {
				return nameRu;
			}
			return nameKz;
		}

	}
}