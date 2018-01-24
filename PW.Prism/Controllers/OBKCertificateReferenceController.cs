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
        private NcelsEntities db = UserHelper.GetCn();
        private OBKCertificateReferenceFieldSaver fieldSaver = new Ncels.Database.Helpers.OBKCertificateReferenceFieldSaver();
        // GET: /Reference/
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }


        public ActionResult AllList([DataSourceRequest] DataSourceRequest request, string status)
        {
            var list = db.OBK_CertificateReference.Include(o => o.OBK_CertificateValidityType).ToList();
            foreach (var obj in list)
            {
                if (obj.EndDate < DateTime.Now && !"Recalled".Equals(obj.OBK_CertificateValidityType.Code))
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
                           CertificateProducer = certRef.CertificateProducer,
                           CertificateType = certType.NameRu,
                           CertificateTypeId = certRef.CertificateTypeId,
                           CertificateCountryId = certRef.CertificateCountryId,
                           CertificateCountry = dic.Name,
                           LastInspection = certRef.LastInspection,
                           AttachPath = certRef.AttachPath,
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
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, OBKCertificateReferenceModel dictionary)
        {

            if (dictionary != null && ModelState.IsValid)
            {

                OBK_CertificateReference d = new OBK_CertificateReference()
                {
                    Id = (Guid)dictionary.Id,
                    Number = dictionary.Number,
                    CertificateNumber = dictionary.CertificateNumber,
                    StartDate = dictionary.StartDate,
                    EndDate = dictionary.EndDate,
                    CertificateCountryId = dictionary.CertificateCountryId,
                    CertificateOrganization = dictionary.CertificateOrganization,
                    CertificateProducer = dictionary.CertificateProducer,
                    CertificateTypeId = dictionary.CertificateTypeId,
                    LastInspection = dictionary.LastInspection,
                    AttachPath = dictionary.AttachPath,
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

                fieldSaver.FieldSave(new OBK_CertificateReference(), d, UserHelper.GetCurrentEmployee().Id);

                List<UploadInitialFile> physicalfiles = UploadHelper.GetFilesInfo(dictionary.AttachPath.ToString(), false);

                List<FileLink> oldList = db.FileLinks.Where(o => o.DocumentId == dictionary.Id).ToList();

                foreach(var file in physicalfiles)
                {
                    if (!oldList.Any( o => o.FileName == file.name))
                    {
                        FileLink dbFile = new FileLink
                        {
                            Id = Guid.NewGuid(),
                            FileName = file.name,
                            CreateDate = DateTime.Now,
                            DocumentId = dictionary.Id,
                            OwnerId = UserHelper.GetCurrentEmployee().Id,
                            IsDeleted = false,
                        };
                        db.FileLinks.Add(dbFile);
                        fieldSaver.LogChange("FileLinks", dbFile.FileName, UserHelper.GetCurrentEmployee().Id,
                            dictionary.Id, "добавлен");
                    }
                }
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, OBKCertificateReferenceModel dictionary)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                OBK_CertificateReference d = db.OBK_CertificateReference.First(o => o.Id == dictionary.Id);

                if (!d.OBK_CertificateValidityType.Code.Equals("Recalled"))
                {
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
                }

                var oldCopy = fieldSaver.CopyObject(d);

                d.Number = dictionary.Number;
                d.CertificateNumber = dictionary.CertificateNumber;
                d.StartDate = dictionary.StartDate;
                d.EndDate = dictionary.EndDate;
                d.CertificateCountryId = dictionary.CertificateCountryId;
                d.CertificateOrganization = dictionary.CertificateOrganization;
                d.CertificateProducer = dictionary.CertificateProducer;
                d.CertificateTypeId = dictionary.CertificateTypeId;
                d.LastInspection = dictionary.LastInspection;
                d.AttachPath = dictionary.AttachPath;
                db.SaveChanges();

                fieldSaver.FieldSave(oldCopy, d, UserHelper.GetCurrentEmployee().Id);
            }

            List<UploadInitialFile> physicalfiles = UploadHelper.GetFilesInfo(dictionary.AttachPath.ToString(), false);

            List<FileLink> oldList = db.FileLinks.Where(o => o.DocumentId == dictionary.Id).ToList();

            foreach (var file in physicalfiles)
            {
                if (!oldList.Any(o => o.FileName == file.name))
                {
                    FileLink dbFile = new FileLink
                    {
                        Id = Guid.NewGuid(),
                        FileName = file.name,
                        CreateDate = DateTime.Now,
                        DocumentId = dictionary.Id,
                        OwnerId = UserHelper.GetCurrentEmployee().Id,
                        IsDeleted = false,
                    };
                    db.FileLinks.Add(dbFile);
                    fieldSaver.LogChange("FileLinks", dbFile.FileName, UserHelper.GetCurrentEmployee().Id,
                        dictionary.Id, "добавлен");
                }
            }
            db.SaveChanges();

            List<FileLink> newList = db.FileLinks.Where(o => o.DocumentId == dictionary.Id).ToList();

            foreach (var file in newList)
            {
                if (!physicalfiles.Any(o => o.name == file.FileName))
                {
                    db.FileLinks.Remove(file);
                    fieldSaver.LogChange("FileLinks", file.FileName, UserHelper.GetCurrentEmployee().Id,
                        dictionary.Id, "удален");
}
            }
            db.SaveChanges();

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, OBKCertificateReferenceModel dictionary,
            string type)
        {
            if (dictionary != null)
            {
                OBK_CertificateReference d = db.OBK_CertificateReference.First(o => o.Id == dictionary.Id);
                //Document doc = db.Documents.First(o => o.Id == d.DocumentId);

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
            OBK_CertificateReference reference = db.OBK_CertificateReference.Find(id);
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel();

            if (reference != null)
            {
                fileModel.AttachPath = reference.AttachPath;
                fileModel.AttachFiles = UploadHelper.GetFilesInfo(fileModel.AttachPath.ToString(), false);
            }
            else
            {
                fileModel.AttachPath = FileHelper.GetObjectPathRoot();
                fileModel.AttachFiles = UploadHelper.GetFilesInfo(fileModel.AttachPath.ToString(), false);
            }

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Recall([DataSourceRequest] DataSourceRequest request, Guid? Id)
        {
            OBK_CertificateReference obj = db.OBK_CertificateReference.FirstOrDefault(o => o.Id == Id);
            var oldCopy = fieldSaver.CopyObject(obj);
            if (obj != null)
            {
                obj.OBK_CertificateValidityType = db.OBK_CertificateValidityType.First(o => o.Code == "Recalled");
                db.SaveChanges();

                fieldSaver.FieldSave(oldCopy,obj,UserHelper.GetCurrentEmployee().Id);

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
