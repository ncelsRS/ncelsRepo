using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Antlr.Runtime;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class OBKCertificateReferenceController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        // GET: /Reference/
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }


        public ActionResult AllList([DataSourceRequest] DataSourceRequest request, string status)
        {
            var list = db.OBK_CertificateReference.ToList();
            foreach (var obj in list)
            {
                if (obj.EndDate < DateTime.Now)
                {
                    OBK_CertificateReference d = db.OBK_CertificateReference.First(o => o.Id == obj.Id);
                    d.OBK_CertificateValidityType = db.OBK_CertificateValidityType.First(o => o.Code == "Passive");
                    db.SaveChanges();
                }

            }

            var data = from certRef in db.OBK_CertificateReference
                       join dic in db.Dictionaries on certRef.CertificateCountryId equals dic.Id
                       join certType in db.OBK_Ref_CertificateType on certRef.CertificateTypeId equals certType.Id
                       join validityType in db.OBK_CertificateValidityType on certRef.CertificateValidityTypeId equals validityType.Id
                       select new
                       {
                           Id = certRef.Id,
                           Number = certRef.Number,
                           CertificateNumber = certRef.CertificateNumber,
                           StartDate = certRef.StartDate,
                           EndDate = certRef.EndDate,
                           country = dic.NameKz,
                           CertificateOrganization = certRef.CertificateOrganization,
                           CertificateType = certType.NameRu,
                           CertificateTypeId = certRef.CertificateTypeId,
                           CertificateCountryId = certRef.CertificateCountryId,
                           CertificateCountry = dic.Name,
                           LastInspection = certRef.LastInspection,
                           DocumentId = certRef.DocumentId,
                           CertificateValidityType = validityType.Name,
                           CertificateValidityCode = validityType.Code
                       };

            if (status.Equals("Active"))
            {
                return Json(data.ToList().Where(d => d.CertificateValidityCode == "Active").ToDataSourceResult(request));
            }
            else if (status.Equals("Passive"))
            {
                return Json(data.ToList().Where(d => d.CertificateValidityCode == "Passive").ToDataSourceResult(request));
            }
            else if (status.Equals("Recalled"))
            {
                return Json(data.ToList().Where(d => d.CertificateValidityCode == "Recalled").ToDataSourceResult(request));
            }

            return Json(data.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, OBKCertificateReferenceModel dictionary,
            string type)
        {

            if (dictionary != null && ModelState.IsValid)
            {

                OBK_CertificateReference d = new OBK_CertificateReference()
                {
                    Id = Guid.NewGuid(),
                    Number = dictionary.Number,
                    CertificateNumber = dictionary.CertificateNumber,
                    StartDate = dictionary.StartDate,
                    EndDate = dictionary.EndDate,
                    CertificateCountryId = dictionary.CertificateCountryId,
                    CertificateOrganization = dictionary.CertificateOrganization,
                    CertificateTypeId = dictionary.CertificateTypeId,
                    LastInspection = dictionary.LastInspection,
                    DocumentId = dictionary.DocumentId,
                };
                if (dictionary.EndDate >= DateTime.Now)
                {
                    OBK_CertificateValidityType t = db.OBK_CertificateValidityType.First(o => o.Code == "active");
                    d.OBK_CertificateValidityType = t;
                }
                else
                {
                    OBK_CertificateValidityType t = db.OBK_CertificateValidityType.First(o => o.Code == "passive");
                    d.OBK_CertificateValidityType = t;
                }

                db.OBK_CertificateReference.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, OBKCertificateReferenceModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                OBK_CertificateReference d = db.OBK_CertificateReference.First(o => o.Id == dictionary.Id);
                d.Number = dictionary.Number;
                d.CertificateNumber = dictionary.CertificateNumber;
                d.StartDate = dictionary.StartDate;
                d.EndDate = dictionary.EndDate;
                d.CertificateCountryId = dictionary.CertificateCountryId;
                d.CertificateOrganization = dictionary.CertificateOrganization;
                d.CertificateTypeId = dictionary.CertificateTypeId;
                d.LastInspection = dictionary.LastInspection;
                d.DocumentId = dictionary.DocumentId;
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, OBKCertificateReferenceModel dictionary,
            string type)
        {
            if (dictionary != null)
            {
                OBK_CertificateReference d = db.OBK_CertificateReference.First(o => o.Id == dictionary.Id);
                Document doc = db.Documents.First(o => o.Id == d.DocumentId);

                db.OBK_CertificateReference.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CountryList()
        {
            var data = db.Dictionaries.Where(c => c.Type == "Country").Select(o => new { Id = o.Id, Name = o.Name }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TypeList()
        {
            var data = db.OBK_Ref_CertificateType.Select(o => new { Id = o.Id, Name = o.NameRu }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentRead(Guid id)
        {
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                document = new Document()
                {
                    Id = id,
                    DocumentType = 1,
                    DocumentDate = DateTime.Now,
                    OutgoingType = 0,
                    AttachPath = FileHelper.GetObjectPathRoot()

                };
            }

            DocumentModel model = new DocumentModel(document);
            return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

        }

        [HttpPost]
        public ActionResult DocumentUpdate(DocumentModel document)
        {
            if (document != null)
            {

                Document baseDocument = db.Documents.Find(document.Id);
                if (baseDocument == null)
                {
                    baseDocument = new Document()
                    {
                        Id = document.Id,
                        DocumentType = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        DocumentDate = DateTime.Now,
                    };
                    db.Documents.Add(baseDocument);
                }
                baseDocument = document.GetDocument(baseDocument);

                db.SaveChanges();
                return Content(bool.TrueString);
            }

            return Content(bool.FalseString);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Recall([DataSourceRequest] DataSourceRequest request, Guid? Id)
        {
            OBK_CertificateReference obj = db.OBK_CertificateReference.FirstOrDefault(o => o.Id == Id);
            if (obj != null)
            {
                obj.OBK_CertificateValidityType = db.OBK_CertificateValidityType.First(o => o.Code == "Recalled");
                db.SaveChanges();
                return Json(new
                {
                    success = true
                });
            }

            return Json(new
            {
                success = false
            });

        }
    }
}
