using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplicationCulture.Helper;

namespace PW.Ncels.Controllers {
    [Authorize()]
    public class HomeController : ACommonController
    {
		public ActionResult Index() {

		    return View();
		}

        public ActionResult SetCulture(string culture)
        {
            if (culture.Equals("ҚАЗ"))
                culture = "kk-KZ";
            else
                culture = "ba";

            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult Version() {
            ViewBag.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return View();
        }

        public ActionResult ModalSing() {
			return PartialView();
		}
        public ActionResult SignView()
        {
            return PartialView();
        }

        public ActionResult OBK_SignView()
        {
            return PartialView();
        }

        public ActionResult InstructionRef()
        {
            return View();
        }

        public ActionResult Video(int id){
            FileInfo fi = new FileInfo(@"C:\Instructions\" + id + ".MP4");//Server.MapPath("/Content/video/OrfLs.wmv")
            FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
            BinaryReader rdr = new BinaryReader(fs);
            byte[] fileData = rdr.ReadBytes((int)fs.Length);
            rdr.Close();
            fs.Close();
            return File(fileData, "video/mp4");
        }

        public ActionResult GetInstruction(){
            FileInfo fi = new FileInfo(@"C:\Instructions\Instruction.pdf");//Server.MapPath("/Content/video/OrfLs.wmv")
            FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
            BinaryReader rdr = new BinaryReader(fs);
            byte[] fileData = rdr.ReadBytes((int)fs.Length);
            rdr.Close();
            fs.Close();
            return File(fileData, "application/pdf", "Инструкция РЦ.pdf");
        }


    }
}