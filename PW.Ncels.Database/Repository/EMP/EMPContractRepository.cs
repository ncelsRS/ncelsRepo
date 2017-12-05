using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.EMP;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Models;
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

        /// <summary>
        /// загрузка списка заявлений
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isRegisterProject"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<object> GetContractList(ModelRequest request, bool isRegisterProject)
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
                        Status = "Черновик",//x.Status.NameRu,
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

        public List<Dictionary> GetHolderTypes()
        {
            return AppContext.Dictionaries.Where(e => e.Type == "ContractHolderType" && e.Code != "holder").ToList();
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

        public EMPContractViewModel SaveContract(Guid Guid, EMPContractViewModel contractViewModel)
        {
            var model = GetById(contractViewModel.Id);
            if (model == null)
            {
                EMP_Contract contract = new EMP_Contract();

                contract.Id = Guid;
                contract.EmployeeId = UserHelper.GetCurrentEmployee().Id;
                contract.Number = "б/н";
                contract.CreatedDate = DateTime.Now;
                contract.Status = CodeConstManager.STATUS_DRAFT_ID;
                contract.MedicalDeviceName = contractViewModel.MedicalDeviceName;

                if (contractViewModel.Manufactur != null) {
                    FillDec(contractViewModel.Manufactur, "Manufactur", contract);
                    if (contractViewModel.Manufactur.Contact != null) {
                        FillDecContact(contractViewModel.Manufactur.Contact, contractViewModel.Manufactur.Id, contract, "Manufactur");
                    }
                }
                if (contractViewModel.Declarant != null) {
                    FillDec(contractViewModel.Declarant, "Declarant", contract);
                    if (contractViewModel.Declarant.Contact != null) {
                        FillDecContact(contractViewModel.Declarant.Contact, contractViewModel.Declarant.Id, contract, "Declarant");
                    }
                }
                if (contractViewModel.Payer != null) {
                    FillDec(contractViewModel.Payer, "Payer", contract);
                    if (contractViewModel.Payer.Contact != null) {
                        FillDecContact(contractViewModel.Payer.Contact, contractViewModel.Payer.Id, contract, "Payer");
                    }
                }
                if (contractViewModel.ServicePrices?.Count > 0) {
                    FillCostWork(contractViewModel.ServicePrices, contract);
                }
                contractViewModel.Id = contract.Id;
                AppContext.EMP_Contract.Add(contract);
                AppContext.SaveChanges();
            } else {

                model.MedicalDeviceName = contractViewModel.MedicalDeviceName;

                if (contractViewModel.Manufactur != null) {
                    FillDec(contractViewModel.Manufactur, "Manufactur", model);
                    if (contractViewModel.Manufactur.Contact != null) {
                        FillDecContact(contractViewModel.Manufactur.Contact, contractViewModel.Manufactur.Id, model, "Manufactur");
                    }
                }
                if (contractViewModel.Declarant != null) {
                    FillDec(contractViewModel.Declarant, "Declarant", model);
                    if (contractViewModel.Declarant.Contact != null) {
                        FillDecContact(contractViewModel.Declarant.Contact, contractViewModel.Declarant.Id, model, "Declarant");
                    }
                }
                if (contractViewModel.Payer != null) {
                    FillDec(contractViewModel.Payer, "Payer", model);
                    if (contractViewModel.Payer.Contact != null) {
                        FillDecContact(contractViewModel.Payer.Contact, contractViewModel.Payer.Id, model, "Payer");
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
                    IsResident = declaration.IsResident
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
                    Email = contact.Email,
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
                declarantContact.Email = contact.Email;
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

        public List<object> GetServiceType()
        {
            List<object> serviceTypes = new List<object> {NoData()};
            var result = EmpReferenceHelper.GetServiceType().Where(e=>e.ParentId == null).Select(e=> new {e.Id, e.NameRu, e.NameKz, e.ParentId, e.ChangeType});
            serviceTypes.AddRange(result);
            return serviceTypes;
        }

        public List<object> GetServiceTypeParentId(string id)
        {
            List<object> serviceTypes = new List<object> { NoData() };
            var result = EmpReferenceHelper.GetServiceTypeParentId(Guid.Parse(id)).Select(e => new { e.Id, e.NameRu, e.NameKz, e.ParentId });
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
                MedicalDeviceName = contract.MedicalDeviceName
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
                        BankBik = contract.OBK_DeclarantContactManufactur.BankBik,
                        BankIik = contract.OBK_DeclarantContactManufactur.BankIik,
                        BankNameRu = contract.OBK_DeclarantContactManufactur.BankNameRu,
                        BankNameKz = contract.OBK_DeclarantContactManufactur.BankNameKz,
                        Phone = contract.OBK_DeclarantContactManufactur.Phone,
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
                        BankBik = contract.OBK_DeclarantContact.BankBik,
                        BankIik = contract.OBK_DeclarantContact.BankIik,
                        BankNameRu = contract.OBK_DeclarantContact.BankNameRu,
                        BankNameKz = contract.OBK_DeclarantContact.BankNameKz,
                        Phone = contract.OBK_DeclarantContact.Phone,
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
                        BossPositionKz = contract.OBK_DeclarantContact.BossPositionKz
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
                    OrganizationFormId = contract.OBK_DeclarantPayer.OrganizationFormId
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
                        BankBik = contract.OBK_DeclarantContactPayer.BankBik,
                        BankIik = contract.OBK_DeclarantContactPayer.BankIik,
                        BankNameRu = contract.OBK_DeclarantContactPayer.BankNameRu,
                        BankNameKz = contract.OBK_DeclarantContactPayer.BankNameKz,
                        Phone = contract.OBK_DeclarantContactPayer.Phone,
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
                        BossPositionKz = contract.OBK_DeclarantContactPayer.BossPositionKz
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
    }
}
