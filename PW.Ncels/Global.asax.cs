using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ncels.Helpers;
using PW.Ncels.Resources;


namespace PW.Ncels
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Stream stream = new MemoryStream(StrToByteArray(Resource.Licence));
            stream.Position = 0;
            Aspose.Pdf.License licensePdf = new Aspose.Pdf.License();
            Aspose.Words.License licenseWords = new Aspose.Words.License();

            licensePdf.SetLicense(stream);
            stream.Position = 0;
            licenseWords.SetLicense(stream);
            stream.Close();

            LogHelper.InitLogger();
            LogHelper.Log.Info("Application Start");
        }
        public static byte[] StrToByteArray(string str)
        {
            return Convert.FromBase64String(str);
        }
    }
}
