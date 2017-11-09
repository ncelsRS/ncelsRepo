using System.Web;
using System.Web.Mvc;

namespace PW.Ncels {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogInfoAttribute());
        }
    }
}
