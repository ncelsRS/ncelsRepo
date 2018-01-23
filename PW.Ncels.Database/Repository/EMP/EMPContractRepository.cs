using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.EMP;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.OBK;
using Contacts = PW.Ncels.Database.Models.EMP.Contacts;

namespace PW.Ncels.Database.Repository.EMP
{
    public class EMPContractRepository : ARepository
    {
        public EMPContractRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public EMPContractRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public EMPContractRepository(ncelsEntities context) : base(context) { }

        /// <summary>
        /// загрузка списка заявлений
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isRegisterProject"></param>
        /// <param name="contractScope"></param>
        /// <returns></returns>
        public async Task<object> GetContractList(ModelRequest request, bool isRegisterProject, string contractScope)
        {
            try
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var v = AppContext.EMP_Contract.Where(x => x.EmployeeId == employeeId).AsQueryable();

                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a => a.Number.Contains(request.SearchValue));
                }

                if (!string.IsNullOrWhiteSpace(contractScope))
                    v = v.Where(x => x.EMP_Ref_ContractScope != null && x.EMP_Ref_ContractScope.Code == contractScope);


                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                {
                    v = v.OrderByDescending(x => x.CreatedDate);
                }

                int recordsTotal = await v.CountAsync();
                var contractViews = v.Skip(request.Skip).Take(request.PageSize).Select(x => new
                    {
                        x.Id,
                        x.Number,
                        x.CreatedDate,
                        Status = x.EMP_Ref_Status != null ? x.EMP_Ref_Status.NameRu : string.Empty,
                        ManufacturName = x.OBK_DeclarantManufactur.NameRu,
                        x.MedicalDeviceName,
                        //x.StartDate
                    }
                );
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await contractViews.ToListAsync()
                    };
            }
            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }
        }

        public IQueryable<EMP_Ref_HolderType> GetHolderTypes(string contractScope)
        {
            return AppContext.EMP_Ref_HolderType.Where(e => !e.IsDeleted && e.Code == contractScope);
        }

        public IQueryable<EMP_Ref_ContractType> GetContractType()
        {
            return AppContext.EMP_Ref_ContractType.Where(e => !e.IsDeleted);
        }

        public IQueryable<Dictionary> GetCurrency()
        {
            return AppContext.Dictionaries.Where(e => e.Type == "Currency");
        }

        public IQueryable<object> GetExpertOrganizations()
        {
            var items = AppContext.Units.Where(x => x.Code == "00").Select(x => new
            {
                Id = x.Id,
                Name = x.ShortName
            });
            return items;
        }

        public IQueryable<EMP_Ref_ContractDocumentType> GetEMPContractDocumentType()
        {
            return AppContext.EMP_Ref_ContractDocumentType;
        }

        public List<EMP_Ref_Bank> GetBanks()
        {
            List<EMP_Ref_Bank> addBanks = new List<EMP_Ref_Bank>();
            EMP_Ref_Bank noBank = new EMP_Ref_Bank
            {
                Code = null,
                CreatedDate = DateTime.Now,
                Id = 999,
                IsConfirmed = true,
                IsDeleted = false,
                NameKz = "Нет данных",
                NameRu = "Нет данных"
            };
            addBanks.Add(noBank);
            var banks = EmpReferenceHelper.GetBanks();
            foreach (var bank in banks)
            {
                if (!bank.IsConfirmed)
                {
                    bank.NameRu = bank.NameRu + " (не подтвержден)";
                }
                addBanks.Add(bank);
            }
            return addBanks;
        }

        public EMP_Ref_Bank SaveNewBank(string bankNameRu, string bankNameKz)
        {
            var bank = new EMP_Ref_Bank
            {
                Code = null,
                NameKz = bankNameKz,
                NameRu = bankNameRu,
                CreatedDate = DateTime.Now,
                IsConfirmed = false,
                IsDeleted = false
            };
            AppContext.EMP_Ref_Bank.Add(bank);
            AppContext.SaveChanges();
            return bank;
        }

        public OBK_Declarant FindOrganization(string bin)
        {
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(x => x.Bin == bin && x.IsResident && !x.IsDeleted);
            return declarant;
        }

        private string GetLastNumberOfContract()
        {
            string number = "1";
            var numbers = AppContext.EMP_Contract.Select(x => x.Number).ToList();
            int temp;
            int contractNumber = numbers.Select(n => int.TryParse(n, out temp) ? temp : 0).Max();
            contractNumber++;
            number = contractNumber.ToString();
            return number;
        }

        public EMPContractViewModel SaveContract(Guid Guid, EMPContractViewModel contractViewModel)
        {
            var model = GetById(contractViewModel.Id);
            if (model == null)
            {
                EMP_Contract contract = new EMP_Contract();
                contract.ContractStatusId = AppContext.EMP_Ref_Status.Where(x =>
                    x.Code == CodeConstManager.EmpContractStatus.Draft).Select(x => x.Id).FirstOrDefault();
                contract.ContractScopeId = AppContext.EMP_Ref_ContractScope
                    .Where(x => x.Code == contractViewModel.ContractScope).Select(x => x.Id).FirstOrDefault();

                contract.Id = Guid;
                contract.EmployeeId = UserHelper.GetCurrentEmployee().Id;
                contract.Number = GetLastNumberOfContract();
                contract.CreatedDate = DateTime.Now;
                contract.Status = CodeConstManager.STATUS_DRAFT_ID;
                contract.ContractType = contractViewModel.ContractType;
                contract.HolderType = contractViewModel.HolderType;
                contract.MedicalDeviceName = contractViewModel.MedicalDeviceName;
                contract.MedicalDeviceNameKz = contractViewModel.MedicalDeviceNameKz;
                contract.DeclarantIsManufactur = contractViewModel.DeclarantIsManufactur;
                contract.ChoosePayer = contractViewModel.ChoosePayer;
                contract.HasProxy = contractViewModel.HasProxy;
                contract.DocumentType = contractViewModel.DocumentType;
                contract.StatemantNumber = contractViewModel.StatemantNumber;

                if (contractViewModel.Manufactur != null) {
                    FillDec(contractViewModel.Manufactur, "Manufactur", contract);
                    if (contractViewModel.Manufactur.Contact != null) {
                        FillDecContact(contractViewModel.Manufactur.Contact, contractViewModel.Manufactur.Id, contract, "Manufactur");
                    }
                }
                if (contractViewModel.Declarant != null)
                {
                    if (contractViewModel.DeclarantIsManufactur)
                    {
                        contract.OBK_Declarant = contract.OBK_DeclarantManufactur;
                        contract.OBK_DeclarantContact = contract.OBK_DeclarantContactManufactur;

                        contract.OBK_DeclarantContact.BossDocNumber = contract.OBK_DeclarantContactManufactur.BossDocNumber;
                        contract.OBK_DeclarantContact.BossDocType = contract.OBK_DeclarantContactManufactur.BossDocType;
                        contract.OBK_DeclarantContact.IsHasBossDocNumber = contract.OBK_DeclarantContactManufactur.IsHasBossDocNumber;
                        contract.OBK_DeclarantContact.BossDocCreatedDate = contract.OBK_DeclarantContactManufactur.BossDocCreatedDate;
                        contract.OBK_DeclarantContact.BossDocUnlimited = contract.OBK_DeclarantContactManufactur.BossDocUnlimited;
                        contract.OBK_DeclarantContact.BossDocCreatedDate = contract.OBK_DeclarantContactManufactur.BossDocCreatedDate;
                        contract.OBK_DeclarantContact.BossDocEndDate = contract.OBK_DeclarantContactManufactur.BossDocEndDate;
                        //if (contractViewModel.Declarant.Contact != null)
                        //{
                        //    FillDecContact(contractViewModel.Declarant.Contact, contractViewModel.Declarant.Id,
                        //        contract, "Declarant");
                        //}
                    }
                    else
                    {
                        FillDec(contractViewModel.Declarant, "Declarant", contract);
                        if (contractViewModel.Declarant.Contact != null)
                        {
                            FillDecContact(contractViewModel.Declarant.Contact, contractViewModel.Declarant.Id,
                                contract, "Declarant");
                        }
                    }

                }
                if (contractViewModel.Payer != null) {
                    switch (contractViewModel.ChoosePayer)
                    {
                        case "Manufactur":
                            contract.OBK_DeclarantPayer = contract.OBK_DeclarantManufactur;
                            contract.OBK_DeclarantContactPayer = contract.OBK_DeclarantContactManufactur;
                            break;
                        case "Declarant":
                            contract.OBK_DeclarantPayer = contract.OBK_Declarant;
                            contract.OBK_DeclarantContactPayer = contract.OBK_DeclarantContact;
                            break;
                        case "Payer":
                            FillDec(contractViewModel.Payer, "Payer", contract);
                            if (contractViewModel.Payer.Contact != null) {
                                FillDecContact(contractViewModel.Payer.Contact, contractViewModel.Payer.Id, contract, "Payer");
                            }
                            break;
                    }
                }
                if (contractViewModel.ServicePrices?.Count > 0) {
                    FillCostWork(contractViewModel.ServicePrices, contract);
                }
                contractViewModel.Id = contract.Id;
                AppContext.EMP_Contract.Add(contract);
                AppContext.SaveChanges();
            } else {
                model.ContractType = contractViewModel.ContractType;
                model.HolderType = contractViewModel.HolderType;
                model.MedicalDeviceName = contractViewModel.MedicalDeviceName;
                model.MedicalDeviceNameKz = contractViewModel.MedicalDeviceNameKz;
                model.DeclarantIsManufactur = contractViewModel.DeclarantIsManufactur;
                model.ChoosePayer = contractViewModel.ChoosePayer;
                model.HasProxy = contractViewModel.HasProxy;
                model.DocumentType = contractViewModel.DocumentType;
                model.StatemantNumber = contractViewModel.StatemantNumber;

                if (contractViewModel.Manufactur != null) {
                    FillDec(contractViewModel.Manufactur, "Manufactur", model);
                    if (contractViewModel.Manufactur.Contact != null) {
                        FillDecContact(contractViewModel.Manufactur.Contact, contractViewModel.Manufactur.Id, model, "Manufactur");
                    }
                }
                if (contractViewModel.Declarant != null) {
                    if (contractViewModel.DeclarantIsManufactur)
                    {
                        model.OBK_Declarant = model.OBK_DeclarantManufactur;
                        model.OBK_DeclarantContact = model.OBK_DeclarantContactManufactur;

                        model.OBK_DeclarantContact.BossDocNumber = model.OBK_DeclarantContactManufactur.BossDocNumber;
                        model.OBK_DeclarantContact.BossDocType = model.OBK_DeclarantContactManufactur.BossDocType;
                        model.OBK_DeclarantContact.IsHasBossDocNumber = model.OBK_DeclarantContactManufactur.IsHasBossDocNumber;
                        model.OBK_DeclarantContact.BossDocCreatedDate = model.OBK_DeclarantContactManufactur.BossDocCreatedDate;
                        model.OBK_DeclarantContact.BossDocUnlimited = model.OBK_DeclarantContactManufactur.BossDocUnlimited;
                        model.OBK_DeclarantContact.BossDocCreatedDate = model.OBK_DeclarantContactManufactur.BossDocCreatedDate;
                        model.OBK_DeclarantContact.BossDocEndDate = model.OBK_DeclarantContactManufactur.BossDocEndDate;
                        //if (contractViewModel.Declarant.Contact != null)
                        //{
                        //    FillDecContact(contractViewModel.Declarant.Contact, contractViewModel.Declarant.Id,
                        //        model, "Declarant");
                        //}
                    }
                    else
                    {
                        FillDec(contractViewModel.Declarant, "Declarant", model);
                        if (contractViewModel.Declarant.Contact != null)
                        {
                            FillDecContact(contractViewModel.Declarant.Contact, contractViewModel.Declarant.Id,
                                model, "Declarant");
                        }
                    }
                }
                if (contractViewModel.Payer != null) {
                    switch (contractViewModel.ChoosePayer)
                    {
                        case "Manufactur":
                            model.OBK_DeclarantPayer = model.OBK_DeclarantManufactur;
                            model.OBK_DeclarantContactPayer = model.OBK_DeclarantContactManufactur;
                            break;
                        case "Declarant":
                            model.OBK_DeclarantPayer = model.OBK_Declarant;
                            model.OBK_DeclarantContactPayer = model.OBK_DeclarantContact;
                            break;
                        case "Payer":
                            FillDec(contractViewModel.Payer, "Payer", model);
                            if (contractViewModel.Payer.Contact != null)
                            {
                                FillDecContact(contractViewModel.Payer.Contact, contractViewModel.Payer.Id, model, "Payer");
                            }
                            break;
                    }
                }
                if (contractViewModel.ServicePrices?.Count > 0) {
                    FillCostWork(contractViewModel.ServicePrices, model);
                }
                contractViewModel.Id = model.Id;
                AppContext.EMP_Contract.AddOrUpdate(model);
                AppContext.SaveChanges();
            }
            return contractViewModel;
        }

        private void FillCostWork(List<ServicePrice> servicePrices, EMP_Contract contract)
        {
            var costWorks = AppContext.EMP_CostWorks.Where(e => e.ContractId == contract.Id).ToList();
            if (costWorks.Count > 0)
            {
                foreach (var servicePrice in servicePrices)
                {
                    foreach (var costWork in costWorks)
                    {
                        if (costWork.ContractId != servicePrice.ContractId)
                        {
                            EMP_CostWorks cost = new EMP_CostWorks
                            {
                                Id = servicePrice.Id,
                                Count = servicePrice.Count,
                                Price = servicePrice.Price,
                                PriceListId = servicePrice.PriceListId,
                                TotalPrice = servicePrice.TotalPrice,
                                ContractId = contract.Id
                            };
                            servicePrice.ContractId = contract.Id;
                            contract.EMP_CostWorks.Add(cost);
                        }
                    }
                }
            }
            else
            {
                foreach (var servicePrice in servicePrices)
                {
                    EMP_CostWorks cost = new EMP_CostWorks()
                    {
                        Id = servicePrice.Id,
                        Count = servicePrice.Count,
                        Price = servicePrice.Price,
                        PriceListId = servicePrice.PriceListId,
                        TotalPrice = servicePrice.TotalPrice,
                        ContractId = servicePrice.ContractId
                    };
                    servicePrice.ContractId = contract.Id;
                    contract.EMP_CostWorks.Add(cost);
                }
            }
        }

        private void FillDec(Declarants declaration, string type, EMP_Contract contract)
        {
            var declarant = AppContext.OBK_Declarant.FirstOrDefault(e => e.Id == declaration.Id);
            if (declarant == null)
            {
                OBK_Declarant dec = new OBK_Declarant
                {
                    Id = Guid.NewGuid(),
                    Bin = declaration.Bin,
                    NameKz = declaration.NameKz,
                    NameRu = declaration.NameRu,
                    NameEn = declaration.NameEn,
                    CountryId = declaration.CountryId,
                    OrganizationFormId = declaration.OrganizationFormId,
                    IsResident = declaration.IsResident,
                    Iin = declaration.Iin
                };
                declaration.Id = dec.Id;
                switch (type)
                {
                    case "Manufactur":
                        contract.OBK_DeclarantManufactur = dec;
                        break;
                    case "Declarant":
                        contract.OBK_Declarant = dec;
                        //contract.DeclarantId = dec.Id;
                        break;
                    case "Payer":
                        contract.OBK_DeclarantPayer = dec;
                        break;
                }
            }
            else
            {
                declarant.Bin = declaration.Bin;
                declarant.NameKz = declaration.NameKz;
                declarant.NameRu = declaration.NameRu;
                declarant.NameEn = declaration.NameEn;
                declarant.CountryId = declaration.CountryId;
                declarant.OrganizationFormId = declaration.OrganizationFormId;
                declarant.IsResident = declaration.IsResident;
                declarant.Iin = declaration.Iin;

                switch (type)
                {
                    case "Manufactur":
                        contract.OBK_DeclarantManufactur = declarant;
                        break;
                    case "Declarant":
                        contract.OBK_Declarant = declarant;
                        break;
                    case "Payer":
                        contract.OBK_DeclarantPayer = declarant;
                        break;
                }
            }
        }

        private void FillDecContact(Contacts contact, Guid declarantId, EMP_Contract contract, string type)
        {
            var declarantContact = AppContext.OBK_DeclarantContact.OrderByDescending(e=>e.CreateDate).FirstOrDefault(e => e.Id == contact.Id);
            if (declarantContact == null)
            {
                OBK_DeclarantContact decContact = new OBK_DeclarantContact
                {
                    Id = Guid.NewGuid(),
                    DeclarantId = declarantId,
                    AddressLegalRu = contact.AddressLegalRu,
                    AddressLegalKz = contact.AddressLegalKz,
                    AddressFact = contact.AddressFact,
                    Phone = contact.Phone,
                    Phone2 = contact.Phone2,
                    Email = contact.Email,
                    BankId = contact.BankId,
                    BankAccount = contact.BankAccount,
                    BankNameRu = contact.BankNameRu,
                    BankNameKz = contact.BankNameKz,
                    BankIik = contact.BankIik,
                    BankBik = contact.BankBik,
                    CurrencyId = contact.CurrencyId,
                    BossFio = contact.BossLastName + " " + contact.BossFirstName + " " +
                              contact.BossMiddleName,
                    BossPosition = contact.BossPosition,
                    BossLastName = contact.BossLastName,
                    BossFirstName = contact.BossFirstName,
                    BossMiddleName = contact.BossMiddleName,
                    BossDocNumber = contact.BossDocNumber,
                    BossDocType = contact.BossDocType,
                    IsHasBossDocNumber = contact.IsHasBossDocNumber,
                    BossDocCreatedDate = contact.BossDocCreatedDate,
                    SignLastName = contact.SignLastName,
                    SignFirstName = contact.SignFirstName,
                    SignMiddleName = contact.SignMiddleName,
                    SignPosition = contact.SignPosition,
                    SignDocType = contact.SignDocType,
                    IsHasSignDocNumber = contact.IsHasSignDocNumber,
                    SignDocNumber = contact.SignDocNumber,
                    SignDocCreatedDate = contact.SignDocCreatedDate,
                    CreateDate = DateTime.Now,
                    SignDocEndDate = contact.SignDocEndDate,
                    SignDocUnlimited = contact.SignDocUnlimited,
                    BossDocEndDate = contact.BossDocEndDate,
                    BossDocUnlimited = contact.BossDocUnlimited,
                    SignerIsBoss = contact.SignerIsBoss,
                    SignPositionKz = contact.SignPositionKz,
                    BossPositionKz = contact.BossPositionKz
                };
                contact.Id = decContact.Id;

                switch (type)
                {
                    case "Manufactur":
                        contract.OBK_DeclarantContactManufactur = decContact;
                        break;
                    case "Declarant":
                        contract.OBK_DeclarantContact = decContact;
                        break;
                    case "Payer":
                        contract.OBK_DeclarantContactPayer = decContact;
                        break;
                }
            }
            else
            {
                declarantContact.DeclarantId = declarantId;
                declarantContact.AddressLegalRu = contact.AddressLegalRu;
                declarantContact.AddressLegalKz = contact.AddressLegalKz;
                declarantContact.AddressFact = contact.AddressFact;
                declarantContact.Phone = contact.Phone;
                declarantContact.Phone2 = contact.Phone2;
                declarantContact.Email = contact.Email;
                declarantContact.BankId = contact.BankId;
                declarantContact.BankAccount = contact.BankAccount;
                declarantContact.BankNameRu = contact.BankNameRu;
                declarantContact.BankNameKz = contact.BankNameKz;
                declarantContact.BankIik = contact.BankIik;
                declarantContact.BankBik = contact.BankBik;
                declarantContact.CurrencyId = contact.CurrencyId;
                declarantContact.BossFio = contact.BossLastName + " " + contact.BossFirstName + " " +
                          contact.BossMiddleName;
                declarantContact.BossPosition = contact.BossPosition;
                declarantContact.BossLastName = contact.BossLastName;
                declarantContact.BossFirstName = contact.BossFirstName;
                declarantContact.BossMiddleName = contact.BossMiddleName;
                declarantContact.BossDocNumber = contact.BossDocNumber;
                declarantContact.BossDocType = contact.BossDocType;
                declarantContact.IsHasBossDocNumber = contact.IsHasBossDocNumber;
                declarantContact.BossDocCreatedDate = contact.BossDocCreatedDate;
                declarantContact.SignLastName = contact.SignLastName;
                declarantContact.SignFirstName = contact.SignFirstName;
                declarantContact.SignMiddleName = contact.SignMiddleName;
                declarantContact.SignPosition = contact.SignPosition;
                declarantContact.SignDocType = contact.SignDocType;
                declarantContact.IsHasSignDocNumber = contact.IsHasSignDocNumber;
                declarantContact.SignDocNumber = contact.SignDocNumber;
                declarantContact.SignDocCreatedDate = contact.SignDocCreatedDate;
                declarantContact.CreateDate = DateTime.Now;
                declarantContact.SignDocEndDate = contact.SignDocEndDate;
                declarantContact.SignDocUnlimited = contact.SignDocUnlimited;
                declarantContact.BossDocEndDate = contact.BossDocEndDate;
                declarantContact.BossDocUnlimited = contact.BossDocUnlimited;
                declarantContact.SignerIsBoss = contact.SignerIsBoss;
                declarantContact.SignPositionKz = contact.SignPositionKz;
                declarantContact.BossPositionKz = contact.BossPositionKz;

                switch (type)
                {
                    case "Manufactur":
                        contract.OBK_DeclarantContactManufactur = declarantContact;
                        break;
                    case "Declarant":
                        contract.OBK_DeclarantContact = declarantContact;
                        break;
                    case "Payer":
                        contract.OBK_DeclarantContactPayer = declarantContact;
                        break;
                }
            }
        }

        private EMP_Contract GetById(Guid modelId)
        {
            return AppContext.EMP_Contract.FirstOrDefault(e => e.Id == modelId);
        }

        private object NoData()
        {
            var noServiceType = new
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                NameKz = "Нет данных",
                NameRu = "Нет данных"
            };
            return noServiceType;
        }

        public List<EMP_Ref_ChangeType> GetChangeType()
        {
            var result = EmpReferenceHelper.GetChangeType();
            return result;
        }

        public List<object> GetServiceType(string contractScope)
        {
            List<object> serviceTypes = new List<object> {NoData()};
            var result = EmpReferenceHelper.GetServiceType().Where(e=>e.ParentId == null && !e.IsDeleted && e.Code == contractScope).Select(e=> new {e.Id, e.NameRu, e.NameKz, e.ParentId, e.ChangeType});
            serviceTypes.AddRange(result);
            return serviceTypes;
        }

        public List<object> GetServiceTypeParentId(string id)
        {
            List<object> serviceTypes = new List<object> { NoData() };
            var result = EmpReferenceHelper.GetServiceTypeParentId(Guid.Parse(id)).Select(e => new { e.Id, e.NameRu, e.NameKz, e.ParentId, Code = e.EMP_Ref_DegreeRisk == null ? "9" : e.EMP_Ref_DegreeRisk.Code });
            serviceTypes.AddRange(result);
            return serviceTypes;
        }

        public List<object> GetPriceTypeWhithPriceList(string id)
        {
            List<object> priceTypes = new List<object> { NoData() };
            var resultPriceLists = EmpReferenceHelper.GetPriceList(Guid.Parse(id)).Select(e=>e.PriceTypeId);
            var resultPriceTypes = EmpReferenceHelper.GetPriceType(resultPriceLists);
            foreach (var resultPriceType in resultPriceTypes)
            {
                object priceType = new
                {
                    resultPriceType.Id,
                    resultPriceType.NameKz,
                    resultPriceType.NameRu
                };
                priceTypes.Add(priceType);
            }
            return priceTypes;
        }

        public List<ServicePrice> GetPriceList(Guid serviceTypeId, Guid serviceTypeModifId, bool isImport, int count)
        {
            var result =
                AppContext.EMP_Ref_PriceList.FirstOrDefault(
                    e => e.ServiceTypeId == serviceTypeId && e.Import == isImport);

            if (serviceTypeModifId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                if (result != null)
                {
                    List<ServicePrice> servicePrices = new List<ServicePrice>();
                    var totalCount = TaxHelper.GetCalculationTax((decimal) result.Price) * 1;
                    var returnResult = new ServicePrice
                    {
                        Id = Guid.NewGuid(),
                        Price = (decimal) result.Price,
                        Count = 1,
                        TotalPrice = totalCount,
                        ServiceTypeNameRu = result.EMP_Ref_ServiceType.NameRu,
                        PriceListId = result.Id
                    };
                    servicePrices.Add(returnResult);
                    return servicePrices;
                }
            }
            else
            {
                if (result != null)
                {
                    List<ServicePrice> servicePrices = new List<ServicePrice>();
                    var totalCount = TaxHelper.GetCalculationTax((decimal) result.Price) * 1;
                    var returnResult = new ServicePrice
                    {
                        Id = Guid.NewGuid(),
                        Price = (decimal) result.Price,
                        Count = 1,
                        TotalPrice = totalCount,
                        ServiceTypeNameRu = result.EMP_Ref_ServiceType.NameRu,
                        PriceListId = result.Id
                    };
                    servicePrices.Add(returnResult);

                    var resultModif =
                        AppContext.EMP_Ref_PriceList.FirstOrDefault(
                            e => e.ServiceTypeId == serviceTypeModifId && e.Import == isImport);
                    if (resultModif != null)
                    {
                        var totalCountModif = TaxHelper.GetCalculationTax((decimal) resultModif.Price) * count;
                        var returnResultModif = new ServicePrice
                        {
                            Id = Guid.NewGuid(),
                            Price = (decimal) resultModif.Price,
                            Count = count,
                            TotalPrice = totalCountModif,
                            ServiceTypeNameRu = resultModif.EMP_Ref_ServiceType.NameRu,
                            PriceListId = resultModif.Id
                        };
                        servicePrices.Add(returnResultModif);
                    }
                    return servicePrices;
                }
            }
            return null;
        }

        public List<ServicePrice> GetCalculationEaes(Guid serviceTypeId)
        {
            var result = AppContext.EMP_Ref_PriceList.FirstOrDefault(e => e.ServiceTypeId == serviceTypeId);
            if (result != null)
            {
                List<ServicePrice> servicePrices = new List<ServicePrice>();
                var totalCount = TaxHelper.GetCalculationTax((decimal)result.Price);
                var returnResult = new ServicePrice
                {
                    Id = Guid.NewGuid(),
                    Price = (decimal)result.Price,
                    Count = 1,
                    TotalPrice = totalCount,
                    ServiceTypeNameRu = result.EMP_Ref_ServiceType.NameRu,
                    PriceListId = result.Id
                };
                servicePrices.Add(returnResult);
                return servicePrices;
            }
            return null;
        }

        public void GetClearCostWork(Guid contractId)
        {
            var costWorks = AppContext.EMP_CostWorks.Where(e => e.ContractId == contractId).ToList();
            if (costWorks.Count > 0)
            {
                foreach (var costWork in costWorks)
                {
                    AppContext.EMP_CostWorks.Remove(costWork);
                }
                AppContext.SaveChanges();
            }
        }

        public EMPContractViewModel LoadContract(Guid contractId)
        {
            var contract = AppContext.EMP_Contract.FirstOrDefault(e => e.Id == contractId);
            if (contract == null)
                return null;
            EMPContractViewModel contractView = new EMPContractViewModel
            {
                Id = contract.Id,
                MedicalDeviceName = contract.MedicalDeviceName,
                MedicalDeviceNameKz = contract.MedicalDeviceNameKz,
                HolderType = contract.HolderType,
                ContractType = contract.ContractType,
                ChoosePayer = contract.ChoosePayer,
                HasProxy = contract.HasProxy ?? false,
                DocumentType = contract.DocumentType ?? 0,
                DeclarantIsManufactur = (bool)contract.DeclarantIsManufactur,
                ContractScope = GetContractScopeCode(contractId),
                StatemantNumber = contract.StatemantNumber
            };
            if (contract.OBK_DeclarantManufactur != null)
            {
                Declarants manufactur = new Declarants
                {
                    Id = contract.OBK_DeclarantManufactur.Id,
                    Bin = contract.OBK_DeclarantManufactur.Bin,
                    CountryId = contract.OBK_DeclarantManufactur.CountryId,
                    IsResident = contract.OBK_DeclarantManufactur.IsResident,
                    NameRu = contract.OBK_DeclarantManufactur.NameRu,
                    NameKz = contract.OBK_DeclarantManufactur.NameKz,
                    NameEn = contract.OBK_DeclarantManufactur.NameEn,
                    OrganizationFormId = contract.OBK_DeclarantManufactur.OrganizationFormId
                };
                contractView.Manufactur = manufactur;

                if (contract.OBK_DeclarantContactManufactur != null)
                {
                    Contacts declarantContractManufactur = new Contacts
                    {
                        Id = contract.OBK_DeclarantContactManufactur.Id,
                        AddressFact = contract.OBK_DeclarantContactManufactur.AddressFact,
                        AddressLegalRu = contract.OBK_DeclarantContactManufactur.AddressLegalRu,
                        AddressLegalKz = contract.OBK_DeclarantContactManufactur.AddressLegalKz,
                        BankId = contract.OBK_DeclarantContactManufactur.BankId,
                        BankBik = contract.OBK_DeclarantContactManufactur.BankBik,
                        BankIik = contract.OBK_DeclarantContactManufactur.BankIik,
                        BankAccount = contract.OBK_DeclarantContactManufactur.BankAccount,
                        BankNameRu = contract.OBK_DeclarantContactManufactur.BankNameRu,
                        BankNameKz = contract.OBK_DeclarantContactManufactur.BankNameKz,
                        Phone = contract.OBK_DeclarantContactManufactur.Phone,
                        Phone2 = contract.OBK_DeclarantContactManufactur.Phone2,
                        Email = contract.OBK_DeclarantContactManufactur.Email,
                        CurrencyId = contract.OBK_DeclarantContactManufactur.CurrencyId,
                        BossPosition = contract.OBK_DeclarantContactManufactur.BossPosition,
                        BossLastName = contract.OBK_DeclarantContactManufactur.BossLastName,
                        BossFirstName = contract.OBK_DeclarantContactManufactur.BossFirstName,
                        BossMiddleName = contract.OBK_DeclarantContactManufactur.BossMiddleName,
                        BossDocNumber = contract.OBK_DeclarantContactManufactur.BossDocNumber,
                        BossDocType = contract.OBK_DeclarantContactManufactur.BossDocType,
                        IsHasBossDocNumber = contract.OBK_DeclarantContactManufactur.IsHasBossDocNumber,
                        BossDocCreatedDate = contract.OBK_DeclarantContactManufactur.BossDocCreatedDate,
                        BossPositionKz = contract.OBK_DeclarantContactManufactur.BossPositionKz
                        
                    };
                    contractView.Manufactur.Contact = declarantContractManufactur;
                }
            }
            if (contract.OBK_Declarant != null)
            {
                Declarants declarant = new Declarants
                {
                    Id = contract.OBK_Declarant.Id,
                    Bin = contract.OBK_Declarant.Bin,
                    CountryId = contract.OBK_Declarant.CountryId,
                    IsResident = contract.OBK_Declarant.IsResident,
                    NameRu = contract.OBK_Declarant.NameRu,
                    NameKz = contract.OBK_Declarant.NameKz,
                    NameEn = contract.OBK_Declarant.NameEn,
                    OrganizationFormId = contract.OBK_Declarant.OrganizationFormId
                };
                contractView.Declarant = declarant;

                if (contract.OBK_DeclarantContact != null)
                {
                    Contacts declarantContract = new Contacts
                    {
                        Id = contract.OBK_DeclarantContact.Id,
                        AddressFact = contract.OBK_DeclarantContact.AddressFact,
                        AddressLegalRu = contract.OBK_DeclarantContact.AddressLegalRu,
                        AddressLegalKz = contract.OBK_DeclarantContact.AddressLegalKz,
                        BankId = contract.OBK_DeclarantContact.BankId,
                        BankBik = contract.OBK_DeclarantContact.BankBik,
                        BankIik = contract.OBK_DeclarantContact.BankIik,
                        BankNameRu = contract.OBK_DeclarantContact.BankNameRu,
                        BankNameKz = contract.OBK_DeclarantContact.BankNameKz,
                        Phone = contract.OBK_DeclarantContact.Phone,
                        Phone2 = contract.OBK_DeclarantContact.Phone2,
                        Email = contract.OBK_DeclarantContact.Email,
                        CurrencyId = contract.OBK_DeclarantContact.CurrencyId,
                        BossPosition = contract.OBK_DeclarantContact.BossPosition,
                        BossLastName = contract.OBK_DeclarantContact.BossLastName,
                        BossFirstName = contract.OBK_DeclarantContact.BossFirstName,
                        BossMiddleName = contract.OBK_DeclarantContact.BossMiddleName,
                        BossDocNumber = contract.OBK_DeclarantContact.BossDocNumber,
                        BossDocType = contract.OBK_DeclarantContact.BossDocType,
                        IsHasBossDocNumber = contract.OBK_DeclarantContact.IsHasBossDocNumber,
                        BossDocCreatedDate = contract.OBK_DeclarantContact.BossDocCreatedDate,
                        BossPositionKz = contract.OBK_DeclarantContact.BossPositionKz,
                        BankAccount = contract.OBK_DeclarantContact.BankAccount,
                        BossDocUnlimited = contract.OBK_DeclarantContact.BossDocUnlimited,
                        BossDocEndDate = contract.OBK_DeclarantContact.BossDocEndDate
                    };
                    contractView.Declarant.Contact = declarantContract;
                }
            }
            if (contract.OBK_DeclarantPayer != null)
            {
                Declarants payer = new Declarants
                {
                    Id = contract.OBK_DeclarantPayer.Id,
                    Bin = contract.OBK_DeclarantPayer.Bin,
                    CountryId = contract.OBK_DeclarantPayer.CountryId,
                    IsResident = contract.OBK_DeclarantPayer.IsResident,
                    NameRu = contract.OBK_DeclarantPayer.NameRu,
                    NameKz = contract.OBK_DeclarantPayer.NameKz,
                    NameEn = contract.OBK_DeclarantPayer.NameEn,
                    OrganizationFormId = contract.OBK_DeclarantPayer.OrganizationFormId,
                    Iin = contract.OBK_DeclarantPayer.Iin
                };
                contractView.Payer = payer;
                if (contract.OBK_DeclarantContactPayer != null)
                {
                    Contacts declarantContractPayer = new Contacts
                    {
                        Id = contract.OBK_DeclarantContactPayer.Id,
                        AddressFact = contract.OBK_DeclarantContactPayer.AddressFact,
                        AddressLegalRu = contract.OBK_DeclarantContactPayer.AddressLegalRu,
                        AddressLegalKz = contract.OBK_DeclarantContactPayer.AddressLegalKz,
                        BankId = contract.OBK_DeclarantContactPayer.BankId,
                        BankBik = contract.OBK_DeclarantContactPayer.BankBik,
                        BankIik = contract.OBK_DeclarantContactPayer.BankIik,
                        BankNameRu = contract.OBK_DeclarantContactPayer.BankNameRu,
                        BankNameKz = contract.OBK_DeclarantContactPayer.BankNameKz,
                        Phone = contract.OBK_DeclarantContactPayer.Phone,
                        Phone2 = contract.OBK_DeclarantContactPayer.Phone2,
                        Email = contract.OBK_DeclarantContactPayer.Email,
                        CurrencyId = contract.OBK_DeclarantContactPayer.CurrencyId,
                        BossPosition = contract.OBK_DeclarantContactPayer.BossPosition,
                        BossLastName = contract.OBK_DeclarantContactPayer.BossLastName,
                        BossFirstName = contract.OBK_DeclarantContactPayer.BossFirstName,
                        BossMiddleName = contract.OBK_DeclarantContactPayer.BossMiddleName,
                        BossDocNumber = contract.OBK_DeclarantContactPayer.BossDocNumber,
                        BossDocType = contract.OBK_DeclarantContactPayer.BossDocType,
                        IsHasBossDocNumber = contract.OBK_DeclarantContactPayer.IsHasBossDocNumber,
                        BossDocCreatedDate = contract.OBK_DeclarantContactPayer.BossDocCreatedDate,
                        BossPositionKz = contract.OBK_DeclarantContactPayer.BossPositionKz,
                        BankAccount = contract.OBK_DeclarantContactPayer.BankAccount
                    };
                    contractView.Payer.Contact = declarantContractPayer;
                }
            }
            if (contract.EMP_CostWorks.Count > 0)
            {
                List<ServicePrice> servicePrices = new List<ServicePrice>();
                foreach (var costwork in contract.EMP_CostWorks)
                {
                    ServicePrice servicePrice = new ServicePrice
                    {
                        Id = costwork.Id,
                        ContractId = costwork.ContractId,
                        Price = (decimal)costwork.Price,
                        Count = (int)costwork.Count,
                        TotalPrice = (decimal)costwork.TotalPrice,
                        ServiceTypeNameRu = costwork.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameRu,
                        ServiceTypeNameKz = costwork.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameKz,
                        PriceListId = costwork.EMP_Ref_PriceList.Id
                    };
                    servicePrices.Add(servicePrice);
                }
                contractView.ServicePrices = servicePrices;
            }
            
            return contractView;
        }

        public void SendToCoz(Guid contractId)
        {
            var contract = AppContext.EMP_Contract.First(x => x.Id == contractId);

            if (contract.EMP_Ref_Status.Code == CodeConstManager.EmpContractStatus.Draft)
            {
                var cozStage = AppContext.EMP_Ref_Stage.Where(x => x.Code == CodeConstManager.EmpContractStage.Coz && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
                var validationGroupStage = AppContext.EMP_Ref_Stage.Where(x => x.Code == CodeConstManager.EmpContractStage.ValidationGroup && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
                var inQueueStageStatus = AppContext.EMP_Ref_StageStatus.Where(x => x.Code == CodeConstManager.EmpContractStageStatus.NotDistributed && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
                var inProcessingStatus = AppContext.EMP_Ref_Status.Where(x => x.Code == CodeConstManager.EmpContractStatus.InProcessing && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();

                var cozBoss = Guid.Parse(AppContext.Units.Where(x => x.Code == "coz").Select(x => x.BossId).FirstOrDefault());
                var validationGroupBoss = Guid.Parse(AppContext.Units.Where(x => x.Code == "ValidationGroup").Select(x => x.BossId).FirstOrDefault());

                contract.ContractStatusId = inProcessingStatus;

                Func<Guid, Guid, EMP_ContractStage> f = (stageId, bossId) =>
                {
                    var contractStage = new EMP_ContractStage
                    {
                        Id = Guid.NewGuid(),
                        ContractId = contract.Id,
                        StageId = stageId,
                        StageStatusId = inQueueStageStatus,
                        DateCreate = DateTime.Now
                    };

                    contractStage.EMP_ContractStageExecutors = new List<EMP_ContractStageExecutors>
                {
                    new EMP_ContractStageExecutors
                    {
                        Id = Guid.NewGuid(),
                        EMP_ContractStage = contractStage,
                        ExecutorId = bossId,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                    }
                };

                    return contractStage;
                };

                AppContext.EMP_ContractStage.Add(f(cozStage, cozBoss));
                AppContext.EMP_ContractStage.Add(f(validationGroupStage, validationGroupBoss));
            }
            else if (contract.EMP_Ref_Status.Code == CodeConstManager.EmpContractStatus.OnAdjustment)
            {
                contract.ContractStatusId = AppContext.EMP_Ref_Status
                    .Where(x => x.Code == CodeConstManager.EmpContractStatus.InWork).Select(x => x.Id).FirstOrDefault();
                var stage = contract.EMP_ContractStage.First(x => x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.LegalDepartmant);
                stage.StageStatusId = AppContext.EMP_Ref_StageStatus
                    .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.InWork).Select(x => x.Id)
                    .FirstOrDefault();
            }
            
            AppContext.SaveChanges();
        }

        public string GetContractScopeName(string code)
        {
            return AppContext.EMP_Ref_ContractScope.Where(x => x.Code == code).Select(x => x.NameRu).FirstOrDefault();
        }

        public string GetContractScopeCode(Guid contractId)
        {
            return AppContext.EMP_Ref_ContractScope
                .Where(e => e.EMP_Contract.Any(x => x.Id == contractId && x.ContractScopeId == e.Id))
                .Select(e => e.Code)
                .FirstOrDefault();
        }

        public object GetContractReportData(Guid id)
        {
            var contract = AppContext.EMP_Contract.FirstOrDefault(x => x.Id == id);
            if (contract == null) return null;

            var currency = AppContext.Dictionaries
                .Where(x => x.Type == "Currency" && x.Id == contract.OBK_DeclarantContactPayer.CurrencyId)
                .Select(x => new {x.Name, x.NameKz}).FirstOrDefault();

            Func<string, string> marked = s => string.Format("<b><u>{0}</u></b>", s);
            //Func<string, string, string, string> getString = (reg, reReg, change) =>
            //    string.Format("{0} {1} {2}",
            //        contract.EMP_Ref_ContractType.Code == CodeConstManager.EmpContractType.Registration ? marked(reg) : reg,
            //        contract.EMP_Ref_ContractType.Code == CodeConstManager.EmpContractType.ReRegistration ? marked(reReg) : reReg,
            //        contract.EMP_Ref_ContractType.Code == CodeConstManager.EmpContractType.ChangesInsertion ? marked(change) : change);
            Func<string, string, string> append = (result, appendStr) =>
            {
                if (string.IsNullOrWhiteSpace(appendStr)) return result;
                if (string.IsNullOrWhiteSpace(result))
                    result = appendStr;
                else result += string.Format(", ({0})", appendStr);
                return result;
            };

            string registrationRu = "регистрации,";
            string reRegistrationRu = "перерегистрации,";
            string changesInsertionRu = "внесении изменений в регистрационное досье";

            string registrationKz = "тіркеу,";
            string reRegistrationKz = "қайта тіркеу,";
            string changesInsertionKz = "және тіркеу дерекнамасына өзгерістер енгізу";

            string declarantName = append(append(contract.OBK_Declarant.NameKz, contract.OBK_Declarant.NameRu), contract.OBK_Declarant.NameEn);
            var obj = new
            {
                DeclarantNameRu = string.Format("{0} {1}", 
                    AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId).Select(x => x.Name).FirstOrDefault(), 
                    declarantName),
                DeclarantNameKz = string.Format("{0} {1}",
                    AppContext.Dictionaries.Where(x => x.Id == contract.OBK_Declarant.OrganizationFormId).Select(x => x.NameKz).FirstOrDefault(),
                    declarantName),
                DeclarantBossLastName = contract.OBK_DeclarantContact.BossLastName,
                DeclarantBossFirstName = contract.OBK_DeclarantContact.BossFirstName,
                DeclarantBossMiddleName = contract.OBK_DeclarantContact.BossMiddleName,
                DeclarantBossPositionNameRu = contract.OBK_DeclarantContact.BossPosition,
                DeclarantBossPositionNameKz = contract.OBK_DeclarantContact.BossPositionKz,
                DeclarantBossDocumentType = contract.DocumentType == 1 ? "Представительство" : contract.DocumentType == 2 ? "Доверенное лицо" : string.Empty,
                //ContractTypeStringRu = getString(registrationRu, reRegistrationRu, changesInsertionRu),
                //ContractTypeStringKz = getString(registrationKz, reRegistrationKz, changesInsertionKz),
                CurrencyNameRu = currency != null ? currency.Name : string.Empty,
                CurrencyNameKz = currency != null ? currency.NameKz : string.Empty,
                TotalPrice = contract.EMP_CostWorks.Select(x => x.TotalPrice).Sum() ?? 0,
                DeclarantInfoStringRu = GetDeclarantString(contract, true),
                DeclarantInfoStringKz = GetDeclarantString(contract, false),
                MedicalDeviceNameRu = contract.MedicalDeviceName,
                contract.MedicalDeviceNameKz,
                CostWorks = contract.EMP_CostWorks.Select(cw => new
                {
                    Name = cw.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameRu,
                    Price = cw.Price ?? 0,
                    Count = cw.Count ?? 0,
                    TotalPrice = cw.TotalPrice ?? 0
                })
            };

            return obj;
        }

        private string GetDeclarantString(EMP_Contract contract, bool isRus)
        {
            Func<string, string> strWithComma = s => string.IsNullOrWhiteSpace(s) ? string.Empty : string.Format("{0}, ", s);
            Func<OBK_Declarant, OBK_DeclarantContact, bool, string> declarantInfo = (declarant, contact, isRu) =>
            {
                var orgForm = AppContext.Dictionaries.Where(x => x.Id == declarant.OrganizationFormId).Select(x => new { x.Name, x.NameKz }).First();
                var cur = AppContext.Dictionaries.Where(x => x.Id == contact.CurrencyId).Select(x => new { x.Name, x.NameKz }).First();
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    strWithComma(isRu ? orgForm.Name : orgForm.NameKz),
                    strWithComma(isRu ? contact.AddressLegalRu : contact.AddressLegalKz),
                    strWithComma(isRu ? contact.BankNameRu : contact.BankNameKz),
                    strWithComma(contact.BankAccount),
                    strWithComma(isRu ? cur.Name : cur.NameKz),
                    strWithComma(contact.BankBik),
                    strWithComma(contact.Phone)).TrimEnd().TrimEnd(',');
            };

            string manufacturStr = isRus ? "Производитель" : "Өндіруші";
            string declarantStr = isRus ? "Заявитель" : "Өтініш беруші";
            string payerStr = isRus ? "Плательщик" : "Толеуші";

            string declarantInfoString;
            if (contract.DeclarantIsManufactur == true)
            {
                if (contract.ChoosePayer == "Declarant")
                {
                    declarantInfoString = string.Format("<b>"+ declarantStr + "</b><br/>{0}",
                        declarantInfo(contract.OBK_Declarant, contract.OBK_DeclarantContact, isRus));
                }
                else
                {
                    declarantInfoString = string.Format("<b>"+ declarantStr + "/"+ manufacturStr + "</b><br/>{0}<br/><b>"+ payerStr + "</b><br/>{1}",
                        declarantInfo(contract.OBK_Declarant, contract.OBK_DeclarantContact, isRus),
                        declarantInfo(contract.OBK_DeclarantPayer, contract.OBK_DeclarantContactPayer, isRus));
                }
            }
            else
            {
                declarantInfoString = string.Format(
                    "<b>"+ manufacturStr + "</b><br/>{0}<br/><b>"+ payerStr + "</b><br/>{1}<br/><b>"+ declarantStr + "</b><br/>{2}",
                    declarantInfo(contract.OBK_DeclarantManufactur, contract.OBK_DeclarantContactManufactur, isRus),
                    declarantInfo(contract.OBK_DeclarantPayer, contract.OBK_DeclarantContactPayer, isRus),
                    declarantInfo(contract.OBK_Declarant, contract.OBK_DeclarantContact, isRus));
            }
            return declarantInfoString;
        }

        public string GetDataForSign(Guid id)
        {
            var contract = AppContext.EMP_Contract.First(e => e.Id == id);

            Func<OBK_Declarant, OBK_DeclarantContact, EmpContractDeclarantSignData> f = (declarant, declarantContact) => new EmpContractDeclarantSignData
            {
                IsResident = declarant.IsResident,
                NameKz = declarant.NameKz,
                NameRu = declarant.NameRu,
                NameEn = declarant.NameEn,
                Bin = declarant.Bin,
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
                BankBik = declarantContact.BankBik,
                Iin = declarant.Iin,
                BankAccount = declarantContact.BankAccount,
            };

            var dataForSign = new EmpContractSignData
            {
                Manufacturer = f(contract.OBK_DeclarantManufactur, contract.OBK_DeclarantContactManufactur),
                Declarant = f(contract.OBK_Declarant, contract.OBK_DeclarantContact),
                Payer = f(contract.OBK_DeclarantPayer, contract.OBK_DeclarantContactPayer),
                ChoosPayer = contract.ChoosePayer,
                MedicalDeviceName = contract.MedicalDeviceName,
                WorkCosts = contract.EMP_CostWorks.Select(x => new EmpContractWorkCostsSignData
                {
                    WorkName = x.EMP_Ref_PriceList.EMP_Ref_ServiceType.NameRu,
                    Price = x.Price ?? 0,
                    Count = x.Count ?? 0
                }).ToList()
            };

            var xmlData = SerializeHelper.SerializeDataContract(dataForSign);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public void SignContractApplicant(Guid contractId, string signedData)
        {
            var data = AppContext.EMP_ContractSignData.FirstOrDefault(x => x.ContractId == contractId);
            if (data == null)
            {
                data = new EMP_ContractSignData {ContractId = contractId};
                AppContext.EMP_ContractSignData.Add(data);
            }

            data.ApplicationSign = signedData;
            data.ApplicationSignDate = DateTime.Now;
            data.CeoSign = null;
            data.CeoSignDate = null;

            SendToCoz(contractId);
        }

        public IEnumerable<EMPRegister> GetSearchReestr(string drugRegNumber, string drugTradeName)
        {
            //List<EMP_Register> list = new List<EMP_Register>();
            //var queryString = "SELECT * FROM sr_register WHERE id = @drugTradeName;";
            //var conString = GetConnectionString();
            //using (SqlConnection con = new SqlConnection(conString))
            //{
            //    SqlCommand command = new SqlCommand(queryString, con);
            //    command.Parameters.AddWithValue("@drugTradeName", 57779);
            //    con.Open();
            //    SqlDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        var regNumber = (string)reader["reg_number"];
            //        var regName = (string)reader["name"];
            //        var register = new EMP_Register { regNumber = regNumber, regName = regName };
            //        list.Add(register);
            //    }
            //    reader.Close();
            //}
            //return list;

            var reestr = from register in AppContext.sr_register
                where (string.IsNullOrEmpty(drugRegNumber) || register.reg_number.ToLower().Contains(drugRegNumber.ToLower())) &&
                      (string.IsNullOrEmpty(drugTradeName) || register.name.ToLower().Contains(drugTradeName.ToLower()))
                select new EMPRegister
                {
                    Id = register.id,
                    RegNumberRu = register.reg_number,
                    RegNumberKz = register.reg_number_kz,
                    RegNameRu = register.name,
                    RegNameKz = register.name_kz,
                    RegProducerNameRu = register.C_producer_name,
                    RegProducerNameKz = register.C_producer_name_kz,
                    RegCounrtyNameRu = register.C_country_name,
                    RegCounrtyNameKz = register.C_country_name_kz
                };
            return reestr;
        }

        private static string GetConnectionString()
        {
            var conString = ConfigurationManager.ConnectionStrings["register_portal"].ToString();
            return conString;
        }
    }
}
