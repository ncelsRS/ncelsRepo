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
using PW.Prism.ViewModels.OBK;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Notifications;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class ZBKCopyController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        private ZBKCopyRepository repository = new ZBKCopyRepository();

        // GET: /Reference/
        public ActionResult Index(int type)
        {
            var model = new OBKEntity
            {
                Guid = Guid.NewGuid(),
                DicStageId = type
            };
            return PartialView(model);
        }

        /// <summary>
        /// назначить исполнителя
        /// </summary>
        /// <param name="id">список заявлении</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetExecuter()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult Products(Guid contractId)
        {
            return Json(new { success = true, result = repository.Products(contractId) });
        }

        [HttpPost]
        public ActionResult SetExecuter(Guid[] stages, Guid[] executors)
        {
            repository.SendToWork(stages, executors);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendNote(Guid zbkCopyId, string notes)
        {
            var zbkCopy = db.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);
            zbkCopy.Notes = notes;
            zbkCopy.StatusId = CodeConstManager.STATUS_OBK_REJECT_ID;

            var zbkCopyStage = db.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == zbkCopyId && o.StageId == 1);
            var stageStatus = db.OBK_Ref_StageStatus.FirstOrDefault(o => OBK_Ref_StageStatus.OnCorrection.Equals(o.Code));
            zbkCopyStage.StageStatusId = stageStatus.Id;

            db.SaveChanges();

            var employee = db.Employees.FirstOrDefault(o => o.Id == zbkCopy.EmployeeId);

            NotificationManager notification = new NotificationManager();

            var text = "Уведомляем Вас о том, что в запросе ЗБК были найдены ошибки.";
            notification.SendNotificationFromCompany(text, ObjectType.OBK_ZBKCopy, zbkCopy.Id.ToString(), employee.Id);

            return Json(new { success = true });
        }

        public ActionResult DocumentRead(string AttachPath)
        {
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel();

            fileModel.AttachPath = AttachPath;
            fileModel.AttachFiles = UploadHelper.GetFilesInfo(AttachPath, false);

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = repository.GetZBKViewModel(id);

            return PartialView(model);
        }

        public ActionResult ListZBKCopies([DataSourceRequest] DataSourceRequest request, int type, int stage)
        {
            var data = repository.ListZBKCopies(type, stage);

            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult AllList([DataSourceRequest] DataSourceRequest request)
        {
            var data =
                db.OBK_Ref_PriceList.OrderBy(o => o.NameRu)
                .Include(o => o.OBK_Ref_Type)
                .Include(o => o.OBK_Ref_ServiceType)
                .Include(o => o.OBK_Ref_DegreeRisk)
                .Include(o => o.Dictionary)
                .Select(o => new
                {
                    Id = o.Id,
                    NameRu = o.NameRu,
                    NameKz = o.NameKz,
                    Type = o.OBK_Ref_Type.NameRu,
                    TypeId = o.TypeId,
                    Unit = o.Dictionary.Name,
                    UnitId = o.UnitId,
                    Price = o.Price,
                    ServiceType = o.OBK_Ref_ServiceType.NameRu,
                    ServiceTypeId = o.ServiceTypeId,
                    Degree = o.OBK_Ref_DegreeRisk.NameRu,
                    DegreeRiskId = o.DegreeRiskId
                });
            return Json(data.ToDataSourceResult(request));
        }

    }
}
