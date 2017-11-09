using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Controllers
{
    public class CorrespondenceController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult Index() {
            return View();
        }

        public ActionResult Income()
        {
            return View();
        }

        public ActionResult Outgoing()
        {
            return View();
        }


        public ActionResult New(Guid? id) {
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult Detail(Guid? id) {
            return View(id ?? Guid.NewGuid());
        }

        [HttpPost]
        public async Task<JsonResult> GetList(ModelRequest request,int type) {
            return Json(await CorrespondenceServices.Instance.GetOutgoing(db, request,type), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> GetCurrentList(ModelRequest request,Guid id) {
            return Json(await CorrespondenceServices.Instance.GetCurrentList(db, request, id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(Document model) {
            var user = UserHelper.GetCurrentEmployee();


            if (db.Documents.Any(m => m.Id == model.Id)) {
                var document = db.Documents.FirstOrDefault(m => m.Id == model.Id);
                document.Summary = model.Summary;
                document.AnswersId = model.AnswersId;
                document.DocumentDate = DateTime.Now;
                document.ApplicantType = model.ApplicantType;
                db.SaveChanges();
                return Json(document.Id, JsonRequestBehavior.AllowGet);
            }
            else {
                Document document = new Document() {
                    Id = model.Id,
                    DocumentType = 0,
                    ProjectType = 3,
                    AnswersId = model.AnswersId,
                    StateType = 0,
                    Summary = model.Summary,
                    ExecutionDate = DateTime.Now.AddDays(20),
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    CorrespondentsInfo = UserHelper.GetCurrentEmployee().DisplayName,
                    CorrespondentsId = user.Id.ToString(),
                    CorrespondentsValue = user.DisplayName,
                    CreatedUserId = user.Id.ToString(),
                    CreatedUserValue = user.DisplayName,
                    ApplicantType = model.ApplicantType,
                DocumentDate = DateTime.Now
                };
                db.Documents.Add(document);
                db.SaveChanges();
                return Json(document.Id, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Load(Guid ? id) {
            var data = db.Documents.FirstOrDefault(m => m.Id == id);
            if (id != null && data!=null) {
                return
                    Json(
                        new {data.Id, data.Summary, data.CreatedDate, ApplicantType = data.ApplicantType==0 ? DictionaryHelper.GetType(db,Guid.Parse(data.AnswersId)) : data.ApplicantType, data.StateType,data.AnswersId},
                        JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(
                      new { Id = id,StateType = 0 },
                      JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Send(Document model) {
            Create(model);
            var doc = db.Documents.First(m => m.Id == model.Id);
            doc.StateType = 1;
            db.SaveChanges();
            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id) {
            Document d = db.Documents.Find(id);
            if (d.StateType == 0 && d.DocumentType == 0 && d.ProjectType == 3) {
                db.Documents.Remove(d);
                db.SaveChanges();
            }
            return Json("ok", JsonRequestBehavior.AllowGet);

        }
    }
}