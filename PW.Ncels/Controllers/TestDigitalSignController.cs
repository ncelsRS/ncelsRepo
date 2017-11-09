using PW.Ncels.Database.Helpers;
using PW.Ncels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Ncels.Controllers
{
    public class TestDigitalSignController : Controller
    {
        // GET: TestDigitalSign
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignOperation(string id)
        {
            bool IsSuccess = true;
            string preambleXml = string.Empty;

            MyClass obj = new MyClass()
            {
                Name = "Name",
                Id = "Id",
                CustomData = "CustomData"
            };

            try
            {
                preambleXml = SerializeHelper.SerializeDataContract(obj);
                preambleXml = preambleXml.Replace("utf-16", "utf-8");
            }
            catch (Exception e)
            {
                IsSuccess = false;
            }

            return Json(new
            {
                IsSuccess,
                preambleXml
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignForm(string preambleId, string xmlAuditForm)
        {
            var success = true;
            var message = "";

            if (xmlAuditForm != null)
            {
                int certificateState = DigitalSignHelper.GetCertificateState(xmlAuditForm);
                if (certificateState != 0)
                {
                    success = false;
                    message = DigitalSignHelper.GetMessage(certificateState); ;
                }
            }
            else
            {

            }

            return Json(new
            {
                success,
                message
            }, JsonRequestBehavior.AllowGet);
        }
    }

    public class MyClass
    {
        public string Name;
        public string Id;
        public string CustomData;
    }
}