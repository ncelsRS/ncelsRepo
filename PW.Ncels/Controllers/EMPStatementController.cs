using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Controllers
{
    public class EMPStatementController : Controller
    {
        private ncelsEntities _ctx;

        public EMPStatementController()
        {
            _ctx = new ncelsEntities();
        }
        // GET: EMPStatement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStatements(ModelRequest request)
        {
            var list = new List<object>
            {
                new
                {
                    Kind = "вид 1",
                    Type = "тип 1",
                    MedicalDeviceName = "наименование изделия 1",
                    Number = "123",
                    Status = "статус 1",
                    StartDate = DateTime.Now,
                    Actions = "123"
                },
                new
                {
                    Kind = "вид 2",
                    Type = "тип 2",
                    MedicalDeviceName = "наименование изделия 2",
                    Number = "123ddd",
                    Status = "статус 2",
                    StartDate = DateTime.Now.AddDays(1),
                    Actions = "1sefaewf23"
                }
            };
            return Json(new
            {
                draw = request.Draw,
                recordsFiltered = list.Count,
                recordsTotal = list.Count,
                Data = list
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Statement()
        {
            ViewBag.ReturnUrl = HttpContext.Request.UrlReferrer;
            return View();
        }

        public ActionResult LoadStatement()
        {
            var statement = new EmpStatementViewModel
            {
                RegistrationKindValue = "3",
                RegistrationKind = new List<SelectListItem>
                {
                    new SelectListItem{Value = "1", Text = "Регистрация"},
                    new SelectListItem{Value = "2", Text = "Перерегистрация"},
                    new SelectListItem{Value = "3", Text = "Внесение изменений"}
                },
                RegistrationCertificateNumber = "123",
                NormativeDocumentNumber = "444",
                RegistrationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(1),
                ChangeData = new List<EmpStatementChangeViewModel>
                {
                    new EmpStatementChangeViewModel
                    {
                        Name = "изменение",
                        Type = "тип 123",
                        BeforeChange = "before",
                        AfterChange = "after"
                    }
                },
                CortractList = _ctx.EMP_Contract.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Number + " " + x.MedicalDeviceName
                }).ToList(),
                RegistrationType = new List<SelectListItem>
                {
                    new SelectListItem{Value = "1", Text = "Ускоренная"}
                },
                LetterNumber = "5555",
                LetterDate = DateTime.Now.AddDays(2),
                IsMt = true,
                MedicalDeviceNameKz = "наименование на каз",
                MedicalDeviceNameRu = "наименование рус",
                NomenclatureCode = "zzzz",
                NomenclatureNameKz = "номенклатура каз",
                NomenclatureNameRu = "номенклатура рус",
                NomenclatureDescriptionKz = "описание каз",
                NomenclatureDescriptionRu = "описание рус",
                ApplicationAreaKz = "Область применения каз",
                ApplicationAreaRu = "Область применения рус",
                PurposeKz = "назначение каз",
                PurposeRu = "назначение рус",
                IsClosedSystem = false,
                RegistrationDossierPageNumber = 12,
                ShortTechnicalCharacteristicKz = "краткаяхарактеристика каз",
                ShortTechnicalCharacteristicRu = "краткаяхарактеристика рус",
                ClassOfPotentialRisk = 1,
                IsBalk = false,
                IsMeasurementDevice = true,
                IsForInvitroDiagnostics = false,
                IsSterile = true,
                IsMedicalProductPresence = false,
                WithouAe = true,
                StorageLifeData = new List<EmpStatementStorageLifeViewModel>
                {
                    new EmpStatementStorageLifeViewModel {
                        Kind = "asd",
                        ExpirationDate = DateTime.Now,
                        Measure = "kg",
                        IsIndefinitely = true
                    }
                },
                TransportConditions = "условия транспортирования",
                StorageConditions = "условия хранения",
                CountryRegistrationData = new List<EmpStatementCountryRegistrationViewModel>
                {
                    new EmpStatementCountryRegistrationViewModel
                    {
                        Country = "country",
                        RegistrationNumber = "reg number",
                        StartDate = DateTime.Now,
                        ExpirationDate = DateTime.Now.AddDays(1),
                        IsIndefinitely = true
                    }
                },
                Production = "произвдство",
                MedicalDeviceComplectationData = new List<EmpStatementMedicalDeviceComplectationViewModel>
                {
                    new EmpStatementMedicalDeviceComplectationViewModel
                    {
                        Type = "тип",
                        Name = "наименование",
                        Id = "id",
                        Model = "model",
                        Manufacturer = "manufac",
                        Country = "country"
                    }
                },
                MedicalDevicePackageData = new[]
                {
                    new EmpStatementMedicalDevicePackagViewModel
                    {
                        Kind = "kind",
                        Name = "name",
                        VolumeValue = 100,
                        VolumeUnit = "kg",
                        Count = 123,
                        Description = "tratata"
                    }
                },
                IsComplectation = true,
                ManufacturerType = "тип производителя",
                ManufacturerNameRu = "производитель рус",
                AllowedDocumentNumber = "ууу",
                BossLastName = "фамилия руководителя",
                BossPosition = "должность руководителя",
                OrganizationForm = "организационная форма",
                ManufacturerNameKz = "производитель каз",
                DateOfIssue = DateTime.Now,
                BossFirstName = "имя руководителя",
                Phone = "1234-135",
                Country = "country",
                ManufacturerNameEn = "производитель англ",
                ManufacturerExpirationDate = DateTime.Now,
                BossMiddleName = "отчество руководителя",
                Email = "eagaer@gmail.com",
                ContactPersonInitials = "фио контактного лица",
                ContactPersonLegalAddress = "юр адрес контактного лица",
                ContactPersonPosition = "должность контактного лица",
                ContactPersonFactAddress = "факт адрес контактного лица",
                Agreement = "но короче я тут соглашаюсь со всем",
                IsAgreed = true
            };
            return Json(statement, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StatementSave(EmpStatementViewModel vm)
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }

    public class EmpStatementMedicalDevicePackagViewModel
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public int VolumeValue { get; set; }
        public string VolumeUnit { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }

    public class EmpStatementMedicalDeviceComplectationViewModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
    }

    public class EmpStatementCountryRegistrationViewModel
    {
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsIndefinitely { get; set; }
    }

    public class EmpStatementStorageLifeViewModel
    {
        public string Kind { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Measure { get; set; }
        public bool IsIndefinitely { get; set; }
    }

    public class EmpStatementChangeViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string BeforeChange { get; set; }
        public string AfterChange { get; set; }
    }

    public class EmpStatementViewModel
    {
        public List<SelectListItem> RegistrationKind { get; set; }
        public string RegistrationCertificateNumber { get; set; }
        public string NormativeDocumentNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<EmpStatementChangeViewModel> ChangeData { get; set; }
        public List<SelectListItem> CortractList { get; set; }
        public List<SelectListItem> RegistrationType { get; set; }
        public string LetterNumber { get; set; }
        public DateTime LetterDate { get; set; }
        public bool IsMt { get; set; }
        public string MedicalDeviceNameKz { get; set; }
        public string MedicalDeviceNameRu { get; set; }
        public string NomenclatureCode { get; set; }
        public string NomenclatureNameKz { get; set; }
        public string NomenclatureNameRu { get; set; }
        public string NomenclatureDescriptionKz { get; set; }
        public string NomenclatureDescriptionRu { get; set; }
        public string ApplicationAreaKz { get; set; }
        public string ApplicationAreaRu { get; set; }
        public string PurposeKz { get; set; }
        public string PurposeRu { get; set; }
        public bool IsClosedSystem { get; set; }
        public int RegistrationDossierPageNumber { get; set; }
        public string ShortTechnicalCharacteristicKz { get; set; }
        public string ShortTechnicalCharacteristicRu { get; set; }
        public int ClassOfPotentialRisk { get; set; }
        public bool IsBalk { get; set; }
        public bool IsMeasurementDevice { get; set; }
        public bool IsForInvitroDiagnostics { get; set; }
        public bool IsSterile { get; set; }
        public bool IsMedicalProductPresence { get; set; }
        public bool WithouAe { get; set; }
        public List<EmpStatementStorageLifeViewModel> StorageLifeData { get; set; }
        public string TransportConditions { get; set; }
        public string StorageConditions { get; set; }
        public List<EmpStatementCountryRegistrationViewModel> CountryRegistrationData { get; set; }
        public string Production { get; set; }
        public List<EmpStatementMedicalDeviceComplectationViewModel> MedicalDeviceComplectationData { get; set; }
        public object MedicalDevicePackageData { get; set; }
        public bool IsComplectation { get; set; }
        public string ManufacturerType { get; set; }
        public string ManufacturerNameRu { get; set; }
        public string AllowedDocumentNumber { get; set; }
        public string BossLastName { get; set; }
        public string BossPosition { get; set; }
        public string OrganizationForm { get; set; }
        public string ManufacturerNameKz { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string BossFirstName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string ManufacturerNameEn { get; set; }
        public DateTime ManufacturerExpirationDate { get; set; }
        public string BossMiddleName { get; set; }
        public string Email { get; set; }
        public string ContactPersonInitials { get; set; }
        public string ContactPersonLegalAddress { get; set; }
        public string ContactPersonPosition { get; set; }
        public string ContactPersonFactAddress { get; set; }
        public string Agreement { get; set; }
        public bool IsAgreed { get; set; }
        public string RegistrationKindValue { get; set; }
    }
}