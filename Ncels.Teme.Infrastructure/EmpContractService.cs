using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;

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
            var q = _uow.GetQueryable<EMP_ContractStage>()
                .Where(stage => stage.EMP_Contract.ContractType != null
                                && stage.EMP_Contract.ManufacturId != null && stage.EMP_Contract.ManufacturContactId != null
                                && stage.EMP_Contract.DeclarantId != null && stage.EMP_Contract.DeclarantContactId != null
                                && stage.EMP_Contract.PayerId != null && stage.EMP_Contract.PayerContactId != null)
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
                    ContractStageId = stage.Id
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

        private EmpContractDeclarantViewModel GetDeclarant(OBK_Declarant declarant, OBK_DeclarantContact declarantContact)
        {
            var boolValues = new List<SelectListItem>
            {
                new SelectListItem {Selected = false, Text = "Нет", Value = false.ToString()},
                new SelectListItem {Selected = false, Text = "Да", Value = true.ToString()}
            };
            //var selecteed = boolValues.FirstOrDefault(x=>x.Value==declarantContact.is)

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

                //BoolValues = 
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

        #region ТУТ НАЧИНАЕТСЯ АЦЦКИЙ КОПИПАСТ

        public void SendToWork(Guid stageId, Guid executorId)
        {
            var stage = _uow.GetQueryable<EMP_ContractStage>().First(x => x.Id == stageId);
            var executor = _uow.GetQueryable<Employee>().First(x => x.Id == executorId);

            var stageExecutor = new EMP_ContractStageExecutors()
            {
                ContractStageId = stage.Id,
                ExecutorId = executor.Id,
                ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
            };

            _uow.Insert(stageExecutor);

            var stageStatus = _uow.GetQueryable<EMP_Ref_StageStatus>().First(x => x.Code == CodeConstManager.EmpContractStageStatus.InWork);

            stage.StageStatusId = stageStatus.Id;


            var contract = _uow.GetQueryable<EMP_Contract>().First(x => x.Id == stage.ContractId);

            // Записывать историю если статус В Работу проставляется впервые, когда его назначают на первого исполнителя
            if (contract.Status != CodeConstManager.STATUS_OBK_WORK)
            {
                AddExtHistoryWork(contract.Id);
            }

            contract.Status = CodeConstManager.STATUS_OBK_WORK;
            contract.ContractStatusId = _uow.GetQueryable<EMP_Ref_Status>()
                .Where(x => x.Code == CodeConstManager.EmpContractStatus.InWork).Select(x => x.Id).FirstOrDefault();

            AddHistorySentToWork(contract.Id);

            _uow.Save();

            SendNotificationToExecutorWork(executorId, contract);
        }

        public IEnumerable<EmpContractHistoryViewModel> GetContractHistory(Guid id)
        {
            return _uow.GetQueryable<EMP_ContractHistoryView>().Where(x => x.ContractId == id)
                .Select(x=>new EmpContractHistoryViewModel
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

        private void AddExtHistoryWork(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractExtHistoryStatus.Work;
            AddExtHistory(contractId, historyStatusCode);
        }

        private void AddExtHistory(Guid contractId, string historyStatusCode, Guid? employeeId = null)
        {
            var status = GetContractExtHistoryStatusByCode(historyStatusCode);

            var history = new EMP_ContractExtHistory()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                ContractId = contractId,
                StatusId = status.Id,
                EmployeeId = employeeId
            };
            _uow.Insert(history);
        }

        private void AddHistorySentToWork(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.SentToWork;
            AddHistory(contractId, historyStatusCode);
        }

        private OBK_Ref_ContractExtHistoryStatus GetContractExtHistoryStatusByCode(string code)
        {
            return _uow.GetQueryable<OBK_Ref_ContractExtHistoryStatus>().Where(x => x.Code == code).FirstOrDefault();
        }

        private void AddHistory(Guid contractId, string historyStatusCode, string reason = null)
        {
            var currentEmployee = UserHelper.GetCurrentEmployee();

            var status = GetContractHistoryStatusByCode(historyStatusCode);

            var unitName = GetParentUnitName(currentEmployee);

            var history = new EMP_ContractHistory()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                RefuseReason = reason,
                ContractId = contractId,
                EmployeeId = currentEmployee.Id,
                UnitName = unitName,
                StatusId = status.Id,
            };
            _uow.Insert(history);
        }

        private string GetParentUnitName(Employee employee)
        {
            string unitName = null;
            if (employee.Units != null && employee.Units.Count > 0)
            {
                foreach (var unit in employee.Units)
                {
                    if (unit.Parent != null)
                    {
                        unitName = unit.Parent.ShortName;
                    }
                    break;
                }
            }
            return unitName;
        }

        public OBK_Ref_ContractHistoryStatus GetContractHistoryStatusByCode(string code)
        {
            return _uow.GetQueryable<OBK_Ref_ContractHistoryStatus>().Where(x => x.Code == code).FirstOrDefault();
        }

        private void SendNotificationToExecutorWork(Guid executorId, EMP_Contract contract)
        {
            string organizationForm = "";
            if (contract.OBK_Declarant.OrganizationFormId != null)
            {
                var orgForm = _uow.GetQueryable<Dictionary>().Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId.Value).FirstOrDefault();
                organizationForm = orgForm.Name;
            }
            var orgName = contract.OBK_Declarant?.NameRu;
            var message = string.Format("Поступил новый договор от {0} \"{1}\"", organizationForm, orgName);
            new NotificationManager().SendNotification(message, ObjectType.EmpContract, contract.Id, executorId);
        }

        #endregion
    }
}
