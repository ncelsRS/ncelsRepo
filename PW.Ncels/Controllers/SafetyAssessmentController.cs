using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.Contract;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Helpers;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Web.Script.Serialization;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.Models.OBK;

namespace PW.Ncels.Controllers
{
    [Authorize]
    public class SafetyAssessmentController : ComAssessmentController
    {

        // GET: SafetyAssessment
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// подача заявления
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(string type)
        {
            var model = new OBK_AssessmentDeclaration
            {
                Id = Guid.NewGuid(),
                EmployeeId = UserHelper.GetCurrentEmployee().Id,
                StatusId = CodeConstManager.STATUS_DRAFT_ID,
                ObkContracts = new OBK_Contract(),
                ObkRsProductses = new List<OBK_RS_Products>()
            };
            
            model.CertificateDate = null;
            model.CreatedDate = DateTime.Now;

            var safetyRepository = new SafetyAssessmentRepository(false);

            if (type == CodeConstManager.OBK_SA_SERIAL)
            {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu",
                    model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_SERIAL).Id);
            }
            if (type == CodeConstManager.OBK_SA_PARTY)
            {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu",
                    model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_PARTY).Id);
            }
            if (type == CodeConstManager.OBK_SA_DECLARATION)
            {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu",
                    model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_DECLARATION).Id);
            }

            ViewData["ContractList"] =
                new SelectList(safetyRepository.GetActiveContractListWithInfo(model.EmployeeId, safetyRepository.GetObkRefTypes(type).Id), "Id",
                    "ContractInfo", model.ContractId);

            var repository = new ReadOnlyDictionaryRepository();

            //огранизационная форма
            var orgForm = safetyRepository.GetOrganizationForm();
            ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name");

            var certType = safetyRepository.GetCertificateType();
            ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu");

            //Наличие сертификата GMP
            var booleans = repository.GetCertificateGMPCheck();
            ViewData["IsGMPList"] = new SelectList(booleans, "CertificateGMPCheck", "NameRu", model.CertificateGMPCheck);

            //справочник стран
            var countries = safetyRepository.GetCounties();
            ViewData["Counties"] = new SelectList(countries, "Id", "Name");

            //Валюта
            var currency = safetyRepository.GetObkCurrencies();
            ViewData["Courrency"] = new SelectList(currency, "Id", "Name");

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            new SafetyAssessmentRepository().DeleteReport(id, UserHelper.GetCurrentEmployee().Id);
            return RedirectToAction("RegisterSafetyAssessmentList");
        }

        public ActionResult RegisterSafetyAssessmentList()
        {
            return View();
        }

        public ActionResult ExportFileTemplate(Guid id, string url)
        {
            return PartialView(new OBKEntityTemplate { Id = id, Url = url });
        }

        /// <summary>
        /// формирование печатной формы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExportFilePdf(Guid id)
        {
            var db = new ncelsEntities();
            string name = "Заявление на проведение оценки безопасности и качества лс.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/SafetyAssessmentDeclaration.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = id;
                report.Dictionary.Variables["ExecutorSign"].ValueObject = null;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            Stream stream = new MemoryStream();
            var assessmentDeclaration = db.OBK_AssessmentDeclaration.FirstOrDefault(dd => dd.Id == id);
            var assessmentDeclarationHistory = assessmentDeclaration.OBK_AssessmentDeclarationHistory.Where(dh => dh.XmlSign != null)
                .OrderByDescending(dh => dh.DateCreate).FirstOrDefault();
            if (assessmentDeclarationHistory != null)
            {
                try
                {
                    report.ExportDocument(StiExportFormat.Word2007, stream);
                    stream.Position = 0;
                    Aspose.Words.Document doc = new Aspose.Words.Document(stream);
                    doc.InserQrCodesToEnd("ExecutorSign", assessmentDeclarationHistory.XmlSign);
                    var pdfFile = new MemoryStream();
                    doc.Save(pdfFile, Aspose.Words.SaveFormat.Pdf);
                    pdfFile.Position = 0;
                    stream.Close();

                    return new FileStreamResult(pdfFile, "application/pdf");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }

        /// <summary>
        /// Акт выполненных работ
        /// </summary>
        /// <returns></returns>
        public ActionResult CertOfComplectFilePdf(Guid id)
        {
            OBKExpDocumentRepository expRepo = new OBKExpDocumentRepository();
            string name = "Акт выполненных работ.pdf";

            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkCertificateOfCompletion.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = id;
                report.Dictionary.Variables["ContractId"].ValueObject = expRepo.GetAssessmentDeclaration(id).ContractId;
                report.Dictionary.Variables["ValueAddedTax"].ValueObject = expRepo.GetValueAddedTax();
                var totalCount = expRepo.GetContractPrice(expRepo.GetAssessmentDeclaration(id).ContractId);
                report.Dictionary.Variables["TotalCount"].ValueObject = totalCount;
                var priceText = RuDateAndMoneyConverter.CurrencyToTxtTenge(Convert.ToDouble(totalCount), false);
                report.Dictionary.Variables["TotalCountText"].ValueObject = priceText;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
            //return File(stream, "application/pdf", name);
        }


        public ActionResult DublicateDrug(string id)
        {
            var model = new SafetyAssessmentRepository().DublicateAssessmentDeclaration(id, UserHelper.GetCurrentEmployee().Id);
            FillDeclarationControl(model);
            return RedirectToAction("RegisterSafetyAssessmentList");
        }

        [HttpGet]
        public ActionResult Edit(string id, bool? isLoadReest)
        {
            var model = GetSaDeclarationById(id);
            return View("Create", model);
        }

        [HttpGet]
        public ActionResult EditContract(string id)
        {
            var repository = new SafetyAssessmentRepository();
            var contract = repository.GetContractById(new Guid(id));
            var model = repository.FindDeclarationByContract(new Guid(id));
            if (model == null) {
                model = new OBK_AssessmentDeclaration {
                    EmployeeId = UserHelper.GetCurrentEmployee().Id,
                    TypeId = contract.OBK_Ref_Type.Id,
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    StatusId = CodeConstManager.STATUS_DRAFT_ID,
                    CertificateDate = DateTime.Now,
                    ContractId = contract.Id,
                    IsDeleted = false,
                    CertificateGMPCheck = contract.OBK_Ref_Type.Code == CodeConstManager.OBK_SA_DECLARATION
                };
                repository.SaveAssessmentDeclaration(model);
            }
            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ShowDetails(string id)
        {
          var model = GetSaDeclarationById(id);
          return View(model);
        }

        public OBK_AssessmentDeclaration GetSaDeclarationById(string id)
        {
          var repository = new SafetyAssessmentRepository();
          var model = repository.GetById(id);
          FillDeclarationControl(model);
          return model;
        }

        private void FillDeclarationControl(OBK_AssessmentDeclaration model)
        {
            var safetyRepository = new SafetyAssessmentRepository();
            ViewData["ContractList"] =
                new SelectList(safetyRepository.GetActiveContractListWithInfo(model.EmployeeId, model.TypeId), "Id",
                    "ContractInfo", model.ContractId);

            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_SERIAL)) {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu", model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_SERIAL).Id);
            }
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_PARTY)) {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu", model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_PARTY).Id);
            }
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_DECLARATION)) {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu", model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_DECLARATION).Id);
            }

            if (model.ContractId != null)
            {
                var contract = safetyRepository.GetContractById(model.ContractId);
                var declarant = safetyRepository.GetDeclarantById(contract.DeclarantId);
                var declarantContact = safetyRepository.GetDeclarantContactById(contract.DeclarantContactId);
                var products = safetyRepository.GetRsProductsAndSeries(contract.Id);

                //справочник стран
                var countries = safetyRepository.GetCounties();
                if (declarant?.CountryId == null) {
                    ViewData["Counties"] = new SelectList(countries, "Id", "Name");
                }
                else {
                    ViewData["Counties"] = new SelectList(countries, "Id", "Name",
                        model.CountryId = declarant.CountryId);
                }

                //Валюта
                var currency = safetyRepository.GetObkCurrencies();
                if (declarantContact?.CurrencyId == null) {
                    ViewData["Courrency"] = new SelectList(currency, "Id", "Name");
                }
                else {
                    ViewData["Courrency"] = new SelectList(currency, "Id", "Name",
                        model.CurrencyId = declarantContact.CurrencyId);
                }

                //Наличие сертификата GMP
                var repository = new ReadOnlyDictionaryRepository();
                var booleans = repository.GetCertificateGMPCheck();
                ViewData["IsGMPList"] = new SelectList(booleans, "CertificateGMPCheck", "NameRu",
                    model.CertificateGMPCheck);

                var certType = safetyRepository.GetCertificateType();
                if (model.CertificateTypeId == null) {
                    ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu");
                } else {
                    ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu", model.CertificateTypeId);
                }

                //организационная форма
                var orgForm = safetyRepository.GetOrganizationForm();
                if (declarant?.OrganizationFormId == null) {
                    ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name");
                }
                else {
                    ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name",
                        model.OrganizationFormId = declarant.OrganizationFormId);
                }

                model.StartDate = string.Format("{0:dd.MM.yyyy}", contract.StartDate);
                model.EndDate = string.Format("{0:dd.MM.yyyy}", contract.EndDate);
                model.NameKz = declarant?.NameKz ?? "нет данных";
                model.NameRu = declarant?.NameRu ?? "нет данных";
                model.NameEn = declarant?.NameEn ?? "нет данных";
                model.ChiefLastName = declarantContact?.BossLastName ?? "нет данных";
                model.ChiefFirstName = declarantContact?.BossFirstName ?? "нет данных";
                model.ChiefMiddleName = declarantContact?.BossMiddleName ?? "нет данных";
                model.ChiefPosition = declarantContact?.BossPosition ?? "нет данных";
                model.AddressFact = declarantContact?.AddressFact ?? "нет данных";
                model.AddressLegal = declarantContact?.AddressLegalRu ?? "нет данных";
                model.Phone = declarantContact?.Phone ?? "нет данных";
                model.Email = declarantContact?.Email ?? "нет данных";
                model.BankBik = declarantContact?.BankBik ?? "нет данных";
                model.BankIik = declarantContact?.BankIik ?? "нет данных";
                model.BankName = declarantContact?.BankNameRu ?? "нет данных";

                var resultProducts = new List<OBK_RS_Products>();
                foreach (var product in products)
                {
                    var prod = new OBK_RS_Products();
                    prod.Id = product.Id;
                    prod.NameRu = product.NameRu;
                    prod.NameKz = product.NameKz;
                    prod.ProducerNameRu = product.ProducerNameRu;
                    prod.ProducerNameKz = product.ProducerNameKz;
                    prod.CountryNameRu = product.CountryNameRu;
                    prod.CountryNameKZ = product.CountryNameKZ;
                    prod.TnvedCode = product.TnvedCode;
                    prod.KpvedCode = product.KpvedCode;
                    prod.Price = product.Price;
                    foreach (var productSeries in product.OBK_Procunts_Series)
                    {
                        var prodSeries = new OBK_Procunts_Series();
                        prodSeries.Id = productSeries.Id;
                        prodSeries.Series = productSeries.Series;
                        prodSeries.SeriesStartdate = productSeries.SeriesStartdate;
                        prodSeries.SeriesEndDate = productSeries.SeriesEndDate;
                        prodSeries.SeriesParty = productSeries.SeriesParty;
                        prodSeries.SeriesShortNameRu = productSeries.sr_measures.short_name;
                        prod.OBK_Procunts_Series.Add(prodSeries);
                    }
                    resultProducts.Add(prod);
                }
                model.ObkRsProductses = resultProducts;
            }
            else
            {
                var countries = safetyRepository.GetCounties();
                ViewData["Counties"] = new SelectList(countries, "Id", "Name");

                //Валюта
                var currency = safetyRepository.GetObkCurrencies();
                ViewData["Courrency"] = new SelectList(currency, "Id", "Name");

                var repository = new ReadOnlyDictionaryRepository();
                //Наличие сертификата GMP
                var booleans = repository.GetCertificateGMPCheck();
                ViewData["IsGMPList"] = new SelectList(booleans, "CertificateGMPCheck", "NameRu",
                    model.CertificateGMPCheck);

                var certType = safetyRepository.GetCertificateType();
                ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu");

                //организационная форма
                var orgForm = safetyRepository.GetOrganizationForm();
                ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name");
            }
        }

        /// <summary>
        /// получение списка заявлений
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ReadSafetyAssessmentDeclaration(ModelRequest request, int? type)
        {
            return Json(await new SafetyAssessmentRepository().GetSafetyAssessmentDeclarationList(request, true, type), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult UpdateModel(string code, int typeId, string modelId, string userId, long? recordId, string fieldName, string fieldValue, string fieldDisplay)
        {
            var filter = new SafetyAssessmentRepository().UpdateModel(code, typeId, modelId, userId, recordId, fieldName, fieldValue, fieldDisplay);
            return Json(new { Success = true, modelId = filter.ModelId, recordId = filter.RecordId, controlId = filter.ControlId });
        }

        /// <summary>
        /// Выбор договора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetContract(Guid id)
        {
            var contract = new SafetyAssessmentRepository().GetContractById(id);

            if (contract == null) {
                return Json(new { isSuccess = false });
            }

            var declarant = new SafetyAssessmentRepository().GetDeclarantById(contract.DeclarantId);
            var declarantContact = new SafetyAssessmentRepository().GetDeclarantContactById(contract.DeclarantContactId);
            var products = new SafetyAssessmentRepository().GetRsProductsAndSeries(contract.Id);
            
            var result = new OBK_AssessmentDeclaration();
            
            result.StartDate = string.Format("{0:dd.MM.yyyy}", contract.StartDate);
            result.EndDate = string.Format("{0:dd.MM.yyyy}", contract.EndDate);
            result.OrganizationFormId = declarant?.OrganizationFormId ?? null;
            result.NameKz = declarant?.NameKz ?? "нет данных";
            result.NameRu = declarant?.NameRu ?? "нет данных";
            result.NameEn = declarant?.NameEn ?? "нет данных";
            result.ChiefLastName = declarantContact?.BossLastName ?? "нет данных";
            result.ChiefFirstName = declarantContact?.BossFirstName ?? "нет данных";
            result.ChiefMiddleName = declarantContact?.BossMiddleName ?? "нет данных";
            result.ChiefPosition = declarantContact?.BossPosition ?? "нет данных";
            result.AddressFact = declarantContact?.AddressFact ?? "нет данных";
            result.AddressLegal = declarantContact?.AddressLegalRu ?? "нет данных";
            result.Phone = declarantContact?.Phone ?? "нет данных";
            result.Email = declarantContact?.Email ?? "нет данных";
            result.BankBik = declarantContact?.BankBik ?? "нет данных";
            result.BankIik = declarantContact?.BankIik ?? "нет данных";
            result.BankName = declarantContact?.BankNameRu ?? "нет данных";
            result.CountryId = declarant?.CountryId ?? null;
            result.CurrencyId = declarantContact?.CurrencyId ?? null;

            var resultProducts = new List<OBK_RS_Products>();
            foreach (var product in products)
            {
                var prod = new OBK_RS_Products();
                prod.Id = product.Id;
                prod.NameRu = product.NameRu;
                prod.NameKz = product.NameKz;
                prod.ProducerNameRu = product.ProducerNameRu;
                prod.ProducerNameKz = product.ProducerNameKz;
                prod.CountryNameRu = product.CountryNameRu;
                prod.CountryNameKZ = product.CountryNameKZ;
                prod.TnvedCode = product.TnvedCode;
                prod.KpvedCode = product.KpvedCode;
                prod.Price = product.Price;
                prod.CurrencyId = product.CurrencyId;
                prod.DrugFormBoxCount = product.DrugFormBoxCount;
                prod.DrugFormFullName = product.DrugFormFullName;
                prod.RegTypeId = product.RegTypeId;
                foreach (var productSeries in product.OBK_Procunts_Series)
                {
                    var prodSeries = new OBK_Procunts_Series();
                    prodSeries.Id = productSeries.Id;
                    prodSeries.Series = productSeries.Series;
                    prodSeries.SeriesStartdate = productSeries.SeriesStartdate;
                    prodSeries.SeriesEndDate = productSeries.SeriesEndDate;
                    prodSeries.SeriesParty = productSeries.SeriesParty;
                    prodSeries.SeriesShortNameRu = productSeries.sr_measures.short_name;
                    prod.OBK_Procunts_Series.Add(prodSeries);
                }
                foreach (var mtPart in product.OBK_MtPart)
                {
                    var mtParts = new OBK_MtPart();
                    mtParts.PartNumber = mtPart.PartNumber;
                    mtParts.Model = mtPart.Model;
                    mtParts.Specification = mtPart.Specification;
                    mtParts.ProducerName = mtPart.ProducerName;
                    mtParts.CountryName = mtPart.CountryName;
                    mtParts.Name = mtPart.Name;
                    prod.OBK_MtPart.Add(mtParts);
                }
                resultProducts.Add(prod);
            }
            result.ObkRsProductses = resultProducts;
            return Json(new { isSuccess = true , result });
        }
        [HttpPost]
        public virtual ActionResult GetHistory(Guid modelId)
        {
            var repository = new SafetyAssessmentRepository();
            var result = new List<OBK_DeclarationHistory>();
            var models = repository.GetDeclarationHistory(modelId);
            foreach (var model in models)
            {
                if (repository.GetStageStatus(model.StageStatusId).Code == OBK_Ref_StageStatus.Completed &&
                    model.StatusId == 9)
                {
                    var history = new OBK_DeclarationHistory();
                    history.StartDateHistory = DateHelper.GetDate(model.StartDate); 
                    history.EndDateHistory = DateHelper.GetDate(model.EndDate);
                    history.Note = model.Note;
                    history.StatusName = repository.GetStatus(model.StatusId).NameRu;
                    history.StageName = repository.GetStage(model.StageId).NameRu;
                    result.Add(history);
                }
            }
            return Json(new {isSuccess = true, result});
        }


        public ActionResult SignOperation(string id)
        {
            var repository = new SafetyAssessmentRepository();
            var model = repository.GetPreamble(new Guid(id));
            bool IsSuccess = true;
            string preambleXml = string.Empty;
            try
            {
                preambleXml = SerializeHelper.SerializeDataContract(model);
                preambleXml = preambleXml.Replace("utf-16", "utf-8");
            }
            catch (Exception)
            {
                IsSuccess = false;
            }

            return Json(new
            {
                IsSuccess,
                preambleXml
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// подписание
        /// </summary>
        /// <param name="preambleId"></param>
        /// <param name="xmlAuditForm"></param>
        /// <returns></returns>
        public ActionResult SignForm(string preambleId, string xmlAuditForm)
        {
            var success = true;
            var repository = new SafetyAssessmentRepository();
            var model = repository.GetById(preambleId);
            new SignDocumentRepository().SaveSignDocument(UserHelper.GetCurrentEmployee().Id, xmlAuditForm, model);

            if (model.StatusId == CodeConstManager.STATUS_DRAFT_ID)
            {
                model.FirstSendDate = DateTime.Now;
            }

            var modelStage = new AssessmentStageRepository().GetByDeclarationId(model.Id.ToString(), CodeConstManager.STAGE_OBK_COZ);
            if (modelStage != null && modelStage.StageStatusId ==
                new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InReWork).Id)
            {
                modelStage.StageStatusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                new SafetyAssessmentRepository().SaveStage(modelStage);
            }

            model.StatusId = CodeConstManager.STATUS_SEND_ID;
            model.SendDate = DateTime.Now;
            model.IsSigned = false;

            var history = new OBK_AssessmentDeclarationHistory()
            {
                DateCreate = DateTime.Now,
                AssessmentDeclarationId = model.Id,
                XmlSign = xmlAuditForm,
                StatusId = CodeConstManager.STATUS_SEND_ID,
                UserId = UserHelper.GetCurrentEmployee().Id,
                Note = "Отчет предоставлен. Дата отправки:" + DateTime.Now
            };
            if (string.IsNullOrEmpty(model.Number))
            {
                model.Number = repository.GetAppNumber();
            }
            repository.SaveOrUpdate(model, UserHelper.GetCurrentEmployee().Id);
            repository.SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// отправка заявки без подписи 
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult SendNotSign(string modelId)
        {
            var repository = new SafetyAssessmentRepository();
            var model = repository.GetById(modelId);

            if (model.StatusId == CodeConstManager.STATUS_DRAFT_ID)
            {
                model.FirstSendDate = DateTime.Now;
            }

            var modelStage = new AssessmentStageRepository().GetByDeclarationId(modelId, CodeConstManager.STAGE_OBK_COZ);
            if (modelStage != null && modelStage.StageStatusId ==
                new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InReWork).Id)
            {
                modelStage.StageStatusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                new SafetyAssessmentRepository().SaveStage(modelStage);
            }

            model.StatusId = CodeConstManager.STATUS_SEND_ID;
            model.SendDate = DateTime.Now;
            model.IsSigned = false;
            var history = new OBK_AssessmentDeclarationHistory()
            {
                DateCreate = DateTime.Now,
                AssessmentDeclarationId = model.Id,
                StatusId = CodeConstManager.STATUS_SEND_ID,
                UserId = UserHelper.GetCurrentEmployee().Id,
                Note = "Отчет предоставлен без подписи. Дата отправки:" + DateTime.Now
            };
            if (string.IsNullOrEmpty(model.Number))
            {
                model.Number = repository.GetAppNumber();
            }
            repository.SaveOrUpdate(model, UserHelper.GetCurrentEmployee().Id);
            repository.SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            return Json(new
            {
                isSuccess = true
            });
        }
    }
}