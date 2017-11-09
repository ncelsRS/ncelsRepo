using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Words;
using Ncels.Helpers;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Notifications;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Ncels.Database.Repository.Contract
{
    public class ContractRepository : ARepository
    {
        public ContractRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public ContractRepository(ncelsEntities context) : base(context)
        {

        }

        public IQueryable<DataModel.Contract> GetContractsByStatuses(Guid ownerId, params string[] statusCodes)
        {
            return AppContext.Contracts.Where(e => e.OwnerId == ownerId && statusCodes.Contains(e.ContractStatus.Code));
        }

        public IQueryable<DataModel.Contract> EmployeeContracts(Guid ownerId)
        {
            return AppContext.Contracts.Where(e => e.OwnerId == ownerId);
        }

        public void SaveContract(RequestModel model)
        {
            var project = AppContext.Contracts.Any(o => o.Id == model.Contract.Id);
            Dictionary contractStatus;
            if (project)
            {
                contractStatus = model.Contract.ContractStatus;
                model.Contract.ManufacturerOrganizationId = model.Manufacture.Id;
                model.Contract.ManufacturerOrganization = model.Manufacture;
                AppContext.Entry(model.Manufacture).State = EntityState.Modified;

                var applicant = model.Applicant;
                if (model.Applicant.Id != (applicant = model.Manufacture).Id)
                {
                    applicant = model.Applicant;
                    AppContext.Entry(applicant).State = EntityState.Modified;
                }
                model.Contract.ApplicantOrganizationId = applicant.Id;
                model.Contract.ApplicantOrganization = applicant;

                var holder = model.Holder;
                if (model.Holder.Id != (holder = model.Manufacture).Id && model.Holder.Id != (holder = model.Applicant).Id)
                {
                    holder = model.Holder;
                    AppContext.Entry(holder).State = EntityState.Modified;
                }
                model.Contract.HolderOrganizationId = holder.Id;
                model.Contract.HolderOrganization = holder;

                var payer = model.Payer;
                if (model.Payer.Id != (payer = model.Manufacture).Id && model.Payer.Id != (payer = model.Applicant).Id &&
                    model.Payer.Id != (payer = model.Holder).Id)
                {
                    model.Contract.PayerOrganizationId = model.Payer.Id;
                    model.Contract.PayerOrganization = model.Payer;
                    AppContext.Entry(model.Payer).State = EntityState.Modified;
                }
                else
                {
                    model.Contract.PayerOrganizationId = payer.Id;
                    model.Contract.PayerOrganization = payer;
                }
                if (model.PayerTranslation.Id != (payer = model.Manufacture).Id &&
                    model.PayerTranslation.Id != (payer = model.Applicant).Id &&
                    model.PayerTranslation.Id != (payer = model.Holder).Id)
                {
                    model.Contract.PayerTranslationOrganizationId = model.PayerTranslation.Id;
                    model.Contract.PayerTranslationOrganization = model.PayerTranslation;
                    AppContext.Entry(model.PayerTranslation).State = EntityState.Modified;
                }
                else
                {
                    model.Contract.PayerTranslationOrganizationId = payer.Id;
                    model.Contract.PayerTranslationOrganization = payer;
                }
                AppContext.Entry(model.Contract).State = EntityState.Modified;
            }
            else
            {
                model.Contract.CreatedDate = DateTime.Now;
                model.Contract.ManufacturerOrganizationId = model.Manufacture.Id;
                model.Contract.ManufacturerOrganization = model.Manufacture;
                if (AppContext.Organizations.Any(o => o.Id == model.Manufacture.Id))
                {
                    AppContext.Entry(model.Manufacture).State = EntityState.Modified;
                }
                else
                {
                    AppContext.Organizations.Add(model.Manufacture);
                }
                var applicant = model.Applicant;
                if (model.Applicant.Id != (applicant = model.Manufacture).Id)
                {
                    applicant = model.Applicant;
                    if (AppContext.Organizations.Any(o => o.Id == applicant.Id))
                    {
                        AppContext.Entry(applicant).State = EntityState.Modified;
                    }
                    else
                    {
                        AppContext.Organizations.Add(applicant);
                    }
                }
                model.Contract.ApplicantOrganizationId = applicant.Id;
                model.Contract.ApplicantOrganization = applicant;

                var holder = model.Holder;
                if (model.Holder.Id != (holder = model.Manufacture).Id && model.Holder.Id != (holder = model.Applicant).Id)
                {
                    holder = model.Holder;
                    if (AppContext.Organizations.Any(o => o.Id == holder.Id))
                    {
                        AppContext.Entry(holder).State = EntityState.Modified;
                    }
                    else
                    {
                        AppContext.Organizations.Add(holder);
                    }
                }
                model.Contract.HolderOrganizationId = holder.Id;
                model.Contract.HolderOrganization = holder;

                var payer = model.Payer;
                if (model.Payer.Id != (payer = model.Manufacture).Id && model.Payer.Id != (payer = model.Applicant).Id &&
                    model.Payer.Id != (payer = model.Holder).Id)
                {
                    model.Contract.PayerOrganizationId = model.Payer.Id;
                    model.Contract.PayerOrganization = model.Payer;
                    if (AppContext.Organizations.Any(o => o.Id == model.Payer.Id))
                    {
                        AppContext.Entry(model.Payer).State = EntityState.Modified;
                    }
                    else
                    {
                        AppContext.Organizations.Add(model.Payer);
                    }
                }
                else
                {
                    model.Contract.PayerOrganizationId = payer.Id;
                    model.Contract.PayerOrganization = payer;
                }
                if (model.PayerTranslation.Id != (payer = model.Manufacture).Id &&
                    model.PayerTranslation.Id != (payer = model.Applicant).Id &&
                    model.PayerTranslation.Id != (payer = model.Holder).Id)
                {
                    model.Contract.PayerTranslationOrganizationId = model.PayerTranslation.Id;
                    model.Contract.PayerTranslationOrganization = model.PayerTranslation;
                    if (AppContext.Organizations.Any(o => o.Id == model.PayerTranslation.Id))
                    {
                        AppContext.Entry(model.PayerTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        AppContext.Organizations.Add(model.PayerTranslation);
                    }
                }
                else
                {
                    model.Contract.PayerTranslationOrganizationId = payer.Id;
                    model.Contract.PayerTranslationOrganization = payer;
                }
                contractStatus = DictionaryHelper.GetDicItemByCode(Dictionary.ContractStatus.DicCode,
                    Database.DataModel.Contract.StatusNew);
                model.Contract.StatusId = contractStatus.Id;
                AppContext.Contracts.Add(model.Contract);
            }
            if (model.Contract.DoverennostTypeDicId != null && model.Contract.DoverennostTypeDicId.Value != Guid.Empty)
            {
                var doverenostCode =
                    AppContext.Dictionaries.FirstOrDefault(e => e.Id == model.Contract.DoverennostTypeDicId).Code;
                model.Contract.StartDate = model.Contract.DoverennostCreatedDate;
                if (doverenostCode == Dictionary.DicDoverenostType.StatuteCode)
                {
                    model.Contract.EndDate = model.Contract.DoverennostCreatedDate != null
                        ? (DateTime?)model.Contract.DoverennostCreatedDate.Value.AddYears(3)
                        : null;
                }
                else
                {
                    if (model.Contract.StartDate != null && model.Contract.DoverennostExpiryDate != null)
                    {
                        int monthsApart = 12 *
                                          (model.Contract.StartDate.Value.Year -
                                           model.Contract.DoverennostExpiryDate.Value.Year) +
                                          model.Contract.StartDate.Value.Month -
                                          model.Contract.DoverennostExpiryDate.Value.Month;
                        if (Math.Abs(monthsApart) > 36)
                            model.Contract.EndDate = model.Contract.DoverennostCreatedDate != null
                                ? (DateTime?)model.Contract.DoverennostCreatedDate.Value.AddYears(3)
                                : null;
                        else
                            model.Contract.EndDate = model.Contract.DoverennostExpiryDate;

                    }
                }
            }
            AppContext.SaveChanges();
            model.Contract.ContractStatus = contractStatus;
        }

        public void SaveContractAddition(DataModel.Contract contractAddition)
        {
            var project = AppContext.Contracts.Any(o => o.Id == contractAddition.Id);
            Dictionary contractStatus;
            if (project)
            {
                contractStatus = contractAddition.ContractStatus;
                AppContext.Entry(contractAddition).State = EntityState.Modified;
            }
            else
            {
                contractAddition.CreatedDate = DateTime.Now;
                contractStatus = DictionaryHelper.GetDicItemByCode(Dictionary.ContractStatus.DicCode,
                    Database.DataModel.Contract.StatusNew);
                contractAddition.StatusId = contractStatus.Id;
                AppContext.Contracts.Add(contractAddition);
            }
            AppContext.SaveChanges();
            contractAddition.ContractStatus = contractStatus;
        }

        public Dictionary<string, string> GetContractTemplateData(Guid contractId)
        {
            var result = new Dictionary<string, string>();
            AppContext.Configuration.LazyLoadingEnabled = true;
            AppContext.Configuration.ProxyCreationEnabled = true;
            var data = AppContext.Contracts.Where(e => e.Id == contractId)
                .Select(e => new
                {
                    Contract = e,
                    e.ManufacturerOrganization,
                    e.ApplicantOrganization,
                    e.HolderOrganization,
                    e.PayerOrganization,
                    e.PayerTranslationOrganization,
                    e.Signer,
                    e.DoverennostType
                }).FirstOrDefault();
            if (data == null) return result;
            result.Add("Number", data.Contract.Number);
            result.Add("ContractDate",
                (data.Contract.ContractDate != null ? data.Contract.ContractDate.Value.ToString("dd.MM.yyyy") : null));

            if (data.ManufacturerOrganization != null)
            {
                result.Add("ProducerNameEn", data.ManufacturerOrganization.NameEn);
                result.Add("ProducerNameRu", data.ManufacturerOrganization.NameRu);
                result.Add("BossLastName", data.ManufacturerOrganization.BossLastName);
                result.Add("BossFirstName", data.ManufacturerOrganization.BossFirstName);
                result.Add("BossMiddleName", data.ManufacturerOrganization.BossMiddleName);
                result.Add("ProducerBossPosition", data.ManufacturerOrganization.BossPosition);
                if (data.ManufacturerOrganization.OpfTypeDicId != null &&
                    data.ManufacturerOrganization.OpfTypeDicId.Value != Guid.Empty)
                    result.Add("ProducerForm",
                        AppContext.Dictionaries.FirstOrDefault(e => e.Id == data.ManufacturerOrganization.OpfTypeDicId)
                            .Name);
                result.Add("ProducerLegalAddress", data.ManufacturerOrganization.AddressLegal);
                result.Add("ProducerFactAddress", data.ManufacturerOrganization.AddressFact);
                result.Add("ProducerPhone", data.ManufacturerOrganization.Phone);
                result.Add("ProducerEmail", data.ManufacturerOrganization.Email);
            }
            if (data.HolderOrganization != null)
            {
                result.Add("HolderNameEn", data.HolderOrganization.NameEn);
                result.Add("HolderNameRu", data.HolderOrganization.NameRu);
                result.Add("HolderBossLastName", data.HolderOrganization.BossLastName);
                result.Add("HolderBossFirstName", data.HolderOrganization.BossFirstName);
                result.Add("HolderBossMiddleName", data.HolderOrganization.BossMiddleName);
                result.Add("HolderBossPosition", data.HolderOrganization.BossPosition);
                if (data.HolderOrganization.OpfTypeDicId != null &&
                    data.HolderOrganization.OpfTypeDicId.Value != Guid.Empty)
                    result.Add("HolderForm",
                        AppContext.Dictionaries.FirstOrDefault(e => e.Id == data.HolderOrganization.OpfTypeDicId).Name);
                result.Add("HolderLegalAddress", data.HolderOrganization.AddressLegal);
                result.Add("HolderFactAddress", data.HolderOrganization.AddressFact);
                result.Add("HolderPhone", data.HolderOrganization.Phone);
                result.Add("HolderEmail", data.HolderOrganization.Email);
            }
            result.Add("SignerPosition", data.Signer != null ? data.Signer.Position.Name : null);
            result.Add("SignerInitials", data.Signer != null ? data.Signer.ShortName : null);
            result.Add("DoverennostType", data.DoverennostType != null ? data.DoverennostType.Name : null);
            result.Add("DoverennostNumber", data.Contract.DoverennostNumber);
            result.Add("DoverennostCreatedDate",
                data.Contract.DoverennostCreatedDate != null
                    ? data.Contract.DoverennostCreatedDate.Value.ToString("dd.MM.yyyy")
                    : null);
            if (data.ApplicantOrganization != null)
            {
                result.Add("ApplicantName", data.ApplicantOrganization.NameRu);
                if (data.ApplicantOrganization.OpfTypeDicId != null &&
                    data.ApplicantOrganization.OpfTypeDicId.Value != Guid.Empty)
                    result.Add("ApplicantForm",
                        AppContext.Dictionaries.FirstOrDefault(e => e.Id == data.ApplicantOrganization.OpfTypeDicId)
                            .Name);
                result.Add("ApplicantLastName", data.ApplicantOrganization.BossLastName);
                result.Add("ApplicantFirstName", data.ApplicantOrganization.BossFirstName);
                result.Add("ApplicantMiddleName", data.ApplicantOrganization.BossMiddleName);
                result.Add("ApplicantLegalAddress", data.ApplicantOrganization.AddressLegal);
                result.Add("ApplicantFactAddress", data.ApplicantOrganization.AddressFact);
                result.Add("ApplicantPhone", data.ApplicantOrganization.Phone);
                result.Add("ApplicantEmail", data.ApplicantOrganization.Email);
                result.Add("ApplicantBossPossition", data.ApplicantOrganization.BossPosition);
                result.Add("ApplicantBankName", data.ApplicantOrganization.BankName);
                result.Add("ApplicantPaymentBill", data.ApplicantOrganization.PaymentBill);
                if (data.ApplicantOrganization.BankCurencyDicId != null &&
                    data.ApplicantOrganization.BankCurencyDicId.Value != Guid.Empty)
                    result.Add("ApplicantBankCurency",
                        AppContext.Dictionaries.FirstOrDefault(e => e.Id == data.ApplicantOrganization.BankCurencyDicId)
                            .Name);
                result.Add("ApplicantSwift", string.Format("SWIFT: {0}", data.ApplicantOrganization.BankSwift));
                result.Add("ApplicantIin", string.Format("ИИН: {0}", data.ApplicantOrganization.Iin));
            }
            if (data.PayerOrganization != null)
            {
                result.Add("PayerName", data.PayerOrganization.NameRu);
                result.Add("PayerLegalAddress", data.PayerOrganization.AddressLegal);
                result.Add("PayerFactAddress", data.PayerOrganization.AddressFact);
                result.Add("PayerPhone", data.PayerOrganization.Phone);
                result.Add("PayerEmail", data.PayerOrganization.Email);
                result.Add("PayerLastName", data.PayerOrganization.BossLastName);
                result.Add("PayerFirstName", data.PayerOrganization.BossFirstName);
                result.Add("PayerMiddleName", data.PayerOrganization.BossMiddleName);
                result.Add("PayerBossPossition", data.PayerOrganization.BossPosition);
                string currency = null;
                if (data.PayerOrganization.BankCurencyDicId != null &&
                    data.PayerOrganization.BankCurencyDicId.Value != Guid.Empty)
                    currency =
                        AppContext.Dictionaries.FirstOrDefault(e => e.Id == data.PayerOrganization.BankCurencyDicId)
                            .Name;
                result.Add("PayerBankCurency", currency);
                result.Add("PayerBankName", data.PayerOrganization.BankName);
                if (data.PayerOrganization.IsResident)
                {
                    result.Add("PayerBankData1", data.PayerOrganization.BankIik);
                    result.Add("PayerBankData2", data.PayerOrganization.BankBik);
                    result.Add("PayerBankData3", data.PayerOrganization.Bin);
                    result.Add("PayerBankData4", data.PayerOrganization.Iin);
                }
                else
                {
                    result.Add("PayerBankData1", data.PayerOrganization.PaymentBill);
                    result.Add("PayerBankData2", currency);
                    result.Add("PayerBankData3", data.PayerOrganization.BankSwift);
                    result.Add("PayerBankData4", null);

                }
            }
            if (data.PayerTranslationOrganization != null)
            {
                result.Add("PayerTrName", data.PayerTranslationOrganization.NameRu);
                result.Add("PayerTrLegalAddress", data.PayerTranslationOrganization.AddressLegal);
                result.Add("PayerTrFactAddress", data.PayerTranslationOrganization.AddressFact);
                result.Add("PayerTrPhone", data.PayerTranslationOrganization.Phone);
                result.Add("PayerTrEmail", data.PayerTranslationOrganization.Email);
                result.Add("PayerTrLastName", data.PayerTranslationOrganization.BossLastName);
                result.Add("PayerTrFirstName", data.PayerTranslationOrganization.BossFirstName);
                result.Add("PayerTrMiddleName", data.PayerTranslationOrganization.BossMiddleName);
                result.Add("PayerTrBossPossition", data.PayerTranslationOrganization.BossPosition);
                string currency = null;
                if (data.PayerTranslationOrganization.BankCurencyDicId != null &&
                    data.PayerTranslationOrganization.BankCurencyDicId.Value != Guid.Empty)
                    currency =
                        AppContext.Dictionaries.FirstOrDefault(
                            e => e.Id == data.PayerTranslationOrganization.BankCurencyDicId).Name;
                result.Add("PayerTrBankCurency", currency);
                result.Add("PayerTrBankName", data.PayerTranslationOrganization.BankName);
                if (data.PayerTranslationOrganization.IsResident)
                {
                    result.Add("PayerTrBankData1", data.PayerTranslationOrganization.BankIik);
                    result.Add("PayerTrBankData2", data.PayerTranslationOrganization.BankBik);
                    result.Add("PayerTrBankData3", data.PayerTranslationOrganization.Bin);
                    result.Add("PayerTrBankData4", data.PayerTranslationOrganization.Iin);
                }
                else
                {
                    result.Add("PayerTrBankData1", data.PayerTranslationOrganization.PaymentBill);
                    result.Add("PayerTrBankData2", currency);
                    result.Add("PayerTrBankData3", data.PayerTranslationOrganization.BankSwift);
                    result.Add("PayerTrBankData4", null);

                }
            }
            result.Add("ContractStartDate",
                data.Contract.StartDate != null ? data.Contract.StartDate.Value.ToString("dd.MM.yyyy") : null);
            result.Add("ContractEndDate",
                data.Contract.EndDate != null ? data.Contract.EndDate.Value.ToString("dd.MM.yyyy") : null);

            return result;
        }

        public Dictionary<string, string> GetContractAdditionTemplateData(Guid contractId)
        {
            var result = new Dictionary<string, string>();
            AppContext.Configuration.LazyLoadingEnabled = true;
            AppContext.Configuration.ProxyCreationEnabled = true;
            var data = AppContext.Contracts.Where(e => e.Id == contractId)
                .Select(e => new
                {
                    ContractAddition = e,
                    Contract = e.ParentContract,
                    e.Signer
                }).FirstOrDefault();
            if (data == null) return result;
            result.Add("Number", data.Contract.Number);
            result.Add("ContractDate",
                (data.Contract.ContractDate != null ? data.Contract.ContractDate.Value.ToString("dd.MM.yyyy") : null));
            result.Add("SignerPosition", data.Signer != null ? data.Signer.Position.Name : null);
            result.Add("SignerInitials", data.Signer != null ? data.Signer.ShortName : null);
            result.Add("ContractAdditionDate",
            (data.ContractAddition.ContractDate != null
                ? data.ContractAddition.ContractDate.Value.ToString("dd.MM.yyyy")
                : null));
            return result;
        }

        public string GetContractAdditionType(Guid contractId)
        {
            return
                AppContext.Contracts.Where(e => e.Id == contractId)
                    .Select(e => e.ContractAdditionType.Code)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Показать список по владельцу и типу договора
        /// </summary>
        /// <param name="ownerId">владелец</param>
        /// <param name="code">код</param>
        /// <returns></returns>
        public IEnumerable<DataModel.Contract> GetOwnerList(Guid ownerId, int? code)
        {
            if (code == null)
            {
                return AppContext.Contracts.Where(e => e.OwnerId == ownerId).OrderBy(e => e.CreatedDate);
            }
            return AppContext.Contracts.Where(e => e.OwnerId == ownerId && e.Status == code).OrderBy(e => e.CreatedDate);
        }

        /// <summary>
        /// Показать список c проставлением дополнительной информации, по владельцу и типу договора
        /// </summary>
        /// <param name="ownerId">владелец</param>
        /// <param name="code">код</param>
        /// <returns></returns>
        public IEnumerable<DataModel.Contract> GetActiveContractListWithInfo(Guid ownerId)
        {
            var list = GetContractsByStatuses(ownerId, DataModel.Contract.StatusActive).OrderBy(e => e.ContractDate);
            foreach (var contract in list)
            {
                var buider = new StringBuilder("№" + contract.Number);
                if (contract.CreatedDate != null)
                {
                    buider.Append("; Дата:" + contract.CreatedDate.Value.ToShortDateString());
                }
                if (contract.ApplicantOrganizationId != null)
                {
                    buider.Append("; Заявитель:" + GetOrganizationInfo(contract.ApplicantOrganizationId));
                }
                if (contract.ManufacturerOrganizationId != null)
                {
                    buider.Append("; Производитель:" + GetOrganizationInfo(contract.ManufacturerOrganizationId));
                }
                contract.ContractInfo = buider.ToString();
            }
            return list;
        }

        /// <summary>
        /// Получение данных с организации
        /// </summary>
        /// <param name="orgId">ид организации</param>
        /// <returns></returns>
        private string GetOrganizationInfo(Guid? orgId)
        {
            if (orgId == null)
            {
                return null;
            }
            var org = AppContext.Organizations.FirstOrDefault(e => e.Id == orgId);
            return org?.NameRu;
        }

        /// <summary>
        /// Получение данные конктракта
        /// </summary>
        /// <param name="id">ид контракта</param>
        /// <returns></returns>
        public DataModel.Contract GetContractById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }
            var org = AppContext.Contracts.FirstOrDefault(e => e.Id == id);
            return org;
        }

        public ContractProcSetting GetContractProcSetting()
        {
            return AppContext.ContractProcSettings.FirstOrDefault();
        }

        public ContractProcSetting SaveContractProcSettings(ContractProcSetting settings)
        {
            if (settings.Id == Guid.Empty)
                AppContext.ContractProcSettings.Add(settings);
            else
                AppContext.Entry(settings).State = EntityState.Modified;
            AppContext.SaveChanges();
            return settings;
        }

        /// <summary>
        /// Отправка договора в ЦОЗ
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>Статус договора</returns>
        public Dictionary SendContractToProcessing(Guid contractId)
        {
            Document baseDocument = AppContext.Documents.FirstOrDefault(m => m.Id == contractId);
            var baseContract = AppContext.Contracts.First(o => o.Id == contractId);
            Dictionary contractStatus;
            if (baseDocument == null)
            {
                contractStatus = DictionaryHelper.GetDicItemByCode(Dictionary.ContractStatus.DicCode,
                    Database.DataModel.Contract.StatusInQueue);
                var user = UserHelper.GetCurrentEmployee();
                baseContract.Status = 1;
                Document document = new Document()
                {
                    Id = contractId,
                    DocumentType = 4,
                    ExecutionDate = DateTime.Now.AddDays(30),
                    DocumentDate = DateTime.Now,
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    CorrespondentsInfo = user.DisplayName,
                    CorrespondentsId = user.Id.ToString(),
                    CorrespondentsValue = user.DisplayName,
                    TemplateId = new Guid("C3292589-A25B-4CEF-8CB5-C7E64946C1D3"),
                    IsTradeSecret = false,
                    StateType = 1,
                    ProjectType = baseContract.Type.Value,
                    RegistratorId = user.Id.ToString(),
                    RegistratorValue = user.DisplayName,
                    ModifiedDate = DateTime.Now,
                    FirstExecutionDate = DateTime.Now.AddDays(30)
                };
                Registrator.SetNumber(document);
                AppContext.Documents.Add(document);
            }
            else
            {
                contractStatus = DictionaryHelper.GetDicItemByCode(Dictionary.ContractStatus.DicCode,
                    Database.DataModel.Contract.StatusCorrected);
            }
            baseContract.StatusId = contractStatus.Id;
            AppContext.SaveChanges();
            return contractStatus;
        }

        public Dictionary GetContractStatus(Guid contractId)
        {
            return AppContext.Contracts.Where(e => e.Id == contractId).Select(e => e.ContractStatus).FirstOrDefault();
        }

        public void TakeToWork(Guid contractId, Guid executorId)
        {
            var contract = AppContext.Contracts.Include(e => e.HolderType).FirstOrDefault(e => e.Id == contractId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                Database.DataModel.Contract.StatusInWork);
            var contractDoc = AppContext.Documents.FirstOrDefault(e => e.Id == contractId);
            contractDoc.ExecutorsId = executorId.ToString();
            var executor = AppContext.Employees.FirstOrDefault(e => e.Id == executorId);
            contractDoc.ExecutorsValue = executor.DisplayName;
            if (contract.ContractId == null)
            {
                string templateName;
                if (contract.HolderType.Code == Dictionary.ContractHolderType.Holder)
                    templateName = "ContractWithHolder.docx";
                else
                    templateName = "Contract.docx";
                var templatePath =
                    System.Web.HttpContext.Current.Server.MapPath("~/Content/DocxTemplates/" + templateName);
                Aspose.Words.Document doc = new Aspose.Words.Document(templatePath);
                var file = new MemoryStream();
                doc.Save(file, SaveFormat.Docx);
                file.Position = 0;
                FileHelper.SaveFile(
                    DictionaryHelper.GetDicIdByCode(Dictionary.SysAttachContract.DicCode,
                        Dictionary.SysAttachContract.Contract).ToString(),
                    contractId.ToString(), "договор.docx", file, AppContext);
            }
            AppContext.SaveChanges();
        }

        public bool AddComment(ContractComment contractComment)
        {
            var validStatuses = new[]
            {
                DataModel.Contract.StatusOnCorrection, DataModel.Contract.StatusInWork,
                DataModel.Contract.StatusCorrected
            };
            var contract =
                AppContext.Contracts.Include(c => c.ContractStatus)
                    .FirstOrDefault(e => e.Id == contractComment.ContractId);
            if (!validStatuses.Contains(contract.ContractStatus.Code))
                return false;
            //var contractStatus = DictionaryHelper.GetDicItemByCode(Dictionary.ContractStatus.DicCode,
            //    DataModel.Contract.StatusOnCorrection);
            if (contractComment.Id == Guid.Empty)
                AppContext.ContractComments.Add(contractComment);
            else
                AppContext.Entry(contractComment).State = EntityState.Modified;
            AppContext.SaveChanges();
            //var contractOwner = AppContext.Employees.FirstOrDefault(e => e.Id == contract.OwnerId);
            //NotificationManager nm = new NotificationManager();
            //nm.SendNotification("К договору добавлены замечания", ObjectType.Contract, contract.Id, contract.OwnerId,
            //    contractOwner.Email);
            return true;
        }

        public void SendComments(Guid[] commentIds)
        {
            var comments = AppContext.ContractComments.Include(e => e.Contract)
                .Where(e => commentIds.Contains(e.Id)).ToList();
            var contract = comments.FirstOrDefault().Contract;
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                DataModel.Contract.StatusOnCorrection);
            foreach (var comment in comments)
            {
                comment.Sended = true;
            }
            AppContext.SaveChanges();
            var contractOwner = AppContext.Employees.FirstOrDefault(e => e.Id == contract.OwnerId);
            NotificationManager nm = new NotificationManager();
            nm.SendNotification("К договору добавлены замечания", ObjectType.Contract, contract.Id, contract.OwnerId,
                contractOwner.Email);
        }

        public FileLink GetActualContractFile(Guid contractId)
        {
            var isContract = AppContext.Contracts.Any(e => e.Id == contractId && e.ContractId == null);
            var attachTypeCode = isContract
                ? Dictionary.SysAttachContract.DicCode
                : Dictionary.SysAttachContractAddition.DicCode;
            var attachItemTypeCode = isContract
                ? Dictionary.SysAttachContract.Contract
                : Dictionary.SysAttachContractAddition.ContractAddition;
            return
                AppContext.FileLinks.FirstOrDefault(
                    e => e.ParentId == null && e.DocumentId == contractId && e.FileCategory.Type == attachTypeCode
                         && e.FileCategory.Code == attachItemTypeCode);
        }

        public string GetContractTemplatePath(Guid contractId)
        {
            string templateName;
            if (
                AppContext.Contracts.Any(
                    e => e.Id == contractId && e.HolderType.Code == Dictionary.ContractHolderType.Holder))
                templateName = "ContractWithHolder.docx";
            else
                templateName = "Contract.docx";
            return System.Web.HttpContext.Current.Server.MapPath("~/Content/DocxTemplates/" + templateName);
        }

        public string GetContractAdditionTemplatePath(Guid contractId)
        {
            var additionTypeCode = GetContractAdditionType(contractId);
            string templateName;
            if (additionTypeCode == Dictionary.DicContractAddition.BankChangedCode)
                templateName = "BankChangedContractAddition.docx";
            else if (additionTypeCode == Dictionary.DicContractAddition.BossChangedCode)
                templateName = "BossChangedContractAddition.docx";
            else if (additionTypeCode == Dictionary.DicContractAddition.ContractPeriodChangedCode)
                templateName = "ContractPeriodChangedContractAddition.docx";
            else if (additionTypeCode == Dictionary.DicContractAddition.LegalAddresChangeCode)
                templateName = "LegalAddresChangeContractAddition.docx";
            else
                templateName = "";
            return System.Web.HttpContext.Current.Server.MapPath("~/Content/DocxTemplates/" + templateName);
        }

        public Guid GetExecutorId(Guid contractId)
        {
            var executor =
                AppContext.Documents.Where(e => e.Id == contractId).Select(e => e.ExecutorsId).FirstOrDefault();
            Guid executorId = Guid.Empty;
            Guid.TryParse(executor, out executorId);
            return executorId;
        }

        public Guid GetSignerId(Guid contractId)
        {
            var signerId = AppContext.Contracts.Where(e => e.Id == contractId).Select(e => e.SignerId).FirstOrDefault();
            return signerId != null ? signerId.Value : Guid.Empty;
        }

        public void SendToCopySign(Guid contractId)
        {
            SendToRegistration(contractId);
            var contractOwnerId =
                AppContext.Contracts.Where(e => e.Id == contractId).Select(e => e.OwnerId).FirstOrDefault();
            var contractOwner = AppContext.Employees.FirstOrDefault(e => e.Id == contractOwnerId);
            NotificationManager nm = new NotificationManager();
            nm.SendNotification("Вам необходимо подписать договор в НЦЭЛС", ObjectType.Contract, contractId, contractOwnerId,
                contractOwner.Email);
        }

        public void SendToRegistration(Guid contractId)
        {
            var contract = AppContext.Contracts.FirstOrDefault(e => e.Id == contractId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                DataModel.Contract.StatusOnRegistration);
            ConvertSignedContractToPdf(contractId, contract.ContractId == null ? GetContractTemplateData(contractId) : GetContractAdditionTemplateData(contractId));
            AppContext.SaveChanges();
        }

        public DataModel.Contract SetNumberAndDate(Guid contractId)
        {
            var contract = AppContext.Contracts.FirstOrDefault(e => e.Id == contractId);
            if (contract.ContractDate != null && !string.IsNullOrEmpty(contract.Number)) return contract;
            contract.ContractDate = DateTime.Now;
            contract.Number = Registrator.GetNumber("ContractNumbers").ToString();
            AppContext.SaveChanges();
            return contract;
        }

        public void RegisterContract(Guid contractId)
        {
            var contract = AppContext.Contracts
                .Include(e => e.ManufacturerOrganization)
                .Include(e => e.ApplicantOrganization)
                .Include(e => e.HolderOrganization)
                .Include(e => e.PayerOrganization)
                .Include(e => e.PayerTranslationOrganization)
                .FirstOrDefault(e => e.Id == contractId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                DataModel.Contract.StatusActive);
            //кидаем организации в историю
            var histProducer = (Organization)contract.ManufacturerOrganization.Clone();
            var histApplicant = (Organization)contract.ApplicantOrganization.Clone();
            var histHolder = contract.HolderOrganizationId != null ? (Organization)contract.HolderOrganization.Clone() : null;
            var histPayer = (Organization)contract.PayerOrganization.Clone();
            var histPayerTranslation = (Organization)contract.PayerTranslationOrganization.Clone();
            var origProducerId = contract.ManufacturerOrganizationId;
            var origApplicantId = contract.ApplicantOrganizationId;
            var origHolderId = contract.HolderOrganizationId;
            var origPayerId = contract.PayerOrganizationId;
            var origPayerTranslationId = contract.PayerTranslationOrganizationId;
            AppContext.Organizations.Add(histApplicant);
            contract.ApplicantOrganizationId = histApplicant.Id;
            contract.ApplicantOrganization = histApplicant;
            if (origProducerId != origHolderId)
            {
                AppContext.Organizations.Add(histProducer);
                contract.ManufacturerOrganizationId = histProducer.Id;
                contract.ManufacturerOrganization = histProducer;
            }
            if (histHolder != null)
            {
                AppContext.Organizations.Add(histHolder);
                contract.HolderOrganizationId = histHolder.Id;
                contract.HolderOrganization = histHolder;
            }
            if (origHolderId == origProducerId)
            {
                contract.ManufacturerOrganizationId = histHolder.Id;
                contract.ManufacturerOrganization = histHolder;
            }
            if (origPayerId != origProducerId
                && origPayerId != origHolderId
                && origPayerId != origApplicantId)
            {
                AppContext.Organizations.Add(histPayer);
                contract.PayerOrganizationId = histPayer.Id;
                contract.PayerOrganization = histPayer;
            }
            if (origPayerId == origProducerId)
            {
                contract.PayerOrganizationId = histProducer.Id;
                contract.PayerOrganization = histProducer;
            }
            else if (origPayerId == origApplicantId)
            {
                contract.PayerOrganizationId = histApplicant.Id;
                contract.PayerOrganization = histApplicant;
            }
            else if (origPayerId == origHolderId)
            {
                contract.PayerOrganizationId = histHolder.Id;
                contract.PayerOrganization = histHolder;
            }
            if (origPayerTranslationId != origProducerId
                && origPayerTranslationId != origHolderId
                && origPayerTranslationId != origApplicantId)
            {
                AppContext.Organizations.Add(histPayerTranslation);
                contract.PayerTranslationOrganizationId = histPayerTranslation.Id;
                contract.PayerOrganization = histPayerTranslation;
            }
            if (origPayerTranslationId == origProducerId)
            {
                contract.PayerTranslationOrganizationId = histProducer.Id;
                contract.PayerTranslationOrganization = histProducer;
            }
            else if (origPayerTranslationId == origApplicantId)
            {
                contract.PayerTranslationOrganizationId = histApplicant.Id;
                contract.PayerTranslationOrganization = histApplicant;
            }
            else if (origPayerTranslationId == origHolderId)
            {
                contract.PayerTranslationOrganizationId = histHolder.Id;
                contract.PayerTranslationOrganization = histHolder;
            }
            AppContext.SaveChanges();
            var contract1C = new I1c_exp_Contracts()
            {
                Id = contract.Id,
                ContractDate = contract.ContractDate.Value,
                ContractNumber = contract.Number,
                ContractUrl = string.Format("{0}FileStorage/Download?id={1}&fileType=Contract", ConfigurationManager.AppSettings["ServerBaseUrl"], contract.Id)
            };
            AppContext.I1c_exp_Contracts.Add(contract1C);
            AppContext.SaveChanges();
        }

        private void ConvertSignedContractToPdf(Guid contractId, Dictionary<string, string> templateData)
        {
            var contractFile = GetActualContractFile(contractId);
            Aspose.Words.Document doc = new Aspose.Words.Document(FileHelper.GetFilePath(contractId.ToString(), contractFile.CategoryId.Value.ToString(),
                null, string.Format("{0}{1}", contractFile.Id, contractFile.Extension)));
            if (templateData != null)
                doc.ReplaceText(templateData);
            var contractSign = AppContext.ContractSignedDatas.FirstOrDefault(e => e.ContractId == contractId);
            doc.InserQrCodes("applicantDigSign", contractSign != null ? contractSign.ApplicantSig : null);
            doc.InserQrCodes("ceoDigSign", contractSign != null ? contractSign.CeoSign : null);
            doc.InsertDocUrl(string.Format("{0}FileStorage/Download?id={1}&fileType=Contract", ConfigurationManager.AppSettings["ServerBaseUrl"], contractId));
            var file = new MemoryStream();
            doc.Save(file, SaveFormat.Pdf);
            file.Position = 0;
            FileHelper.SaveAttachNewVersion(contractFile.CategoryId.Value.ToString(), contractId.ToString(),
                contractFile.Id.ToString(),
                string.Format("{0}.pdf", Path.GetFileNameWithoutExtension(contractFile.FileName)), file, AppContext);
            file.Close();
        }

        public bool IsContractWithoutDc(Guid contractId)
        {
            return !AppContext.EXP_Activities.Any(e => e.DocumentId == contractId
                                                       && e.ExpActivityType.Code == Dictionary.ExpActivityType.ContractSigningByApplicantAndCeo);
        }

        public string GetDataForSign(Guid contractId)
        {
            var contract = AppContext.Contracts.FirstOrDefault(e => e.Id == contractId);
            var dataForSign = new ContractSignData()
            {
                ContractId = contractId,
                ContractDate = contract.ContractDate.Value,
                ContractNumber = contract.Number,
                ContractStartDate = contract.StartDate.Value,
                ContractEndDate = contract.EndDate.Value
            };
            var xmlData = SerializeHelper.SerializeDataContract(dataForSign);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public void SaveSign(Guid contractId, string signedData, bool ceoSign)
        {
            var signedContract = AppContext.ContractSignedDatas.FirstOrDefault(e => e.ContractId == contractId);
            if (signedContract == null)
            {
                signedContract = new ContractSignedData()
                {
                    ContractId = contractId
                };
                AppContext.ContractSignedDatas.Add(signedContract);
            }
            if (ceoSign)
                signedContract.CeoSign = signedData;
            else
                signedContract.ApplicantSig = signedData;
            AppContext.SaveChanges();

        }
    }
}