using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using Ncels.Teme.Infrastructure.ContractStage;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.EMP;

namespace Ncels.Teme.Infrastructure
{
    public class EmpContractService : IEmpContractService
    {
        private IUnitOfWork _uow;

        private const string Manufactur = "Производитель";
        private const string Declarant = "Заявитель";
        private const string Payer = "Третье лицо";

        private const string ManufacturCode = "Manufactur";
        private const string DeclarantCode = "Declarant";
        private const string PayerCode = "Payer";

        private const string Root = "Attachments";
        private const string AttachPath = "AttachPath";

        public EmpContractService()
        {
            // TODO - через DI
            _uow = new UnitOfWork();
        }

        public IQueryable<EmpContractViewModel> GetContracts()
        {
            var empl = UserHelper.GetCurrentEmployee();
            string stageCode = GetStageCode();

            var q = _uow.GetQueryable<EMP_ContractStage>()
                .Where(stage => stage.EMP_Contract.ContractType != null
                                && stage.EMP_Contract.ManufacturId != null && stage.EMP_Contract.ManufacturContactId != null
                                && stage.EMP_Contract.DeclarantId != null && stage.EMP_Contract.DeclarantContactId != null
                                && stage.EMP_Contract.PayerId != null && stage.EMP_Contract.PayerContactId != null)
                .Where(x => x.EMP_ContractStageExecutors.Any(e => e.ExecutorId == empl.Id))
                .Where(x => x.EMP_Ref_Stage.Code == stageCode)
                .ToArray()
                .GroupBy(stage => stage.ContractId)
                .Select(x => x.OrderBy(stage => stage.DateCreate).Last())
                .Select(stage => new EmpContractViewModel
                {
                    Id = stage.EMP_Contract.Id,
                    Number = stage.EMP_Contract.Number,
                    CreateDate = stage.EMP_Contract.CreatedDate,
                    StartDate = stage.EMP_Contract.StartDate,
                    EndDate = stage.EMP_Contract.EndDate,
                    Declarant = stage.EMP_Contract.OBK_Declarant.NameRu,
                    ContractType = stage.EMP_Contract.EMP_Ref_ContractType.NameRu,
                    StageStatusCode = stage.EMP_Ref_StageStatus.Code,
                    ContractStageId = stage.Id,
                    ContractStatusId = stage.EMP_Contract.EMP_DirectionToPayments.Select(x => x.OBK_Ref_PaymentStatus.Code).FirstOrDefault()
                });

            return q.AsQueryable();
        }

        public EmpContractDetailsViewModel GetContractDetailsViewModel(Guid contractId)
        {
            var contract = _uow.GetQueryable<EMP_Contract>().FirstOrDefault(x => x.Id == contractId);
            if (contract == null) return new EmpContractDetailsViewModel();

            var manufacturer = GetDeclarant(contract.OBK_DeclarantManufactur, contract.OBK_DeclarantContactManufactur);
            manufacturer.Title = Manufactur;
            manufacturer.Code = ManufacturCode;

            var declarant = GetDeclarant(contract.OBK_Declarant, contract.OBK_DeclarantContact);
            declarant.Title = Declarant;
            declarant.Code = DeclarantCode;
            declarant.HasProxy = contract.HasProxy == true ? "Да" : "Нет";
            declarant.DocumentType = contract.DocumentType == 1 ? "Представительство" : contract.DocumentType == 2 ? "Доверенное лицо" : string.Empty;

            var payer = GetDeclarant(contract.OBK_DeclarantPayer, contract.OBK_DeclarantContactPayer);
            payer.Code = PayerCode;
            var payers = new List<SelectListItem>
            {
                new SelectListItem {Value = ManufacturCode, Text = Manufactur},
                new SelectListItem {Value = DeclarantCode, Text = Declarant},
                new SelectListItem {Value = PayerCode, Text = Payer}
            };
            var selectPayer = payers.FirstOrDefault(x => x.Value == contract.ChoosePayer);
            if (selectPayer != null) selectPayer.Selected = true;

            return new EmpContractDetailsViewModel
            {
                Id = contract.Id,
                Manufacturer = manufacturer,
                Declarant = declarant,
                Payer = payer,
                Payers = payers,
                ChoosPayer = contract.ChoosePayer,
                MedicalDeviceName = contract.MedicalDeviceName,
                WorkCosts = contract.EMP_CostWorks.Select(x => new EmpContractWorkCostViewModel
                {
                    WorkName = x.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameRu,
                    Price = x.Price ?? 0,
                    Count = x.Count ?? 0
                }).ToList(),
                Attachments = GetContractAttachments(contractId)
            };
        }

        public IEnumerable<EmpContractPaymentViewModel> GetPayments()
        {
            var query = _uow.GetQueryable<EMP_DirectionToPayments>()
                .Select(x => new EmpContractPaymentViewModel
                {
                    Id = x.Id,
                    ContractId = x.ContractId,
                    TotalPrice = x.TotalPrice ?? 0,
                    CreateDate = x.CreateDate,
                    ContractNumber = x.EMP_Contract.Number,
                    ExecutorName = x.Employee.DisplayName,
                    PayerValue = x.OBK_Declarant.NameRu,
                    StatusCode = x.OBK_Ref_PaymentStatus.Code
                });
            return query.ToList();
        }

        public EmpContractPaymentDetailsViewModel GetPayment(Guid contractId)
        {
            var contract = _uow.GetQueryable<EMP_Contract>().First(x => x.Id == contractId);
            var vm = new EmpContractPaymentDetailsViewModel
            {
                InvoiceNumber1C = contract.EMP_DirectionToPayments.FirstOrDefault(e => e.ContractId == contract.Id && e.IsDeleted == false)?.InvoiceNumber1C,
                InvoiceDate1C = contract.EMP_DirectionToPayments.FirstOrDefault(e => e.ContractId == contract.Id && e.IsDeleted == false)?.InvoiceDate1C,
                ContractId = contract.Id,
                Contract = contract.Number + (contract.StartDate != null ? " от " + contract.StartDate : "") + " " + contract.EMP_Ref_ContractType.NameRu,
                Provider = "БИН/ИИН " + contract.OBK_DeclarantManufactur.Bin + " " + contract.OBK_DeclarantManufactur.NameRu + " " + contract.OBK_DeclarantContactManufactur.AddressLegalRu,
                Buyer = "БИН/ИИН " + contract.OBK_Declarant.Bin + " " + contract.OBK_Declarant.NameRu + " " + contract.OBK_DeclarantContact.AddressLegalRu
            };
            return vm;
        }

        public IEnumerable<EmpContractPaymentPriceViewModel> GetContractPrice(Guid contractId)
        {
            var contract = _uow.GetQueryable<EMP_Contract>().First(x => x.Id == contractId);
            int i = 1;
            return contract.EMP_CostWorks.Select(x => new EmpContractPaymentPriceViewModel
            {
                Line = i++,
                PriceWithTax = Math.Round(TaxHelper.GetCalculationTax(x.Price ?? 0), 2),
                //Price = Math.Round((decimal?)x.Count ?? 0 * TaxHelper.GetCalculationTax(x.Price ?? 0), 2),
                Count = x.Count ?? 0,
                ServiceName = x.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameRu,
                ProductName = contract.MedicalDeviceName
            });
        }

        private EmpContractDeclarantViewModel GetDeclarant(OBK_Declarant declarant, OBK_DeclarantContact declarantContact)
        {
            return new EmpContractDeclarantViewModel
            {
                IsResident = declarant.IsResident,
                NameKz = declarant.NameKz,
                NameRu = declarant.NameRu,
                NameEn = declarant.NameEn,
                Countries = GetDictionaryList("Country", declarant.CountryId),
                Bin = declarant.Bin,
                OrganizationForms = GetDictionaryList("OpfType", declarant.OrganizationFormId),
                NonResidentsNames = GetNonResidentNameList(declarant.CountryId),
                BossLastName = declarantContact.BossLastName,
                BossFirstName = declarantContact.BossFirstName,
                BossMiddleName = declarantContact.BossMiddleName,
                BossPositionRu = declarantContact.BossPosition,
                BossPositionKz = declarantContact.BossPositionKz,
                AddressLegal = declarantContact.AddressLegalRu,
                AddressFact = declarantContact.AddressFact,
                Phone = declarantContact.Phone,
                Email = declarantContact.Email,
                BankName = declarantContact.BankNameRu,
                BankIik = declarantContact.BankIik,
                Currencies = GetDictionaryList("Currency", declarantContact.CurrencyId),
                BankBik = declarantContact.BankBik,
                Iin = declarant.Iin,
                BankAccount = declarantContact.BankAccount,
                IsHasBossDocNumber = declarantContact.IsHasBossDocNumber ? "Да" : "Нет",
                BossDocNumber = declarantContact.BossDocNumber,
                BossDocUnlimited = declarantContact.BossDocUnlimited ? "Да" : "Нет",
                BossDosCreateDate = declarantContact.BossDocCreatedDate != null ? declarantContact.BossDocCreatedDate.Value.ToString("dd-MM-yyyy") : string.Empty,
                BossDocEndDate = declarantContact.BossDocEndDate != null ? declarantContact.BossDocEndDate.Value.ToString("dd-MM-yyyy") : string.Empty
            };
        }

        public IEnumerable<SelectListItem> GetSignerList(EMP_Contract contract)
        {
            string[] signerCodes = { "ncels_deputyceo", "ncels_ceo" };
            return _uow.GetQueryable<Employee>().Where(e => signerCodes.Contains(e.Position.Code)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Position.ShortName + " " + x.ShortName,
                Selected = x.Id == contract.Signer
            });
        }

        private IEnumerable<SelectListItem> GetNonResidentNameList(Guid? countryId)
        {
            return countryId != null
                ? _uow.GetQueryable<OBK_Declarant>()
                    .Where(x => x.CountryId == countryId && x.IsConfirmed && !x.IsResident)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.NameRu,
                        Selected = x.CountryId == countryId
                    }).ToList()
                : new List<SelectListItem> { new SelectListItem { Value = Guid.Empty.ToString(), Text = "Нет данных", Selected = true } };
        }

        private IEnumerable<SelectListItem> GetDictionaryList(string type, Guid? selectedItem)
        {
            return _uow.GetQueryable<Dictionary>().Where(x => x.Type == type)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = x.Id == selectedItem
                });
        }

        private IEnumerable<EmpContractFileAttachmentViewModel> GetContractAttachments(Guid contractId)
        {
            var doc = contractId.ToString();
            var list = new List<EmpContractFileAttachmentViewModel>();

            var info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings[AttachPath], Root, doc));
            if (!info.Exists) info.Create();

            var codes = new[] { EmpCodeConsts.ATTACH_CONTRACT_FILE, EmpCodeConsts.ATTACH_CONTRACT_DEGREERISK_FILE };
            var dicListQuery = _uow.GetQueryable<Dictionary>().Where(o => codes.Contains(o.Type));

            var markList = _uow.GetQueryable<FileLinksCategoryCom>().Where(e => e.DocumentId == contractId).ToList();
            var fileMetadatas = _uow.GetQueryable<FileLink>().Where(e => e.DocumentId == contractId).ToList();

            foreach (var dictionary in dicListQuery)
            {
                var fileLinksCategoryCom = markList.FirstOrDefault(e => e.CategoryId == dictionary.Id);
                var group = new EmpContractFileAttachmentViewModel();
                group.Id = dictionary.Id;
                group.Code = dictionary.Code;
                group.Name = dictionary.Name;
                group.MarkClassName = markList.FirstOrDefault(e => e.CategoryId == dictionary.Id) == null
                    ? "control-default"
                    : fileLinksCategoryCom != null && fileLinksCategoryCom.IsError
                        ? "control-error"
                        : "control-good";

                group.IsNotApplicable = markList.FirstOrDefault(e => e.CategoryId == dictionary.Id) != null &&
                                        fileLinksCategoryCom?.IsNotApplicable != null &&
                                        fileLinksCategoryCom.IsNotApplicable.Value;

                var dirInfo = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings[AttachPath], Root, doc, dictionary.Id.ToString()));

                group.Items = dirInfo.Exists
                    ? dirInfo.GetFiles().Join(fileMetadatas, f => f.Name,
                        f => string.Format("{0}{1}", f.Id, Path.GetExtension(f.FileName)),
                        (f, fm) => new { File = f, FileMetadata = fm })
                    .ToList().Select(k => new EmpContractFileAttachmentItemViewModel
                    {
                        AttachId = string.Format("id={0}&path={1}&fileId={2}", dictionary.Id, doc,
                                string.Format("{0}{1}", k.FileMetadata.Id, Path.GetExtension(k.FileMetadata.FileName))),
                        AttachName = k.FileMetadata.FileName,
                        AttachSize = k.File.Length,
                        Version = k.FileMetadata.Version,
                        OriginFileId = k.FileMetadata.ParentId,
                        OwnerName = k.FileMetadata.OwnerName,
                        OwnerId = (Guid)k.FileMetadata.OwnerId,
                        CreateDate = k.FileMetadata.CreateDate.ToString(CultureInfo.InvariantCulture),
                        MetadataId = k.FileMetadata.Id,
                        Comment = k.FileMetadata.Comment,
                        StatusCode = k.FileMetadata.DIC_FileLinkStatus != null ? k.FileMetadata.DIC_FileLinkStatus.Code : string.Empty,
                        StatusName = k.FileMetadata.DIC_FileLinkStatus != null ? k.FileMetadata.DIC_FileLinkStatus.NameRu : string.Empty,
                        Language = k.FileMetadata.Language,
                        NumOfPages = k.FileMetadata.PageNumbers,
                        Stage = k.FileMetadata.EXP_DIC_Stage != null ? k.FileMetadata.EXP_DIC_Stage.NameRu : string.Empty
                    }).ToList()
                    : new List<EmpContractFileAttachmentItemViewModel>();


                list.Add(group);
            }
            return list;
        }

        public void SendToWork(Guid stageId, Guid executorId)
        {
            var stage = _uow.GetQueryable<EMP_ContractStage>().First(x => x.Id == stageId);
            var executor = _uow.GetQueryable<Employee>().First(x => x.Id == executorId);
            var contract = _uow.GetQueryable<EMP_Contract>().First(x => x.Id == stage.ContractId);
            var stageStatus = _uow.GetQueryable<EMP_Ref_StageStatus>().First(x => x.Code == CodeConstManager.EmpContractStageStatus.InWork);

            if (stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.NotDistributed)
                stage.StageStatusId = stageStatus.Id;

            contract.ContractStatusId = _uow.GetQueryable<EMP_Ref_Status>()
                .Where(x => x.Code == CodeConstManager.EmpContractStatus.InWork).Select(x => x.Id).FirstOrDefault();

            var stageExecutor = new EMP_ContractStageExecutors
            {
                Id = Guid.NewGuid(),
                ExecutorId = executor.Id,
                ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
            };

            if (stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Coz)
            {
                var validationGroupStage = contract.EMP_ContractStage.FirstOrDefault(x =>
                    x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.ValidationGroup);
                var defStage = contract.EMP_ContractStage.FirstOrDefault(x =>
                    x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Def);
                var isRejected = validationGroupStage != null && validationGroupStage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.NotApproved
                                  || defStage != null && defStage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.NotApproved;

                var legalStage = new EMP_ContractStage
                {
                    Id = Guid.NewGuid(),
                    ContractId = contract.Id,
                    DateCreate = DateTime.Now,
                    StageStatusId = isRejected
                        ? _uow.GetQueryable<EMP_Ref_StageStatus>()
                            .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.NotApproved)
                            .Select(x => x.Id).FirstOrDefault()
                        : stageStatus.Id,
                    StageId = _uow.GetQueryable<EMP_Ref_Stage>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStage.LegalDepartmant).Select(x => x.Id)
                        .FirstOrDefault()
                };
                _uow.Insert(legalStage);
                stageExecutor.EMP_ContractStage = legalStage;
            }
            else
            {
                stageExecutor.EMP_ContractStage = stage;
            }

            _uow.Insert(stageExecutor);

            new EmpContractStageHistoryHandler(_uow).AddHistorySentToWork(contract.Id);

            _uow.Save();

            SendNotificationToExecutorWork(executorId, contract);
        }

        public IEnumerable<EmpContractHistoryViewModel> GetContractHistory(Guid id)
        {
            return _uow.GetQueryable<EMP_ContractHistoryView>().Where(x => x.ContractId == id)
                .Select(x => new EmpContractHistoryViewModel
                {
                    Id = x.Id,
                    UnitName = x.UnitName,
                    ContractId = x.ContractId,
                    StatusCode = x.StatusCode,
                    EmployeeId = x.EmployeeId,
                    StatusId = x.StatusId,
                    Created = x.Created,
                    EmployeeFullName = x.EmployeeFullName,
                    EmployeeShortName = x.EmployeeShortName,
                    RefuseReason = x.RefuseReason,
                    StatusNameKz = x.StatusNameKz,
                    StatusNameRu = x.StatusNameRu
                });
        }

        private bool IsEmployeeInUnit(Employee employee, string unitCode)
        {
            var unit = employee.Position;
            while (unit != null && unit.Code != unitCode)
                unit = unit.Parent;
            return unit != null;
        }

        private bool IsEmployeeBossInUnit(Employee employee, string unitCode)
        {
            var unitBoss = Guid.Parse(_uow.GetQueryable<Unit>().Where(x => x.Code == unitCode).Select(x => x.BossId).FirstOrDefault());
            return unitBoss == employee.Id;
        }

        public void Approve(Guid stageId, bool result)
        {
            var stage = _uow.GetQueryable<EMP_ContractStage>().First(x => x.Id == stageId);
            var empl = UserHelper.GetCurrentEmployee();

            var canUserApprove = stage.EMP_ContractStageExecutors.Any(x => x.Employee.Id == empl.Id);
            if (!canUserApprove) return;

            var factory = new EmpContractStageProcessorFactory(_uow);
            var processor = factory.Create(stage.EMP_Ref_Stage.Code);
            processor.Handle(stage, result);
        }

        private void SendNotificationToExecutorWork(Guid executorId, EMP_Contract contract)
        {
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = _uow.GetQueryable<Dictionary>().FirstOrDefault(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value);
                if (orgForm != null) organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Поступил новый договор от {0} \"{1}\"", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.EmpContract, contract.Id, executorId);
        }

        private void SendNotificationToApplicantRegistered(EMP_Contract contract)
        {
            var message = string.Format("Договор зарегистрирован. № {0}, дата начала действия {1}. Необходимо произвести оплату работ по предоставленному счету на оплату",
                contract.Number, contract.StartDate?.ToString("dd.MM.yyyy"));
            new NotificationManager().SendNotification(message, ObjectType.ObkContract, contract.Id, contract.EmployeeId.Value);
        }

        public bool CanApprove(Guid stageId)
        {
            var empl = UserHelper.GetCurrentEmployee();
            var stage = _uow.GetQueryable<EMP_ContractStage>().First(x => x.Id == stageId);

            bool isStageExecutor = stage.EMP_ContractStageExecutors.Any(x => x.ExecutorId == empl.Id);
            if (!isStageExecutor) return false;

            bool canCozBossApprove = stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Coz
                                     && stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired
                                     && stage.EMP_Contract.EMP_ContractStage.Any(x => x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.LegalDepartmant);
            bool canLegalApprove = stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.LegalDepartmant
                                   && stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.InWork
                                   && stage.EMP_Contract.EMP_ContractHistory.Any(x => x.OBK_Ref_ContractHistoryStatus.Code == OBK_Ref_ContractHistoryStatus.Returned);
            bool canValidationGroupUserApprove = IsEmployeeInUnit(empl, "ValidationGroup") && !IsEmployeeBossInUnit(empl, "ValidationGroup")
                && stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.ValidationGroup
                && stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.InWork;
            var canValidationGroupBossApprove = IsEmployeeBossInUnit(empl, "ValidationGroup")
                                                && stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.ValidationGroup
                                                && stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired;
            bool canDefBossApprove = stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Def
                                     && stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.New;
            bool canCeoApprove = stage.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Ceo
                                 && stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired;

            return canCozBossApprove || canLegalApprove || canValidationGroupUserApprove ||
                   canValidationGroupBossApprove || canDefBossApprove || canCeoApprove;
        }

        public string GetStageCode()
        {
            var empl = UserHelper.GetCurrentEmployee();
            string stageCode = string.Empty;
            if (IsEmployeeBossInUnit(empl, "coz"))
                stageCode = CodeConstManager.EmpContractStage.Coz;
            else if (IsEmployeeInUnit(empl, "coz"))
                stageCode = CodeConstManager.EmpContractStage.LegalDepartmant;
            else if (IsEmployeeInUnit(empl, "ValidationGroup"))
                stageCode = CodeConstManager.EmpContractStage.ValidationGroup;
            else if (IsEmployeeInUnit(empl, "finance"))
                stageCode = CodeConstManager.EmpContractStage.Def;
            else if (IsEmployeeInUnit(empl, "01"))
                stageCode = CodeConstManager.EmpContractStage.Ceo;
            return stageCode;
        }

        public void SendToAdjustment(Guid stageId)
        {
            var stage = _uow.GetQueryable<EMP_ContractStage>().First(x => x.Id == stageId);
            stage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.OnAdjustment).Select(x => x.Id)
                .FirstOrDefault();
            stage.EMP_Contract.ContractStatusId = _uow.GetQueryable<EMP_Ref_Status>()
                .Where(x => x.Code == CodeConstManager.EmpContractStatus.OnAdjustment).Select(x => x.Id)
                .FirstOrDefault();
            new EmpContractStageHistoryHandler(_uow).AddHistoryReturnedToAdjustment(stage.ContractId);
            _uow.Save();
        }

        public string RegisterContract(Guid contractId)
        {
            var contract = _uow.GetQueryable<EMP_Contract>().First(x => x.Id == contractId);
            contract.Number = GetLastNumberOfContract();
            contract.StartDate = DateTime.Now;

            var digitalSign = _uow.GetQueryable<EMP_ContractSignData>().FirstOrDefault(x => x.ContractId == contractId);
            if (digitalSign != null)
            {
                contract.ContractStatusId = _uow.GetQueryable<EMP_Ref_Status>()
                    .Where(x => x.Code == CodeConstManager.EmpContractStatus.OnInvoiceOfPaymentFormation)
                    .Select(x => x.Id).FirstOrDefault();
                var stageStatus = _uow.GetQueryable<EMP_Ref_StageStatus>()
                    .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.Active).Select(x => x.Id)
                    .FirstOrDefault();
                foreach (var contractStage in contract.EMP_ContractStage)
                    contractStage.StageStatusId = stageStatus;
            }

            new EmpContractStageHistoryHandler(_uow).AddHistoryRegistered(contractId);
            _uow.Save();

            SendNotificationToApplicantRegistered(contract);

            SavePayments(contract);

            return contract.Number;
        }

        public object GetContractTemplatePdf(Guid contractId)
        {
            return new EMPContractRepository().GetContractReportData(contractId);
        }

        private void SavePayments(EMP_Contract contract)
        {
            var pay = new EMP_DirectionToPayments
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                ContractId = contract.Id,
                CreateEmployeeId = UserHelper.GetCurrentEmployee().Id,
                CreateEmployeeValues = UserHelper.GetCurrentEmployee().DisplayName,
                DirectionDate = DateTime.Now,
                Number = contract.Number,
                PayerId = contract.OBK_Declarant.Id,
                PayerValue = contract.OBK_Declarant.NameRu,
                IsDeleted = false,
                TotalPrice = contract.EMP_CostWorks.Sum(e =>
                    Math.Round(Convert.ToDecimal(TaxHelper.GetCalculationTax(e.Price.Value) * e.Count), 2)),
                StatusId = _uow.GetQueryable<OBK_Ref_PaymentStatus>()
                    .Where(e => e.Code == OBK_Ref_PaymentStatus.OnFormation).Select(x => x.Id).FirstOrDefault()
            };
            _uow.Insert(pay);
            _uow.Save();
        }

        private string GetLastNumberOfContract()
        {
            string number = "1";
            var numbers = _uow.GetQueryable<EMP_Contract>().Select(x => x.Number).ToList();
            int temp;
            int contractNumber = numbers.Select(n => int.TryParse(n, out temp) ? temp : 0).Max();
            contractNumber++;
            number = contractNumber.ToString();
            return number;
        }
    }
}
