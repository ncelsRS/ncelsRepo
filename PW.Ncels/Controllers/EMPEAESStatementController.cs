using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PW.Ncels.Controllers
{
    public class EMPEAESStatementController : Controller
    {
        private NcelsEntities _ctx;

        public EMPEAESStatementController()
        {
            _ctx = new NcelsEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStatements(ModelRequest request)
        {
            var query = _ctx.EMP_EAESStatement.Select(x => new
            {
                Id = x.Id,
                Kind = x.RegistrationKindValue == "1"
                    ? "Регистрация"
                    : x.RegistrationKindValue == "2"
                        ? "Перерегистрация"
                        : "Внесение изменений",
                MedicalDeviceName = x.MedicalDeviceNameRu,
                Number = x.RegistrationCertificateNumber,
                Status = "Черновик",
                StartDate = x.RegistrationDate,
                Actions = string.Empty
            }).ToList();

            return Json(new
            {
                draw = request.Draw,
                recordsFiltered = query.Count,
                recordsTotal = query.Count,
                Data = query
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Statement(Guid? id)
        {
            ViewBag.ReturnUrl = HttpContext.Request.UrlReferrer;
            return View(id);
        }

        public ActionResult LoadStatement(Guid? id)
        {
            var statement = _ctx.EMP_EAESStatement.FirstOrDefault(x => x.Id == id) ?? new EMP_EAESStatement()
            {
                Agreement = "Гарантирую: достоверность и идентичность информации, содержащейся в регистрационном досье и заявлении, представление  образцов изделий медицинского назначения, стандартных образцов в количествах, достаточных для трехкратного анализа, специфические реагенты, расходные материалы, применяемые при проведении испытаний (в исключительных случаях и на условиях возврата), а также их соответствие нормативным документам, представляемым на регистрацию. Обязуюсь сообщать обо всех изменениях в регистрационное досье, а также представлять заявление и материалы при обнаружении побочных воздействий при применении изделия медицинского назначения, медицинской техники, ранее не указанных в инструкции по медицинскому применению изделий медицинского назначения / руководстве по эксплуатации медицинской техники.",
                DateOfIssue = DateTime.Now,
                ManufacturerExpirationDate = DateTime.Now
            };

            var changes = _ctx.EMP_StatementChange.Where(x => x.StatementId == statement.Id).ToList();
            var storageLifes = _ctx.EMP_StatementStorageLife.Where(x => x.StatementId == statement.Id).ToList();
            var countryRegistrations = _ctx.EMP_StatementCountryRegistration.Where(x => x.StatementId == statement.Id).ToList();
            var complectations = _ctx.EMP_StatementMedicalDeviceComplectation.Where(x => x.StatementId == statement.Id).ToList();
            var packages = _ctx.EMP_StatementMedicalDevicePackage.Where(x => x.StatementId == statement.Id).ToList();

            var statementVm = new EmpEaesStatementViewModel
            {
                GarantExpDate = statement.GarantExpDate,
                GarantNoExp = statement.GarantNoExp,
                GarantUnit = statement.GarantUnit,
                Id = statement.Id,
                RegistrationKindValue = statement.RegistrationKindValue,
                RegistrationKind = new List<SelectListItem>
                {
                    new SelectListItem{Value = "1", Text = "Регистрация"},
                    new SelectListItem{Value = "3", Text = "Внесение изменений"}
                },
                RegistrationCertificateNumber = statement.RegistrationCertificateNumber,
                NormativeDocumentNumber = statement.NormativeDocumentNumber,
                RegistrationDate = statement.RegistrationDate,
                ExpirationDate = statement.ExpirationDate == DateTime.MinValue ? DateTime.Now : statement.ExpirationDate,
                ChangeData = changes.Select(x => new EmpEaesStatementChangeViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    BeforeChange = x.BeforeChange,
                    AfterChange = x.AfterChange
                }).ToList(),
                ContractId = statement.ContractId,
                CortractList = _ctx.EMP_Contract
                    .Where(c => c.EMP_Ref_ContractScope.Code == "eaesrg" || c.EMP_Ref_ContractScope.Code == "eaesgp")
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Number + " " + x.MedicalDeviceName
                    }).ToList(),
                LetterNumber = statement.LetterNumber,
                LetterDate = statement.LetterDate ?? DateTime.Now,
                IsMt = statement.IsMt ?? false,
                MedicalDeviceNameKz = statement.MedicalDeviceNameKz,
                MedicalDeviceNameRu = statement.MedicalDeviceNameRu,
                NomenclatureCode = statement.NomenclatureCode,
                NmirkId = statement.NmirkId,
                NomenclatureNameKz = statement.NomenclatureNameKz,
                NomenclatureNameRu = statement.NomenclatureNameRu,
                NomenclatureDescriptionKz = statement.NomenclatureDescriptionKz,
                NomenclatureDescriptionRu = statement.NomenclatureDescriptionRu,
                ApplicationAreaKz = statement.ApplicationAreaKz?.Split(",".ToCharArray())?.ToList(),
                ApplicationAreaRu = statement.ApplicationAreaRu?.Split(",".ToCharArray())?.ToList(),
                PurposeKz = statement.PurposeKz,
                PurposeRu = statement.PurposeRu,
                IsClosedSystem = statement.IsClosedSystem ?? false,
                RegistrationDossierPageNumber = statement.RegistrationDossierPageNumber,
                ShortTechnicalCharacteristicKz = statement.ShortTechnicalCharacteristicKz,
                ShortTechnicalCharacteristicRu = statement.ShortTechnicalCharacteristicRu,
                ClassOfPotentialRisk = statement.ClassOfPotentialRisk,
                IsBalk = statement.IsBalk ?? false,
                IsMeasurementDevice = statement.IsMeasurementDevice ?? false,
                IsForInvitroDiagnostics = statement.IsForInvitroDiagnostics ?? false,
                IsSterile = statement.IsSterile ?? false,
                IsMedicalProductPresence = statement.IsMedicalProductPresence ?? false,
                WithouAe = statement.WithouAe ?? false,
                StorageLifeData = storageLifes.Select(x => new EmpEaesStatementStorageLifeViewModel
                {
                    Id = x.Id,
                    Kind = x.Kind,
                    ExpirationDate = x.ExpirationDate,
                    Measure = x.Measure,
                    IsIndefinitely = x.IsIndefinitely ?? false
                }).ToList(),
                TransportConditions = statement.TransportConditions,
                StorageConditions = statement.StorageConditions,
                CountryRegistrationData = countryRegistrations.Select(x => new EmpEaesStatementCountryRegistrationViewModel
                {
                    Id = x.Id,
                    Country = x.Country,
                    RegistrationNumber = x.RegistrationNumber,
                    StartDate = x.StartDate,
                    ExpirationDate = x.ExpirationDate,
                    IsIndefinitely = x.IsIndefinitely ?? false
                }).ToList(),
                Production = statement.Production,
                MedicalDeviceComplectationData = complectations.Select(x => new EmpEaesStatementMedicalDeviceComplectationViewModel
                {
                    Id = x.Id,
                    Type = x.Type,
                    Name = x.Name,
                    Identifier = x.Identifier,
                    Model = x.Model,
                    Manufacturer = x.Manufacturer,
                    Country = x.Country
                }).ToList(),
                MedicalDevicePackageData = packages.Select(x => new EmpEaesStatementMedicalDevicePackageViewModel
                {
                    Id = x.Id,
                    Kind = x.Kind,
                    Name = x.Name,
                    VolumeValue = x.VolumeValue,
                    VolumeUnit = x.VolumeUnit,
                    Count = x.Count ?? 0,
                    Description = x.Description
                }).ToList(),
                IsComplectation = statement.IsComplectation ?? false,
                ManufacturerType = statement.ManufacturerType,
                ManufacturerNameRu = statement.ManufacturerNameRu,
                AllowedDocumentNumber = statement.AllowedDocumentNumber,
                BossLastName = statement.BossLastName,
                BossPosition = statement.BossPosition,
                OrganizationForm = statement.OrganizationForm,
                ManufacturerNameKz = statement.ManufacturerNameKz,
                DateOfIssue = statement.DateOfIssue,
                BossFirstName = statement.BossFirstName,
                Phone = statement.Phone,
                Country = statement.Country,
                ManufacturerNameEn = statement.ManufacturerNameEn,
                ManufacturerExpirationDate = statement.ManufacturerExpirationDate,
                BossMiddleName = statement.BossMiddleName,
                Email = statement.Email,
                ContactPersonInitials = statement.ContactPersonInitials,
                ContactPersonLegalAddress = statement.ContactPersonLegalAddress,
                ContactPersonPosition = statement.ContactPersonPosition,
                ContactPersonFactAddress = statement.ContactPersonFactAddress,
                Agreement = statement.Agreement,
                IsAgreed = statement.IsAgreed ?? false,
                RegCountries = _ctx.EMP_EAESStatementRegCountry
                    .Where(c => c.StatementId == statement.Id)
                    .Select(c => new EmpEaesStatementRegCountry
                    {
                        Id = c.Id,
                        Country = c.Country,
                        DateOfIssue = c.DateOfIssue,
                        ExpDate = c.ExpDate,
                        IsIndefinitely = c.IsIndefinitely,
                        RegNumber = c.RegNumber
                    }),
                //Samples = statement.EMP_StatementSamples.Select(s => new EmpEaesStatementSampleVm
                //{
                //    Id = s.Id,
                //    Addition = s.Addition,
                //    Conditions = s.Conditions,
                //    Count = s.Count,
                //    CreateDate = s.CreateDate,
                //    ExpirationDate = s.ExpirationDate,
                //    Name = s.Name,
                //    SampleType = s.SampleType,
                //    SeriesPart = s.SeriesPart,
                //    Storage = s.Storage,
                //    Unit = s.Unit
                //}),
                RefCountry = statement.RefCountry,
                ConCountry = statement.ConCountry,
                PlaceType = statement.PlaceType,
                PlaceNameRu = statement.PlaceNameRu,
                PlaceAllowedDocumentNumber = statement.PlaceAllowedDocumentNumber,
                PlaceBossLastName = statement.PlaceBossLastName,
                PlaceBossPosition = statement.PlaceBossPosition,
                PlaceOrganizationForm = statement.PlaceOrganizationForm,
                PlaceNameKz = statement.PlaceNameKz,
                PlaceDateOfIssue = statement.PlaceDateOfIssue,
                PlaceBossFirstName = statement.PlaceBossFirstName,
                PlacePhone = statement.PlacePhone,
                PlaceCountry = statement.PlaceCountry,
                PlaceNameEn = statement.PlaceNameEn,
                PlaceExpirationDate = statement.PlaceExpirationDate,
                PlaceBossMiddleName = statement.PlaceBossMiddleName,
                PlaceEmail = statement.PlaceEmail,
                PlaceContactPersonInitials = statement.PlaceContactPersonInitials,
                PlaceContactPersonPosition = statement.PlaceContactPersonPosition,
                PlaceContactPersonFactAddress = statement.PlaceContactPersonFactAddress,
                ShowerType = statement.ShowerType,
                ShowerNameRu = statement.ShowerNameRu,
                ShowerAllowedDocumentNumber = statement.ShowerPAllowedDocumentNumber,
                ShowerBossLastName = statement.ShowerBossLastName,
                ShowerBossPosition = statement.ShowerBossPosition,
                ShowerOrganizationForm = statement.ShowerOrganizationForm,
                ShowerNameKz = statement.ShowerNameKz,
                ShowerDateOfIssue = statement.ShowerDateOfIssue,
                ShowerBossFirstName = statement.ShowerBossFirstName,
                ShowerPhone = statement.ShowerPhone,
                ShowerCountry = statement.ShowerCountry,
                ShowerNameEn = statement.ShowerNameEn,
                ShowerExpirationDate = statement.ShowerExpirationDate,
                ShowerBossMiddleName = statement.ShowerBossMiddleName,
                ShowerEmail = statement.ShowerEmail,
                ShowerContactPersonInitials = statement.ShowerContactPersonInitials,
                ShowerContactPersonPosition = statement.ShowerContactPersonPosition,
                ShowerContactPersonFactAddress = statement.ShowerContactPersonFactAddress,
                ShowerContactPersonActualAddress = statement.ShowerContactPersonActualAddress
            };
            return Json(statementVm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContractData(Guid id)
        {
            var res = _ctx.EMP_Contract
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    IMNName = c.MedicalDeviceName,
                    IMNNameKz = c.MedicalDeviceNameKz,
                    Type = c.EMP_Ref_ContractType
                }).FirstOrDefault();
            var res1 = new
            {
                res.IMNName,
                res.IMNNameKz,
                res.Type?.Code
            };
            return Json(res1, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StatementSave(EmpEaesStatementViewModel vm)
        {
            var statement = _ctx.EMP_EAESStatement.FirstOrDefault(x => x.Id == vm.Id);
            if (statement == null)
            {
                statement = new EMP_EAESStatement { Id = Guid.NewGuid() };
                _ctx.EMP_EAESStatement.Add(statement);
            }

            statement.RegistrationKindValue = vm.RegistrationKindValue;
            statement.RegistrationCertificateNumber = vm.RegistrationCertificateNumber;
            statement.NormativeDocumentNumber = vm.NormativeDocumentNumber;
            if (vm.RegistrationDate != null) statement.RegistrationDate = vm.RegistrationDate;
            if (vm.ExpirationDate != null) statement.ExpirationDate = vm.ExpirationDate;
            statement.ContractId = vm.ContractId;
            statement.LetterNumber = vm.LetterNumber;
            if (vm.LetterDate != null) statement.LetterDate = vm.LetterDate;
            statement.IsMt = vm.IsMt;
            statement.MedicalDeviceNameKz = vm.MedicalDeviceNameKz;
            statement.MedicalDeviceNameRu = vm.MedicalDeviceNameRu;
            statement.NomenclatureCode = vm.NomenclatureCode;
            statement.NmirkId = vm.NmirkId;
            statement.NomenclatureNameKz = vm.NomenclatureNameKz;
            statement.NomenclatureNameRu = vm.NomenclatureNameRu;
            statement.NomenclatureDescriptionKz = vm.NomenclatureDescriptionKz;
            statement.NomenclatureDescriptionRu = vm.NomenclatureDescriptionRu;
            statement.ApplicationAreaKz = string.Join(",", vm.ApplicationAreaKz);
            statement.ApplicationAreaRu = string.Join(",", vm.ApplicationAreaRu);
            statement.PurposeKz = vm.PurposeKz;
            statement.PurposeRu = vm.PurposeRu;
            statement.IsClosedSystem = vm.IsClosedSystem;
            statement.RegistrationDossierPageNumber = vm.RegistrationDossierPageNumber;
            statement.ShortTechnicalCharacteristicKz = vm.ShortTechnicalCharacteristicKz;
            statement.ShortTechnicalCharacteristicRu = vm.ShortTechnicalCharacteristicRu;
            statement.ClassOfPotentialRisk = vm.ClassOfPotentialRisk;
            statement.IsBalk = vm.IsBalk;
            statement.IsMeasurementDevice = vm.IsMeasurementDevice;
            statement.IsForInvitroDiagnostics = vm.IsForInvitroDiagnostics;
            statement.IsSterile = vm.IsSterile;
            statement.IsMedicalProductPresence = vm.IsMedicalProductPresence;
            statement.WithouAe = vm.WithouAe;
            statement.TransportConditions = vm.TransportConditions;
            statement.StorageConditions = vm.StorageConditions;
            statement.Production = vm.Production;
            statement.IsComplectation = vm.IsComplectation;
            statement.ManufacturerType = vm.ManufacturerType;
            statement.ManufacturerNameRu = vm.ManufacturerNameRu;
            statement.AllowedDocumentNumber = vm.AllowedDocumentNumber;
            statement.BossLastName = vm.BossLastName;
            statement.BossPosition = vm.BossPosition;
            statement.OrganizationForm = vm.OrganizationForm;
            statement.ManufacturerNameKz = vm.ManufacturerNameKz;
            if (vm.DateOfIssue != null) statement.DateOfIssue = vm.DateOfIssue;
            statement.BossFirstName = vm.BossFirstName;
            statement.Phone = vm.Phone;
            statement.Country = vm.Country;
            statement.ManufacturerNameEn = vm.ManufacturerNameEn;
            if (vm.ManufacturerExpirationDate != null) statement.ManufacturerExpirationDate = vm.ManufacturerExpirationDate;
            statement.BossMiddleName = vm.BossMiddleName;
            statement.Email = vm.Email;
            statement.ContactPersonInitials = vm.ContactPersonInitials;
            statement.ContactPersonLegalAddress = vm.ContactPersonLegalAddress;
            statement.ContactPersonPosition = vm.ContactPersonPosition;
            statement.ContactPersonFactAddress = vm.ContactPersonFactAddress;
            statement.Agreement = vm.Agreement;
            statement.IsAgreed = vm.IsAgreed;
            statement.RefCountry = vm.RefCountry;
            statement.ConCountry = vm.ConCountry;
            statement.GarantExpDate = vm.GarantExpDate;
            statement.GarantNoExp = vm.GarantNoExp;
            statement.GarantUnit = vm.GarantUnit;

            statement.PlaceType = vm.PlaceType;
                statement.PlaceNameRu = vm.PlaceNameRu;
                statement.PlaceAllowedDocumentNumber = vm.PlaceAllowedDocumentNumber;
                statement.PlaceBossLastName = vm.PlaceBossLastName;
                statement.PlaceBossPosition = vm.PlaceBossPosition;
                statement.PlaceOrganizationForm = vm.PlaceOrganizationForm;
                statement.PlaceNameKz = vm.PlaceNameKz;
                statement.PlaceDateOfIssue = vm.PlaceDateOfIssue;
                statement.PlaceBossFirstName = vm.PlaceBossFirstName;
                statement.PlacePhone = vm.PlacePhone;
                statement.PlaceCountry = vm.PlaceCountry;
                statement.PlaceNameEn = vm.PlaceNameEn;
                statement.PlaceExpirationDate = vm.PlaceExpirationDate;
                statement.PlaceBossMiddleName = vm.PlaceBossMiddleName;
                statement.PlaceEmail = vm.PlaceEmail;
                statement.PlaceContactPersonInitials = vm.PlaceContactPersonInitials;
                statement.PlaceContactPersonPosition = vm.PlaceContactPersonPosition;
                statement.PlaceContactPersonFactAddress = vm.PlaceContactPersonFactAddress;
                statement.ShowerType = vm.ShowerType;
                statement.ShowerNameRu = vm.ShowerNameRu;
                statement.ShowerPAllowedDocumentNumber = vm.ShowerAllowedDocumentNumber;
                statement.ShowerBossLastName = vm.ShowerBossLastName;
                statement.ShowerBossPosition = vm.ShowerBossPosition;
                statement.ShowerOrganizationForm = vm.ShowerOrganizationForm;
                statement.ShowerNameKz = vm.ShowerNameKz;
                statement.ShowerDateOfIssue = vm.ShowerDateOfIssue;
                statement.ShowerBossFirstName = vm.ShowerBossFirstName;
                statement.ShowerPhone = vm.ShowerPhone;
                statement.ShowerCountry = vm.ShowerCountry;
                statement.ShowerNameEn = vm.ShowerNameEn;
                statement.ShowerExpirationDate = vm.ShowerExpirationDate;
                statement.ShowerBossMiddleName = vm.ShowerBossMiddleName;
                statement.ShowerEmail = vm.ShowerEmail;
                statement.ShowerContactPersonInitials = vm.ShowerContactPersonInitials;
                statement.ShowerContactPersonPosition = vm.ShowerContactPersonPosition;
                statement.ShowerContactPersonFactAddress = vm.ShowerContactPersonFactAddress;
                statement.ShowerContactPersonActualAddress = vm.ShowerContactPersonActualAddress;

            if (vm.ChangeData != null)
            {
                var changes = _ctx.EMP_StatementChange.Where(x => x.StatementId == statement.Id).ToList();
                foreach (var changeVm in vm.ChangeData)
                {
                    var change = changes.FirstOrDefault(x => x.Id == changeVm.Id);
                    if (change == null)
                    {
                        change = new EMP_StatementChange { Id = Guid.NewGuid(), StatementId = statement.Id };
                        _ctx.EMP_StatementChange.Add(change);
                    }
                    change.Name = changeVm.Name;
                    change.Type = changeVm.Type;
                    change.BeforeChange = changeVm.BeforeChange;
                    change.AfterChange = changeVm.AfterChange;
                }
            }

            if (vm.StorageLifeData != null)
            {
                var storageLifes = _ctx.EMP_StatementStorageLife.Where(x => x.StatementId == statement.Id).ToList();
                foreach (var storageLifeVm in vm.StorageLifeData)
                {
                    var storageLife = storageLifes.FirstOrDefault(x => x.Id == storageLifeVm.Id);
                    if (storageLife == null)
                    {
                        storageLife = new EMP_StatementStorageLife { Id = Guid.NewGuid(), StatementId = statement.Id };
                        _ctx.EMP_StatementStorageLife.Add(storageLife);
                    }
                    storageLife.Kind = storageLifeVm.Kind;
                    if (storageLifeVm.ExpirationDate != null)
                        storageLife.ExpirationDate = storageLifeVm.ExpirationDate;
                    storageLife.Measure = storageLifeVm.Measure;
                    storageLife.IsIndefinitely = storageLifeVm.IsIndefinitely;
                }
            }

            if (vm.CountryRegistrationData != null)
            {
                var countryRegistrations = _ctx.EMP_StatementCountryRegistration.Where(x => x.StatementId == statement.Id).ToList();
                foreach (var countryRegistrationVm in vm.CountryRegistrationData)
                {
                    var countryRegistration = countryRegistrations.FirstOrDefault(x => x.Id == countryRegistrationVm.Id);
                    if (countryRegistration == null)
                    {
                        countryRegistration = new EMP_StatementCountryRegistration { Id = Guid.NewGuid(), StatementId = statement.Id };
                        _ctx.EMP_StatementCountryRegistration.Add(countryRegistration);
                    }
                    countryRegistration.Country = countryRegistrationVm.Country;
                    countryRegistration.RegistrationNumber = countryRegistrationVm.RegistrationNumber;
                    if (countryRegistrationVm.StartDate != null)
                        countryRegistration.StartDate = countryRegistrationVm.StartDate;
                    if (countryRegistrationVm.ExpirationDate != null)
                        countryRegistration.ExpirationDate = countryRegistrationVm.ExpirationDate;
                    countryRegistration.IsIndefinitely = countryRegistrationVm.IsIndefinitely;
                }
            }

            if (vm.MedicalDeviceComplectationData != null)
            {
                var complectations = _ctx.EMP_StatementMedicalDeviceComplectation.Where(x => x.StatementId == statement.Id).ToList();
                foreach (var complectationVm in vm.MedicalDeviceComplectationData)
                {
                    var complectation = complectations.FirstOrDefault(x => x.Id == complectationVm.Id);
                    if (complectation == null)
                    {
                        complectation = new EMP_StatementMedicalDeviceComplectation { Id = Guid.NewGuid(), StatementId = statement.Id };
                        _ctx.EMP_StatementMedicalDeviceComplectation.Add(complectation);
                    }
                    complectation.Type = complectationVm.Type;
                    complectation.Name = complectationVm.Name;
                    complectation.Identifier = complectationVm.Identifier;
                    complectation.Model = complectationVm.Model;
                    complectation.Manufacturer = complectationVm.Manufacturer;
                    complectation.Country = complectationVm.Country;
                }
            }

            if (vm.MedicalDevicePackageData != null)
            {
                var packages = _ctx.EMP_StatementMedicalDevicePackage.Where(x => x.StatementId == statement.Id).ToList();
                foreach (var packageVm in vm.MedicalDevicePackageData)
                {
                    var package = packages.FirstOrDefault(x => x.Id == packageVm.Id);
                    if (package == null)
                    {
                        package = new EMP_StatementMedicalDevicePackage { Id = Guid.NewGuid(), StatementId = statement.Id };
                        _ctx.EMP_StatementMedicalDevicePackage.Add(package);
                    }
                    package.Kind = packageVm.Kind;
                    package.Name = packageVm.Name;
                    package.VolumeValue = packageVm.VolumeValue;
                    package.VolumeUnit = packageVm.VolumeUnit;
                    package.Count = packageVm.Count;
                    package.Description = packageVm.Description;
                }
            }

            //if (vm.Samples != null)
            //{
            //    statement.EMP_StatementSamples = vm.Samples.Select(s =>
            //    {
            //        var res = new EMP_StatementSamples
            //        {
            //            Addition = s.Addition,
            //            Conditions = s.Conditions,
            //            Count = s.Count,
            //            CreateDate = s.CreateDate,
            //            ExpirationDate = s.ExpirationDate,
            //            Name = s.Name,
            //            SampleType = s.SampleType,
            //            SeriesPart = s.SeriesPart,
            //            StatementId = vm.Id,
            //            Storage = s.Storage,
            //            Unit = s.Unit
            //        };
            //        if (s.Id != 0) res.Id = s.Id;
            //        return res;
            //    }).ToList();
            //}

            if (vm.RegCountries != null && vm.RegCountries.Count() > 0)
            {
                vm.RegCountries.ToList().ForEach(rc =>
                {
                    var current = _ctx.EMP_EAESStatementRegCountry.FirstOrDefault(c => c.Id == rc.Id);
                    if (current != null)
                    {
                        current.IsIndefinitely = rc.IsIndefinitely;
                        current.RegNumber = rc.RegNumber;
                        current.StatementId = statement.Id;
                        current.Country = rc.Country;
                        current.DateOfIssue = rc.DateOfIssue ?? current.DateOfIssue;
                        current.ExpDate = rc.ExpDate ?? current.ExpDate;
                    }
                    else
                    {
                        var nc = new EMP_EAESStatementRegCountry
                        {
                            Country = rc.Country,
                            DateOfIssue = rc.DateOfIssue,
                            ExpDate = rc.ExpDate,
                            IsIndefinitely = rc.IsIndefinitely,
                            RegNumber = rc.RegNumber,
                            StatementId = statement.Id
                        };
                        if (nc.DateOfIssue == null)
                            Console.Write("cheat");
                        _ctx.EMP_EAESStatementRegCountry.Add(nc);
                    }
                });
            }

            _ctx.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Dictionaries(string type, string search)
        {
            return Json(_ctx.Dictionaries
                .Where(x => x.Type == type)
                .Select(x => x.Name), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DicNMIRK(string search, int id = 0)
        {
            var res = _ctx.EXP_DIC_NMIRK
                .Where(x =>
                    x.Code.ToString().Contains(search)
                    || x.NameRu.Contains(search)
                    || x.NameKk.Contains(search)
                    || x.Id == id)
                .Take(50)
                .Select(x => new
                {
                    x.Id,
                    x.NameRu,
                    x.NameKk,
                    x.DescriptionRu,
                    x.Descriptionkk,
                    x.Code
                });
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }

    public class EmpEaesStatementSampleVm
    {
        public int Id { get; set; }
        public string SampleType { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Unit { get; set; }
        public string SeriesPart { get; set; }
        public string Storage { get; set; }
        public string Conditions { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? Addition { get; set; }
    }

    public class EmpEaesStatementMedicalDevicePackageViewModel
    {
        public Guid Id { get; set; }
        public string Kind { get; set; }
        public string Name { get; set; }
        public string VolumeValue { get; set; }
        public string VolumeUnit { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }

    public class EmpEaesStatementMedicalDeviceComplectationViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
    }

    public class EmpEaesStatementCountryRegistrationViewModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsIndefinitely { get; set; }
    }

    public class EmpEaesStatementStorageLifeViewModel
    {
        public Guid Id { get; set; }
        public string Kind { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Measure { get; set; }
        public bool IsIndefinitely { get; set; }
    }

    public class EmpEaesStatementChangeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string BeforeChange { get; set; }
        public string AfterChange { get; set; }
    }

    public class EmpEaesStatementRegCountry
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string RegNumber { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public DateTime? ExpDate { get; set; }
        public bool? IsIndefinitely { get; set; }
    }

    public class EmpEaesStatementViewModel
    {
        public List<SelectListItem> RegistrationKind { get; set; }
        public string RegistrationCertificateNumber { get; set; }
        public string NormativeDocumentNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public List<EmpEaesStatementChangeViewModel> ChangeData { get; set; }
        public List<SelectListItem> CortractList { get; set; }
        public string LetterNumber { get; set; }
        public DateTime? LetterDate { get; set; }
        public bool IsMt { get; set; }
        public string MedicalDeviceNameKz { get; set; }
        public string MedicalDeviceNameRu { get; set; }
        public string NomenclatureCode { get; set; }
        public string NomenclatureNameKz { get; set; }
        public string NomenclatureNameRu { get; set; }
        public string NomenclatureDescriptionKz { get; set; }
        public string NomenclatureDescriptionRu { get; set; }
        public int? NmirkId { get; set; }
        public List<string> ApplicationAreaKz { get; set; }
        public List<string> ApplicationAreaRu { get; set; }
        public string PurposeKz { get; set; }
        public string PurposeRu { get; set; }
        public bool IsClosedSystem { get; set; }
        public string RegistrationDossierPageNumber { get; set; }
        public string ShortTechnicalCharacteristicKz { get; set; }
        public string ShortTechnicalCharacteristicRu { get; set; }
        public string ClassOfPotentialRisk { get; set; }
        public bool IsBalk { get; set; }
        public bool IsMeasurementDevice { get; set; }
        public bool IsForInvitroDiagnostics { get; set; }
        public bool IsSterile { get; set; }
        public bool IsMedicalProductPresence { get; set; }
        public bool WithouAe { get; set; }
        public List<EmpEaesStatementStorageLifeViewModel> StorageLifeData { get; set; }
        public string TransportConditions { get; set; }
        public string StorageConditions { get; set; }
        public List<EmpEaesStatementCountryRegistrationViewModel> CountryRegistrationData { get; set; }
        public string Production { get; set; }
        public List<EmpEaesStatementMedicalDeviceComplectationViewModel> MedicalDeviceComplectationData { get; set; }
        public List<EmpEaesStatementMedicalDevicePackageViewModel> MedicalDevicePackageData { get; set; }
        public bool IsComplectation { get; set; }
        public string ManufacturerType { get; set; }
        public string ManufacturerNameRu { get; set; }
        public string AllowedDocumentNumber { get; set; }
        public string BossLastName { get; set; }
        public string BossPosition { get; set; }
        public string OrganizationForm { get; set; }
        public string ManufacturerNameKz { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public string BossFirstName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string ManufacturerNameEn { get; set; }
        public DateTime? ManufacturerExpirationDate { get; set; }
        public string BossMiddleName { get; set; }
        public string Email { get; set; }
        public string ContactPersonInitials { get; set; }
        public string ContactPersonLegalAddress { get; set; }
        public string ContactPersonPosition { get; set; }
        public string ContactPersonFactAddress { get; set; }
        public string Agreement { get; set; }
        public bool IsAgreed { get; set; }
        public string RegistrationKindValue { get; set; }
        public Guid Id { get; set; }
        public Guid? ContractId { get; set; }
        public IEnumerable<EmpEaesStatementSampleVm> Samples { get; set; }
        public string RefCountry { get; set; }
        public string ConCountry { get; set; }
        public int? GarantExpDate { get; set; }
        public string GarantUnit { get; set; }
        public bool? GarantNoExp { get; set; }
        public IEnumerable<EmpEaesStatementRegCountry> RegCountries { get; set; }
        public string PlaceType { get; set; }
        public string PlaceNameRu { get; set; }
        public string PlaceAllowedDocumentNumber { get; set; }
        public string PlaceBossLastName { get; set; }
        public string PlaceBossPosition { get; set; }
        public string PlaceOrganizationForm { get; set; }
        public string PlaceNameKz { get; set; }
        public DateTime? PlaceDateOfIssue { get; set; }
        public string PlaceBossFirstName { get; set; }
        public string PlacePhone { get; set; }
        public string PlaceCountry { get; set; }
        public string PlaceNameEn { get; set; }
        public DateTime? PlaceExpirationDate { get; set; }
        public string PlaceBossMiddleName { get; set; }
        public string PlaceEmail { get; set; }
        public string PlaceContactPersonInitials { get; set; }
        public string PlaceContactPersonPosition { get; set; }
        public string PlaceContactPersonFactAddress { get; set; }
        public string ShowerType { get; set; }
        public string ShowerNameRu { get; set; }
        public string ShowerAllowedDocumentNumber { get; set; }
        public string ShowerBossLastName { get; set; }
        public string ShowerBossPosition { get; set; }
        public string ShowerOrganizationForm { get; set; }
        public string ShowerNameKz { get; set; }
        public DateTime? ShowerDateOfIssue { get; set; }
        public string ShowerBossFirstName { get; set; }
        public string ShowerPhone { get; set; }
        public string ShowerCountry { get; set; }
        public string ShowerNameEn { get; set; }
        public DateTime? ShowerExpirationDate { get; set; }
        public string ShowerBossMiddleName { get; set; }
        public string ShowerEmail { get; set; }
        public string ShowerContactPersonInitials { get; set; }
        public string ShowerContactPersonPosition { get; set; }
        public string ShowerContactPersonFactAddress { get; set; }
        public string ShowerContactPersonActualAddress { get; set; }
    }
}