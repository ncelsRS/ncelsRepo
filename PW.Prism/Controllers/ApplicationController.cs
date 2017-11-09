using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ncelsEntities _db = UserHelper.GetCn();

        // GET: /IncomingDoc/


        public ActionResult FormPrint(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult FormMyTask(Guid id)
        {

            ViewBag.StageList = new List<Item>()
            {
                new Item { Id = "1", Name = "Первичная экспертиза"},
                new Item { Id = "2", Name = "Фармакологическая экспертиза"},
                new Item { Id = "3", Name = "Фармацевтическая экспертиза"},
                new Item { Id = "4", Name = "Аналитическая экспертиза"},
                new Item { Id = "5", Name = "Переводы"},
                new Item { Id = "6", Name = "Заключение оценки безопасности"},
            };

            ViewBag.StateList = new List<Item>()
       {        new Item { Id = "0", Name = "Новый"},
                new Item { Id = "1", Name = "В работе"},
                new Item { Id = "2", Name = "Исполнен"},
                new Item { Id = "3", Name = "Исполненный отрицательно"},
                new Item { Id = "4", Name = "Принят на исполнение"}
            };
            return PartialView(id);
        }

        public ActionResult FormMyTaskRef(Guid id)
        {

            ViewBag.StageList = new List<Item>();

            ViewBag.StateList = new List<Item>() {
                new Item {Id = "0", Name = "Новый"},
                new Item {Id = "1", Name = "В работе"},
                new Item {Id = "2", Name = "Исполнен"},
                new Item {Id = "3", Name = "Исполненный отрицательно"},
                new Item {Id = "4", Name = "Принят на исполнение"}
            };
            return PartialView(id);
        }

        public ActionResult FormAllTask(Guid id)
        {
            ViewBag.StageList = new List<Item>()
          {
                new Item { Id = "1", Name = "Первичная экспертиза"},
                new Item { Id = "2", Name = "Фармакологическая экспертиза"},
                new Item { Id = "3", Name = "Фармацевтическая экспертиза"},
                new Item { Id = "4", Name = "Аналитическая экспертиза"},
                new Item { Id = "5", Name = "Переводы"},
                new Item { Id = "6", Name = "Заключение оценки безопасности"},
            };
            ViewBag.StateList = new List<Item>()
     {        new Item { Id = "0", Name = "Новый"},
                new Item { Id = "1", Name = "В работе"},
                new Item { Id = "2", Name = "Исполнен"},
                new Item { Id = "3", Name = "Исполненный отрицательно"},
                new Item { Id = "4", Name = "Принят на исполнение"}
            };
            return PartialView(id);
        }
        public ActionResult FormOutgoingList(Guid id)
        {
            ViewBag.AttachDicId = DictionaryHelper.GetDictionaryIdFirst("sysAttachApplicant");

            return PartialView(id);
        }
        public ActionResult FormFile(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult FormFilePrice(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult FormFileRef(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult PriceProjectArchiveRead([DataSourceRequest] DataSourceRequest request, int type, int year){
            var data = _db.PriceProjectArchiveViews.Where(x=>
                x.RequestOrderType == type 
                && x.RequestOrderYear == year)
            .AsNoTracking()
            .OrderBy(o => o.CreatedDate);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult ExportPriceProjectArchive([DataSourceRequest] DataSourceRequest request)
        {
            var filters = request.Filters;

            return Json("Ok");
        }

        public ActionResult FormPrice(Guid id)
        {
            PriceProjectsView projectRkk = _db.PriceProjectsViews.FirstOrDefault(x => x.Id == id);
            Price project = null;
            if (projectRkk.Type == (int)PriceProjectType.PriceLs || projectRkk.Type == (int)PriceProjectType.RePriceLs)
            {
                project = _db.Prices.FirstOrDefault(x => x.PriceProjectId == id
                    && (x.Type == (int)PriceType.LsCurrentPrice || x.Type == (int)PriceType.ReLsCurrentPrice));
            }
            else
            {
                var projectImn = _db.Prices.Where(x => x.PriceProjectId == id
                    && (x.Type == (int)PriceType.ImnCurrentPrice || x.Type == (int)PriceType.ReImnCurrentPrice));
                if (projectImn.Any())
                {
                    project = new Price
                    {
                        CipPrice = projectImn.Sum(p => p.CipPrice),
                        ManufacturerPrice = projectImn.Sum(p => p.ManufacturerPrice),
                        OwnerPrice = projectImn.Sum(p => p.OwnerPrice),
                        RefPrice = projectImn.Sum(p => p.RefPrice),
                        UnitPrice = projectImn.Sum(p => p.UnitPrice),
                        BritishPrice = projectImn.Sum(p => p.BritishPrice),
                        CreatedDate = projectImn.FirstOrDefault().CreatedDate,
                        CalcDateStart = projectImn.FirstOrDefault().CalcDateStart,
                        CalcDateEnd = projectImn.FirstOrDefault().CalcDateEnd,
                        AvgObkCost = projectImn.FirstOrDefault().AvgObkCost,
                        AvgOptCost = projectImn.FirstOrDefault().AvgOptCost,
                        AvgRznCost = projectImn.FirstOrDefault().AvgRznCost,
                        LimitCost = projectImn.FirstOrDefault().LimitCost,
                        ZakupCost = projectImn.FirstOrDefault().ZakupCost,
                        MarkupCost = projectImn.FirstOrDefault().MarkupCost,
                        MarkupCostOpt = projectImn.FirstOrDefault().MarkupCostOpt

                    };
                }
            }
            string name = string.Empty;
            string number = string.Empty;
            decimal count = 0; //Количество в упаковке
            if (projectRkk != null)
            {
                name = projectRkk.NameRu;
                number = projectRkk.RegNumber;
                decimal.TryParse(projectRkk.CountPackage, out count);
                if (count == 0)
                    count++;
            }
            CalcModel calcModel = new CalcModel();
            if (project != null && projectRkk != null)
            {
                calcModel.Id = id;
                calcModel.CipPrice = project.CipPrice;
                calcModel.ManufacturerPrice = project.ManufacturerPrice;
                calcModel.OwnerPrice = project.OwnerPrice;
                calcModel.RefPrice = project.RefPrice;
                calcModel.UnitPrice = project.UnitPrice;
                calcModel.BritishPrice = project.BritishPrice;
                if (projectRkk.Type == (int)PriceProjectType.PriceLs || projectRkk.Type == (int)PriceProjectType.PriceImn)
                {
                    project.OriginalCost = project.UnitPrice;
                }
                else
                {
                    var parentProject = _db.PriceProjects.FirstOrDefault(x => x.Id == projectRkk.PriceProjectId);
                    if (parentProject != null)
                    {
                        var parenPrice = _db.Prices.FirstOrDefault(
                                x => x.PriceProjectId == parentProject.Id && x.Type == (int)PriceType.LsCurrentPrice);
                        if (parenPrice != null)
                        {
                            project.OriginalCost = parenPrice.UnitPrice;
                        }
                    }
                }

                calcModel.OriginalCost = project.OriginalCost;
                calcModel.CreatedDate = project.CreatedDate != null && project.CreatedDate > DateTime.Now.AddYears(-100) ? project.CreatedDate.Value : DateTime.Now;
                calcModel.CalcDateStart = project.CalcDateStart != null && project.CalcDateStart > DateTime.Now.AddYears(-100) ? project.CalcDateStart.Value : calcModel.CreatedDate;
                calcModel.CalcDateEnd = project.CalcDateEnd != null && project.CalcDateEnd > DateTime.Now.AddYears(-100) ? project.CalcDateEnd.Value : DateTime.Now;

                if (project.AvgObkCost < 1)
                {
                    if (projectRkk.RegisterId != null)
                    {
                        calcModel.AvgObkCost =
                            AvgObkRegister(calcModel.CalcDateStart, calcModel.CalcDateEnd, projectRkk.RegisterId.Value, projectRkk.RegisterDfId, count);
                    }
                    else
                    {
                        calcModel.AvgObkCost = project.AvgObkCost == 0 ? AvgObk(name) / count : project.AvgObkCost;
                    }
                }
                else
                {
                    calcModel.AvgObkCost = project.AvgObkCost;
                }

                calcModel.AvgOptCost = project.AvgOptCost == 0 ? AvgOpt(number) / count : project.AvgOptCost;
                calcModel.AvgRznCost = project.AvgRznCost == 0 ? AvgRzn(number) / count : project.AvgRznCost;
                calcModel.LimitCost = project.LimitCost == 0 ? AvgLim(number) : project.LimitCost;
                calcModel.ZakupCost = project.ZakupCost == 0 ? AvgZak(name) / count : project.ZakupCost;
                calcModel.MinimalCost = MinRef(calcModel);
                calcModel.MarkupCost = project.MarkupCost;
                calcModel.MarkupCostOpt = project.MarkupCostOpt;
                calcModel.PriceProjectType = projectRkk.Type;
            }

            return PartialView(calcModel);
        }
        public ActionResult FormProtocols(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult ReportDetails(Guid id)
        {
            var list = _db.Reports.Where(o => o.TaskId == id && o.Type == 2 && o.Type == 3);
            Report report = list.FirstOrDefault() ?? new Report() { Text = "Отчет не создан" };
            return PartialView(report);
        }
        public ActionResult DocumentOutDetails(Guid id)
        {

            var document = _db.Documents.First(o => o.Id == id);

            return PartialView("DocFormOut", document);
        }

        public ActionResult ListMyTask([DataSourceRequest] DataSourceRequest request, Guid id)
        {

            string organizationId = UserHelper.GetCurrentEmployee().Id.ToString();
            var docs = _db.Tasks.Where(o => o.IsActive == true && o.DocumentId == id && o.ExecutorId == organizationId);
            DataSourceResult result = docs.Select(o => new { o.Id, o.Text, o.Stage, o.ExecutionDate, o.AuthorValue, o.Type, o.State, TaskId = o.Id, o.DocumentId }).ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ListPrice([DataSourceRequest] DataSourceRequest request, Guid id)
        {

            var docs = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 99).ToList();

            if (docs.Count == 0)
            {
                CreatePriceLands(id, "Беларусь");
                CreatePriceLands(id, "Венгрия");
                CreatePriceLands(id, "Латвия");
                CreatePriceLands(id, "Чехия");
                CreatePriceLands(id, "Австрия");
                CreatePriceLands(id, "Россия");
                CreatePriceLands(id, "Турция");
                CreatePriceLands(id, "Украина");
                docs = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 99).OrderBy(x => x.CreatedDate).ToList();
            }

            DataSourceResult result = docs.ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        private void CreatePriceLands(Guid id, string name)
        {
            var country = _db.Dictionaries.FirstOrDefault(x => x.Type == "Country" && x.Name == name);
            if (country != null)
            {
                Price price = new Price()
                {
                    Id = Guid.NewGuid(),
                    PriceProjectId = id,
                    ManufacturerPriceCurrencyDicId = null,
                    ManufacturerPrice = 0,
                    CipPrice = 0,
                    RefPrice = 0,
                    CountryId = country.Id,
                    Name = null,
                    CreatedDate = DateTime.Now,
                    Type = 99
                };
                _db.Prices.Add(price);
                _db.SaveChanges();
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PriceCreate([DataSourceRequest] DataSourceRequest request, PricesViewModel dictionary, Guid id)
        {

            PriceProjectsView projectRkk = _db.PriceProjectsViews.FirstOrDefault(x => x.Id == id);
            decimal count = 0;
            if (projectRkk != null)
            {
                decimal.TryParse(projectRkk.CountPackage, out count);
                if (count == 0)
                    count++;
            }
            DateTime now = DateTime.Now.Date;
            decimal cost = dictionary.ManufacturerPrice;
            if (dictionary.ManufacturerPriceCurrencyDicId.HasValue)
            {
                var valutaPw = _db.Dictionaries.FirstOrDefault(x => x.Id == dictionary.ManufacturerPriceCurrencyDicId.Value);
                if (valutaPw != null)
                {
                    var rateYear = now.Month == 1 ? now.Year - 1 : now.Year;
                    var rateMonth = now.Month == 1 ? 12 : now.Month - 1;
                    var valutaObk = _db.obk_currencies.FirstOrDefault(x => x.currency_code == valutaPw.Code);
                    if (valutaObk != null)
                    {

                        // берем курс за текущий день 
                        // если нет то последний актуальный из существующих
                        var exch = _db.AvgExchangeRatesViews.FirstOrDefault(x => x.currency_id == valutaObk.id && x.year == rateYear && x.month == rateMonth) ??
                                   _db.AvgExchangeRatesViews.Where(x => x.currency_id == valutaObk.id && x.year <= rateYear && x.month <= rateMonth)
                                       .OrderByDescending(x => x.year).ThenByDescending(x => x.month).FirstOrDefault();
                        if (exch != null)
                        {
                            cost = cost * exch.rate.Value;
                        }
                    }
                }
            }

            Price price = new Price()
            {
                Id = Guid.NewGuid(),
                PriceProjectId = id,
                ManufacturerPriceCurrencyDicId = dictionary.ManufacturerPriceCurrencyDicId,
                ManufacturerPrice = dictionary.ManufacturerPrice,
                RefPrice = cost,
                CipPrice = cost / count,
                CountryId = dictionary.CountryId,
                Name = dictionary.Name,
                CreatedDate = DateTime.Now,
                Type = 99
            };
            _db.Prices.Add(price);
            _db.SaveChanges();

            var result = _db.PricesViews.First(o => o.Id == price.Id);
            return Json(new[] { result }.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PriceUpdate([DataSourceRequest] DataSourceRequest request, PricesViewModel dictionary)
        {
            PriceProjectsView projectRkk = _db.PriceProjectsViews.FirstOrDefault(x => x.Id == dictionary.PriceProjectId);
            decimal count = 0;
            if (projectRkk != null)
            {
                decimal.TryParse(projectRkk.CountPackage, out count);
                if (count == 0)
                    count++;
            }
            DateTime now = DateTime.Now.Date;
            decimal cost = dictionary.ManufacturerPrice;
            if (dictionary.ManufacturerPriceCurrencyDicId.HasValue)
            {
                var valutaPw = _db.Dictionaries.FirstOrDefault(x => x.Id == dictionary.ManufacturerPriceCurrencyDicId.Value);
                if (valutaPw != null)
                {
                    var rateYear = now.Month == 1 ? now.Year - 1 : now.Year;
                    var rateMonth = now.Month == 1 ? 12 : now.Month - 1;
                    var valutaObk = _db.obk_currencies.FirstOrDefault(x => x.currency_code == valutaPw.Code);
                    if (valutaObk != null)
                    {
                        var exch = _db.AvgExchangeRatesViews.FirstOrDefault(x => x.currency_id == valutaObk.id && x.year == rateYear && x.month == rateMonth) ??
                                   _db.AvgExchangeRatesViews.Where(x => x.currency_id == valutaObk.id && x.year <= rateYear && x.month <= rateMonth)
                                       .OrderByDescending(x => x.year).ThenByDescending(x => x.month).FirstOrDefault();
                        if (exch != null)
                        {
                            cost = cost * exch.rate.Value;
                        }
                    }
                }
            }
            var price = _db.Prices.First(o => o.Id == dictionary.Id);

            price.ManufacturerPriceCurrencyDicId = dictionary.ManufacturerPriceCurrencyDicId;
            price.ManufacturerPrice = dictionary.ManufacturerPrice;
            price.RefPrice = cost;
            price.CipPrice = cost / count;
            price.CountryId = dictionary.CountryId;
            price.Name = dictionary.Name;
            price.CreatedDate = DateTime.Now;
            _db.SaveChanges();

            var result = _db.PricesViews.First(o => o.Id == price.Id);
            return Json(new[] { result }.ToDataSourceResult(request));

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PriceDestroy([DataSourceRequest] DataSourceRequest request, PricesViewModel dictionary)
        {
            if (dictionary != null)
            {
                Price d = _db.Prices.First(o => o.Id == dictionary.Id);
                _db.Prices.Remove(d);
                _db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        public JsonResult ReportCommentTask([DataSourceRequest] DataSourceRequest request, Guid taskId)
        {

            var list = _db.Reports.Where(o => o.TaskId == taskId && o.Type == 4).Select(o => new { o.Id, o.Text });
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListAllTask([DataSourceRequest] DataSourceRequest request, Guid id)
        {


            var docs = _db.Tasks.Where(o => o.IsActive == true && o.DocumentId == id);
            DataSourceResult result = docs.Select(o => new { o.Id, o.Text, o.ExecutionDate, o.Stage, o.AuthorValue, o.Type, o.State, TaskId = o.Id, o.DocumentId }).ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ListOutgoing([DataSourceRequest] DataSourceRequest request, Guid id)
        {

            var key = id.ToString();
            var docs = _db.Documents.Where(o => o.IsDeleted == false && ((o.DocumentType == 1 && o.AnswersId.Contains(key)) || (o.DocumentType == 0 && o.ProjectType == 3 && o.AnswersId.Contains(key))));
            DataSourceResult result = docs.Select(o => new { o.Id, o.Number, o.DocumentDate, o.CreatedUserValue, o.Summary, TaskId = o.Id }).ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ReCalcObk(string id, string date1, string date2)
        {
            decimal result = 0;

            DateTime startDate = DateTime.ParseExact(date1, "dd.MM.yyyy", null);
            DateTime endDate = DateTime.ParseExact(date2, "dd.MM.yyyy", null);

            var _id = Guid.Parse(id);
            PriceProjectsView projectRkk = _db.PriceProjectsViews.FirstOrDefault(x => x.Id == _id);
            if (projectRkk == null || projectRkk.RegisterId == null)
                result = 0;
            else
            {
                decimal count = 0;
                decimal.TryParse(projectRkk.CountPackage, out count);
                if (count == 0)
                    count++;

                result = AvgObkRegister(startDate, endDate, projectRkk.RegisterId.Value, projectRkk.RegisterDfId, count);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DocumentBuild(CalcModel model)
        {
            Price project = _db.Prices.FirstOrDefault(x => x.PriceProjectId == model.Id && x.Type == 0);
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            if (project != null)
            {
                project.AvgObkCost = model.AvgObkCost;
                project.AvgOptCost = model.AvgOptCost;
                project.AvgRznCost = model.AvgRznCost;
                project.BritishCost = model.BritishCost;
                project.LimitCost = model.LimitCost;
                project.MinimalCost = model.MinimalCost;
                project.ZakupCost = model.ZakupCost;
                project.OriginalCost = model.OriginalCost;
                if (model.PriceProjectType == (int)PriceProjectType.RePriceLs || model.PriceProjectType == (int)PriceProjectType.RePriceImn)
                {
                    project.UnitPrice = model.UnitPrice;
                }
                else
                {
                    project.UnitPrice = model.OriginalCost;
                }

                project.CreatedDate = model.CreatedDate < DateTime.Now.AddYears(-100) ? DateTime.Now : model.CreatedDate;
                project.CalcDateStart = model.CalcDateStart < DateTime.Now.AddYears(-100) ? DateTime.Now : model.CalcDateStart;
                project.CalcDateEnd = model.CalcDateEnd < DateTime.Now.AddYears(-100) ? DateTime.Now : model.CalcDateEnd;
                startDate = project.CalcDateStart.Value;
                endDate = project.CalcDateEnd.Value;

                _db.SaveChanges();
            }

            var report = new StiReport();
            report.Load(HttpContext.Server.MapPath("/Content/Projects/price.mrt"));
            foreach (var dbItem in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                dbItem.ConnectionString = UserHelper.GetCnString();
            }
            report["prjId"] = model.Id;
            report["obkStartDate"] = startDate;
            report["obkEndDate"] = endDate;
            report.Dictionary.Variables["obkStartDateStr"].Value = startDate.ToString("dd.MM.yyyy");
            report.Dictionary.Variables["obkEndDateStr"].Value = endDate.ToString("dd.MM.yyyy");
            report.Render();

            MemoryStream memoryStream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel2007, memoryStream);
            memoryStream.Position = 0;

            byte[] data = new byte[memoryStream.Length];
            memoryStream.Read(data, 0, data.Length);
            UploadHelper.Upload(data, "price.xls", model.Id);
            memoryStream.Close();
            FileHelper.ReplaceTextExcel(model.Id.ToString(), "price.xls",
                new List<ReplaceItem>() {
                    new ReplaceItem() {Key = "data1", Value = "Новое значение"},
                    new ReplaceItem() {Key = "data2", Value = "Второе значение"}
                });


            return
                Content(JsonConvert.SerializeObject(new { State = true }, Formatting.Indented,
                    new JsonSerializerSettings() { }));
        }

        [HttpPost]
        public ActionResult CalcMark(CalcModel model)
        {
            Price project = _db.Prices.FirstOrDefault(x => x.PriceProjectId == model.Id && x.Type == 0);
            if (project != null)
            {
                project.MarkupCost = CalcMarkupRzn(project);
                project.MarkupCostOpt = CalcMarkupOpt(project);
                _db.SaveChanges();
            }

            return
                Content(JsonConvert.SerializeObject(new { State = true }, Formatting.Indented,
                    new JsonSerializerSettings()));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrjPrtCreate([DataSourceRequest] DataSourceRequest request, PrtPrjsView2 protocol, Guid id1)
        {


            PrtPrj prt = new PrtPrj();
            prt.Id = Guid.NewGuid();
            prt.ProtocolId = protocol.ProtocolId.Value;
            prt.ProjectId = id1;
            _db.PrtPrjs.Add(prt);
            _db.SaveChanges();

            var result = _db.PrtPrjsViews.First(o => o.Id == prt.Id);
            return Json(new[] { result }.ToDataSourceResult(request));

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrjPrtUpdate([DataSourceRequest] DataSourceRequest request, PrtPrjsView2 protocol)
        {


            var prt = _db.PrtPrjs.First(o => o.Id == protocol.Id);

            prt.ResultDictionaryId = protocol.ResultDictionaryId;
            _db.SaveChanges();

            var result = _db.PrtPrjsView2.First(o => o.Id == protocol.Id);
            return Json(new[] { result }.ToDataSourceResult(request));

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrjPrtDestroy([DataSourceRequest] DataSourceRequest request, PrtPrjsView2 protocol)
        {
            if (protocol != null)
            {
                PrtPrj d = _db.PrtPrjs.First(o => o.Id == protocol.Id);
                _db.PrtPrjs.Remove(d);
                _db.SaveChanges();
            }

            return Json(new[] { protocol }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrtPrjCreate([DataSourceRequest] DataSourceRequest request, PrtPrjsView project, Guid id1)
        {


            PrtPrj prt = new PrtPrj();
            prt.Id = Guid.NewGuid();
            prt.ProtocolId = id1;
            prt.ProjectId = project.ProjectId.Value;
            _db.PrtPrjs.Add(prt);
            _db.SaveChanges();

            var result = _db.PrtPrjsViews.First(o => o.Id == prt.Id);
            return Json(new[] { result }.ToDataSourceResult(request));

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrtPrjDestroy([DataSourceRequest] DataSourceRequest request, PrtPrjsView project)
        {
            if (project != null)
            {
                PrtPrj d = _db.PrtPrjs.First(o => o.Id == project.Id);
                _db.PrtPrjs.Remove(d);
                _db.SaveChanges();
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }

        private decimal AvgObk(string name)
        {
            var list = _db.ReesrtObks.Where(x => x.name.Contains(name) && x.cost.HasValue && x.cost != 0);
            if (list.Any())
            {
                var avg = list.Average(x => x.costExch ?? 0);
                return (decimal)avg;
            }
            return 0;
        }

        private decimal AvgObkRegister(DateTime startDate, DateTime endtDate, long registerId, long? registerDfId, decimal count)
        {
            if (count == 0)
            {
                count++;
            }
            var list = _db.ReesrtObks.Where(x =>
                x.register_id == registerId
                && x.cost.HasValue
                && x.reg_date > startDate
                && x.reg_date <= endtDate);
            if (registerDfId != null)
            {
                list = list.Where(x => x.drug_form_id == registerDfId);
            }

            if (list.Any())
            {
                var avg = list.Average(x => x.costExch ?? x.cost ?? 0) / count;
                return (decimal)avg;
            }
            return 0;
        }

        private decimal AvgRzn(string number)
        {
            var list = _db.Ref_MarketPrices.Where(x => x.Type == 0 && x.Number == number && x.Price.HasValue && x.Price != 0);
            if (list.Any())
            {
                var avg = list.Average(x => x.Price ?? 0);
                return (decimal)avg;
            }
            return 0;
        }

        private decimal AvgOpt(string number)
        {
            var list = _db.Ref_MarketPrices.Where(x => x.Type == 0 && x.Number == number && x.Price.HasValue && x.Price != 0);
            if (list.Any())
            {
                var avg = list.Average(x => x.Price ?? 0);
                return (decimal)avg;
            }
            return 0;
        }
        private decimal AvgZak(string name)
        {
            var list = _db.Ref_PurchasePrices.Where(x => x.Name.Contains(name) && x.Price.HasValue && x.Price != 0);
            if (list.Any())
            {
                var avg = list.Average(x => x.Price ?? 0);
                return (decimal)avg;
            }
            return 0;
        }

        private decimal AvgLim(string number)
        {
            var list = _db.Ref_Limits.Where(x => x.Number == number && x.Cost != 0);
            if (list.Any())
            {
                var avg = list.Average(x => x.Cost);
                return (decimal)avg;
            }
            return 0;
        }

        private decimal MinRef(CalcModel obj)
        {
            var docs = _db.PricesViews.Where(o => o.PriceProjectId == obj.Id && o.Type == 99).OrderBy(x => x.CipPrice).ToList();
            if (docs.Any())
            {
                // минимальная цена в странах
                var docsMinPrice = docs.Any(p => p.CipPrice > 0) == false ? 0 : docs.GroupBy(e => e.CountryId).Select(g => g.Sum(p => p.CipPrice) / g.Count()).Where(p => p > 0).Min();
                var list = new[] {
                    obj.AvgObkCost,
                    obj.AvgOptCost,
                    obj.AvgRznCost,
                    obj.LimitCost,
                    obj.ZakupCost
                };
                // минимальная цена отличная от нуля
                var listMinPrice = list.Any(p => p > 0) == false ? 0 : list.Where(p => p > 0).Min();
                if (docsMinPrice > 0 && listMinPrice > 0)
                {
                    return docsMinPrice > listMinPrice ? listMinPrice : docsMinPrice;
                }
                else if ((docsMinPrice == 0 && listMinPrice > 0) || (listMinPrice == 0 && docsMinPrice > 0))
                {
                    return docsMinPrice == 0 ? listMinPrice : docsMinPrice;
                }
                return 0;
            }
            else
            {
                var list = new[] {
                    obj.AvgObkCost,
                    obj.AvgOptCost,
                    obj.AvgRznCost,
                    obj.LimitCost,
                    obj.ZakupCost
                };
                return list.Any(p => p > 0) == false ? 0 : list.Where(p => p > 0).Min();
            }
        }

        [HttpPost]
        public ActionResult ReCalcMinRef(Guid id, decimal avgObkCost, decimal avgOptCost, decimal avgRznCost, decimal limitCost, decimal zakupCost)
        {
            var docs = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 99).OrderBy(x => x.CipPrice).ToList();
            if (docs.Any())
            {
                // минимальная цена в странах
                var docsMinPrice = docs.Any(p => p.CipPrice > 0) == false ? 0 : docs.GroupBy(e => e.CountryId).Select(g => g.Sum(p => p.CipPrice) / g.Count()).Where(p => p > 0).Min();
                var list = new[] {
                    avgObkCost,
                    avgOptCost,
                    avgRznCost,
                    limitCost,
                    zakupCost
                };
                // минимальная цена отличная от нуля
                var listMinPrice = list.Any(p => p > 0) == false ? 0 : list.Where(p => p > 0).Min();
                if (docsMinPrice > 0 && listMinPrice > 0)
                {
                    return docsMinPrice > listMinPrice ? Json(listMinPrice, JsonRequestBehavior.AllowGet) : Json(docsMinPrice, JsonRequestBehavior.AllowGet);
                }
                else if ((docsMinPrice == 0 && listMinPrice > 0) || (listMinPrice == 0 && docsMinPrice > 0))
                {
                    return docsMinPrice == 0 ? Json(listMinPrice, JsonRequestBehavior.AllowGet) : Json(docsMinPrice, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = new[] {
                    avgObkCost,
                    avgOptCost,
                    avgRznCost,
                    limitCost,
                    zakupCost
                };
                return list.Any(p => p > 0) == false ? Json(0, JsonRequestBehavior.AllowGet) : Json(list.Where(p => p > 0).Min(), JsonRequestBehavior.AllowGet);
            }
        }

        private decimal CalcMarkupOpt(Price obj)
        {
            if (obj.UnitPrice <= 300)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.14;

            if (obj.UnitPrice > 300 && obj.UnitPrice <= 3000)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.13;

            if (obj.UnitPrice > 3000 && obj.UnitPrice <= 30000)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.12;

            if (obj.UnitPrice > 30000 && obj.UnitPrice <= 100000)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.11;

            return obj.UnitPrice + obj.UnitPrice * (decimal)0.10;
        }

        private decimal CalcMarkupRzn(Price obj)
        {
            if (obj.UnitPrice <= 300)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.24;

            if (obj.UnitPrice > 300 && obj.UnitPrice <= 3000)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.23;

            if (obj.UnitPrice > 3000 && obj.UnitPrice <= 30000)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.22;

            if (obj.UnitPrice > 30000 && obj.UnitPrice <= 100000)
                return obj.UnitPrice + obj.UnitPrice * (decimal)0.21;

            return obj.UnitPrice + obj.UnitPrice * (decimal)0.20;
        }
    }
}

