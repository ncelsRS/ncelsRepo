using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Aspose.Words;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.Price;
using PW.Prism.ViewModels.PriceProject;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers.PriceProject
{
    public class ProtocolController : Controller{

        private ncelsEntities db = UserHelper.GetCn();

        // GET: Protocol
        public ActionResult Index(){
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult ListProtocol([DataSourceRequest] DataSourceRequest request) {
            var user = UserHelper.GetCurrentEmployee();
            var list = new PriceProjectRepository().GetProtocols(user.Id, null);
            var result = list.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProtocolForm(Guid? id) {
            var user = UserHelper.GetCurrentEmployee();
            var ppRepository = new PriceProjectRepository();

            PP_Protocols protocol;
            if (id.HasValue) {
                protocol = db.PP_Protocols.FirstOrDefault(x => x.Id == id.Value);
                if (protocol == null) {
                    LogHelper.Log.ErrorFormat("Не удалось найти протокол по идентификатору {0}", id.Value);
                    return null;
                }
                
            }
            else {
                protocol = new PP_Protocols{
                    ProtocolDate = DateTime.Now,
                    Type = (int)ProtocolType.Protocol1,
                    Status = (int)ProtocolStatus.Draft,
                    Number = ppRepository.GenerateProtocolNumber(),
                    IsImn = false
                };
            }

            var model = new ProtocolModel {
                Guid = Guid.NewGuid(),
                Protocol = protocol
            };

            if (protocol.Status == (int) ProtocolStatus.Draft) {
                ViewBag.Leadership = ppRepository.GetLeadership().ToList().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.FullName
                }).ToList();

                var list = new List<SelectListItem>();
                list.AddRange(EnumHelper.GetDisplayNameEnumList<ProtocolType>().ToList().Select(x => new SelectListItem
                {
                    Value = x.Key.ToString(),
                    Text = x.Value
                }));
                ViewBag.ProtocolTypes = list;

                ViewBag.Requesters = ppRepository.GetRequesters().ToList().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.DisplayName
                }).ToList();
                return PartialView(model);
            }
            else {
                ViewBag.HasEdit = model.Protocol.OwnerId == user.Id;

                var employee = ppRepository.GetEmployeeById(protocol.RequesterId.Value);
                ViewBag.RequesterDisplayName = employee != null ? employee.DisplayName : "";
                employee = ppRepository.GetEmployeeById(protocol.ChiefId.Value);
                ViewBag.ChiefDisplayName = employee != null ? employee.DisplayName : "";

                ViewBag.ComissionMembers = string.Join("<br/>", protocol.PP_ProtocolComissionMembers.Select(x => x.Employee.FullName).ToArray());
                
                return PartialView("ProtocolFormView", model);
            }

        }

        public ActionResult AllListComissionMembers(){
            var user = UserHelper.GetCurrentEmployee();
            var comissionMemebrs = new PriceProjectRepository().GetComissionMembers().Where(x=>x.Id != user.Id);
            return Json(comissionMemebrs.Select(o => new { o.Id, Name = o.FullName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBossFio(string id){
            var fio = new PriceProjectRepository().GetBossFio(new Guid(id));
            return Json(fio, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProtocolPriceDetails(string id, bool isImn){
            var prices = new PriceProjectRepository().GetPricesByRequester(new Guid(id), isImn);
            ViewBag.IsSelected = false;
            return PartialView("ProtocolPriceTable", prices.Select(x=> new PP_ProtocolProductPrices {
                PriceProjectId = x.PpId,
                PriceFirst = x.UnitPrice,
                ProductNameRu = x.ProductName
            }).ToList());
        }

        public ActionResult LoadProtocolPriceDetails(string id){
            var prices = db.PP_ProtocolProductPrices.Where(x => x.ProtocolId == new Guid(id));
            ViewBag.IsSelected = true;
            return PartialView("ProtocolPriceTable", prices.ToList());
        }

        [HttpPost]
        public ActionResult SendProtocol(PP_Protocols protocol){
            try {
                var user = UserHelper.GetCurrentEmployee();
                bool isNew = !(protocol.Id != Guid.Empty);
                //save new

                Guid protocolId = isNew ? Guid.NewGuid() : protocol.Id;

                if (isNew) {
                    protocol.Id = protocolId;
                }

                protocol.OwnerId = user.Id;
                protocol.ProtocolDate = DateTime.Now;
                protocol.CreatedDate = DateTime.Now;

                if (!isNew) {
                    db.PP_ProtocolComissionMembers.RemoveRange(db.PP_ProtocolComissionMembers.Where(x => x.ProtocolId == protocolId));
                }
                foreach (var member in protocol.PP_ProtocolComissionMembers) {
                    member.Id = Guid.NewGuid();
                    member.CreatedDate = DateTime.Now;
                    member.ProtocolId = protocolId;
                    db.PP_ProtocolComissionMembers.Add(member);
                }

                if (!isNew){
                    db.PP_ProtocolProductPrices.RemoveRange(db.PP_ProtocolProductPrices.Where(x => x.ProtocolId == protocolId));
                }
                foreach (var productPrice in protocol.PP_ProtocolProductPrices) {
                    productPrice.Id = Guid.NewGuid();
                    productPrice.CreatedDate = DateTime.Now;
                    productPrice.ProtocolId = protocolId;
                    db.PP_ProtocolProductPrices.Add(productPrice);
                }
                //db.Entry(protocol).State = EntityState.Modified;

                if (isNew) {
                    db.PP_Protocols.Add(protocol);
                }
                else {
                    db.Entry(protocol).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Не удалось сохранить протокол", ex);
                throw;
            }
        }

        public FileStreamResult GetOutputDocument(string id, string name) {
            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/PriceProject/Protocol.mrt"));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            report.Dictionary.Variables["protocolId"].ValueObject = new Guid(id);

            report.Render(false);


            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;
            return File(stream, "application/word", string.Format("Протокол {0}.docx", name));
        }

        [HttpPost]
        public ActionResult DeleteProtocol(string rowId) {
            try {
                var result = new PriceProjectRepository().DeleteProtocol(new Guid(rowId));
                return result ? Content(bool.TrueString) : Content(bool.FalseString);
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Ошибка удаления протокола", ex);
                return Content(bool.FalseString);
            }
        }

        [HttpPost]
        public ActionResult CompleteProtocol(string rowId) {
            try {
                var result = new PriceProjectRepository().CompleteProtocol(new Guid(rowId));
                return result ? Content(bool.TrueString) : Content(bool.FalseString);
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Ошибка завершения протокола", ex);
                return Content(bool.FalseString);
            }
        }

    }
}