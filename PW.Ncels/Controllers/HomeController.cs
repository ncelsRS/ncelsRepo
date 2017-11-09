using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Ncels.Controllers {
    [Authorize()]
    public class HomeController : ACommonController
    {
		public ActionResult Index() {

		    return View();
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