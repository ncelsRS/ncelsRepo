using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.EMP;
using PW.Ncels.Database.Constants;

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

        public List<Dictionary> GetHolderTypes()
        {
            return AppContext.Dictionaries.Where(e => e.Type == "ContractHolderType").ToList();
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

        private void FillDeclarant(Declarations declaration, EMP_Contract obkContract)
        {
            OBK_Declarant declarant = new OBK_Declarant
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
            obkContract.OBK_Declarant = declarant;
            declaration.Id = declarant.Id;
        }

        private void FillDeclarantDocntract(Declarations declaration, EMP_Contract obkContract)
        {
            OBK_DeclarantContact declarantContact = new OBK_DeclarantContact
            {
                DeclarantId = obkContract.DeclarantId,
                AddressLegalRu = declaration.AddressLegalRu,
                AddressLegalKz = declaration.AddressLegalKz,
                AddressFact = declaration.AddressFact,
                Phone = declaration.Phone,
                Email = declaration.Email,
                BankNameRu = declaration.BankNameRu,
                BankNameKz = declaration.BankNameKz,
                BankIik = declaration.BankIik,
                BankBik = declaration.BankBik,
                CurrencyId = declaration.CurrencyId,
                BossFio = declaration.BossLastName + " " + declaration.BossFirstName + " " +
                          declaration.BossMiddleName,
                BossPosition = declaration.BossPosition,
                BossLastName = declaration.BossLastName,
                BossFirstName = declaration.BossFirstName,
                BossMiddleName = declaration.BossMiddleName,
                BossDocNumber = declaration.BossDocNumber,
                BossDocType = declaration.BossDocType,
                IsHasBossDocNumber = declaration.IsHasBossDocNumber,
                BossDocCreatedDate = declaration.BossDocCreatedDate,
                SignLastName = declaration.SignLastName,
                SignFirstName = declaration.SignFirstName,
                SignMiddleName = declaration.SignMiddleName,
                SignPosition = declaration.SignPosition,
                SignDocType = declaration.SignDocType,
                IsHasSignDocNumber = declaration.IsHasSignDocNumber,
                SignDocNumber = declaration.SignDocNumber,
                SignDocCreatedDate = declaration.SignDocCreatedDate,
                CreateDate = DateTime.Now,
                SignDocEndDate = declaration.SignDocEndDate,
                SignDocUnlimited = declaration.SignDocUnlimited,
                BossDocEndDate = declaration.BossDocEndDate,
                BossDocUnlimited = declaration.BossDocUnlimited,
                SignerIsBoss = declaration.SignerIsBoss,
                SignPositionKz = declaration.SignPositionKz,
                BossPositionKz = declaration.BossPositionKz
            };
            obkContract.OBK_DeclarantContact = declarantContact;

            declaration.DeclarantContractId = declarantContact.Id;
        }

        private void FillContract(Declarations declaration, bool isNew, string type, EMP_Contract obkContract)
        {
            var dec = AppContext.OBK_Declarant.FirstOrDefault(e=>e.Id == declaration.Id);
            if (dec == null) {
                FillDeclarant(declaration, obkContract);
                FillDeclarantDocntract(declaration, obkContract);
            }
            else {
                obkContract.DeclarantId = dec.Id;
            }

            var decContract = dec?.OBK_DeclarantContact.FirstOrDefault();
            if (decContract == null)
            {
                
            }
            else
            {
                obkContract.DeclarantContactId = decContract.Id;
            }
            //switch (type)
            //{
            //    case "Manufactur":
            //        obkContract.OBK_DeclarantContactManufactur = declarantContact;
            //        break;
            //    case "Declarant":
            //        obkContract.OBK_DeclarantContact = declarantContact;
            //        break;
            //    case "Payer":
            //        obkContract.OBK_DeclarantContactPayer = declarantContact;
            //        break;
            //}

        }

        public EMPContractViewModel SaveContract(Guid Guid, EMPContractViewModel contractViewModel)
        {
            
            var view = new EMPContractViewModel();
            bool isNew = false;
            var model = GetById(contractViewModel.Id);
            if (model == null)
            {
                isNew = true;
                EMP_Contract contract = new EMP_Contract();

                contract.Id = Guid;
                contract.EmployeeId = UserHelper.GetCurrentEmployee().Id;
                contract.Number = "б/н";
                contract.CreatedDate = DateTime.Now;
                contract.Status = CodeConstManager.STATUS_DRAFT_ID;
                contract.MedicalDeviceName = contractViewModel.MedicalDeviceName;

                if (contractViewModel.Declarant != null)
                {
                    FillContract(contractViewModel.Declarant, isNew, "Declarant", contract);
                }
                if (contractViewModel.Manufactur != null)
                {
                    FillContract(contractViewModel.Manufactur, isNew, "Manufactur", contract);
                }
                if (contractViewModel.Payer != null)
                {
                    FillContract(contractViewModel.Payer, isNew, "Payer", contract);
                }
                AppContext.EMP_Contract.Add(contract);
                AppContext.SaveChanges();
                view.Id = contract.Id;

            }
            else
            {
                EMP_Contract contract = new EMP_Contract();
                contract.EmployeeId = UserHelper.GetCurrentEmployee().Id;
                contract.Number = "б/н";
                contract.CreatedDate = DateTime.Now;
                contract.Status = CodeConstManager.STATUS_DRAFT_ID;
                contract.MedicalDeviceName = contractViewModel.MedicalDeviceName;

                if (contractViewModel.Declarant != null)
                {
                    FillContract(contractViewModel.Declarant, isNew, "Declarant", contract);
                }
                if (contractViewModel.Manufactur != null)
                {
                    FillContract(contractViewModel.Manufactur, isNew, "Manufactur", contract);
                }
                if (contractViewModel.Payer != null)
                {
                    FillContract(contractViewModel.Payer, isNew, "Payer", contract);
                }
                view.Id = contract.Id;
            }
            return view;
        }


        //public EMPContractViewModel SaveContract2(Guid Guid, EMPContractViewModel contractViewModel)
        //{
        //    var view = new EMPContractViewModel();
        //    bool isNew = false;
        //    var model = GetById(contractViewModel.Id);
        //    if (model == null)
        //    {
        //        isNew = true;
        //        EMP_Contract contract = new EMP_Contract();

        //        contract.Id = Guid;
        //        contract.EmployeeId = UserHelper.GetCurrentEmployee().Id;
        //        contract.Number = "б/н";
        //        contract.CreatedDate = DateTime.Now;
        //        contract.Status = CodeConstManager.STATUS_DRAFT_ID;
        //        contract.MedicalDeviceName = contractViewModel.MedicalDeviceName;
        //    }



        //    return view;
        //}

        private EMP_Contract GetById(Guid modelId)
        {
            return AppContext.EMP_Contract.FirstOrDefault(e => e.Id == modelId);
        }

        public List<object> GetServiceType()
        {
            List<object> serviceTypes = new List<object>();
            object noServiceType = new {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                NameKz = "Нет данных",
                NameRu = "Нет данных"
            };
            serviceTypes.Add(noServiceType);
            var result = EmpReferenceHelper.GetServiceType().Where(e=>e.ParentId == null).Select(e=> new {e.Id, e.NameRu, e.NameKz, e.ParentId});
            serviceTypes.AddRange(result);
            return serviceTypes;
        }

        public List<object> GetServiceTypeParentId(string id) {
            List<object> serviceTypes = new List<object>();
            object noServiceType = new {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                NameKz = "Нет данных",
                NameRu = "Нет данных"
            };
            serviceTypes.Add(noServiceType);
            var result = EmpReferenceHelper.GetServiceTypeParentId(Guid.Parse(id)).Select(e => new { e.Id, e.NameRu, e.NameKz, e.ParentId });
            serviceTypes.AddRange(result);
            return serviceTypes;
        }

        public List<object> GetPriceTypeWhithPriceList(string id)
        {
            List<object> priceTypes = new List<object>();
            object noPriceType = new
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                NameKz = "Нет данных",
                NameRu = "Нет данных"
            };
            priceTypes.Add(noPriceType);
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

        public List<object> GetPriceList(Guid serviceTypeId, bool isImport, int count)
        {
            var result =
                AppContext.EMP_Ref_PriceList.FirstOrDefault(
                    e => e.ServiceTypeId == serviceTypeId && e.Import == isImport);

            if (result != null)
            {
                var totalCount = TaxHelper.GetCalculationTax((decimal)result.Price) * count;
                List<object> resultList = new List<object>();
                object returnResult = new
                {
                    result.Id,
                    result.Price,
                    Сount = count,
                    TotalCount = totalCount,
                    ServiceTypeNameRu = result.EMP_Ref_ServiceType.NameRu,
                    ServiceTypeNameKz = result.EMP_Ref_ServiceType.NameKz
                };
                resultList.Add(returnResult);
                return resultList;
            }
            return null;
        }
    }
}
