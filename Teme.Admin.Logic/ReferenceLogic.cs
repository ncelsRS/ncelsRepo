using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Admin.Data.Model;
using Teme.Admin.Data.Repository;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Admin.Logic
{
    public class ReferenceLogic
    {
        private readonly IReferenceRepo _refRepo;

        public ReferenceLogic(IReferenceRepo refRepo)
        {
            _refRepo = refRepo;
        }

        public async Task<IEnumerable<ReferenceModel>> GetStorageCondition()
        {
            var scs = await _refRepo.StorageConditionAsync();
            return scs.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<IEnumerable<ReferenceModel>> GetClassifierMedicalArea()
        {
            var cmas = await _refRepo.ClassifierMedicalAreaAsync();
            return cmas.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<IEnumerable<NomenclatureCodeMedProductModel>> GetNomenclatureCodeMedProduct(string name, string culture)
        {
            var ncmps = await _refRepo.NomenclatureCodeMedProductAsync(name, culture);
            return ncmps.Select(x => new NomenclatureCodeMedProductModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameKz,
                NameRu = x.NameRu,
                DefenitionNameKz = x.DefenitionNameKz,
                DefenitionNameRu = x.DefenitionNameRu
            });
        }

        public IEnumerable<object> GetRegistrationType()
        {
            var enumVals = new List<object>();
            foreach (var i in Enum.GetValues(typeof(RegistrationTypeEnum)))
            {
                enumVals.Add(new {
                    key = i.GetHashCode(),
                    value = i.ToString()
                });
            }
            return enumVals;
        }

        public async Task<IEnumerable<object>> GetDegreeRiskClass()
        {
            var drcs = await _refRepo.DegreeRiskClassAsync();
            return drcs.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<IEnumerable<object>> GetOrganizationForm()
        {
            var ofs = await _refRepo.OrganizationFormAsync();
            return ofs.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<int> SaveOrganizationForm(string nameRu, string nameKz)
        {
            return await _refRepo.SaveOrganizationFormAsync(nameRu, nameKz, false);
        }

        public async Task<IEnumerable<object>> GetBank()
        {
            var banks = await _refRepo.BanksAsync();
            return banks.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<int> SaveBank(string nameRu, string nameKz)
        {
            return await _refRepo.SaveBankAsync(nameRu, nameKz, false);
        }

        public async Task<IEnumerable<object>> GetCurrency()
        {
            var currencies = await _refRepo.CurrencyAsync();
            return currencies.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<IEnumerable<object>> GetCountry()
        {
            var countries = await _refRepo.CountryAsync();
            return countries.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public IEnumerable<object> GetHolderType()
        {
            var enumVals = new List<object>();
            foreach (var i in Enum.GetValues(typeof(HolderTypeEnum)))
            {
                enumVals.Add(new
                {
                    key = i.GetHashCode(),
                    value = i.ToString()
                });
            }
            return enumVals;
        }

        public IEnumerable<object> GetContractForm()
        {
            var enumVals = new List<object>();
            foreach (var i in Enum.GetValues(typeof(ContractFormEnum)))
            {
                enumVals.Add(new
                {
                    key = i.GetHashCode(),
                    value = i.ToString()
                });
            }
            return enumVals;
        }

        public async Task<IQueryable<object>> GetCalculatorApplicationType(string contractScope, int contractForm)
        {
            var cats = await _refRepo.CalculatorApplicationType(contractScope, contractForm.ToString());
            return cats.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public async Task<IEnumerable<object>> GetCalculatorServiceType(int applicationTypeId)
        {
            var csts = await _refRepo.CalculatorServiceType(applicationTypeId);

            List<CalculatorServiceTypeModel> list = new List<CalculatorServiceTypeModel>();
            foreach (var cst in csts)
            {
                var result = new CalculatorServiceTypeModel
                {
                    Id = cst.Id,
                    NameRu = cst.NameRu,
                    NameKz = cst.NameKz,
                    Children = cst.Children?.Count > 0,
                    CalculatorServiceTypeModificationModels = cst.Children?.Count > 0 ? cst.Children?.Select(x => new CalculatorServiceTypeModificationModel
                    {
                        Id = x.Id,
                        NameKz = x.NameKz,
                        NameRu = x.NameRu
                    }) : null
                };
                list.Add(result);
            }
            return list;
        }

        public async Task<object> GetShowPrice(bool isImport, int serviceTypeId, int? serviceTypeModifId)
        {
            var valueAddedTax = await _refRepo.GetValueAddedTax(DateTime.Now.Year);
            if (valueAddedTax == null)
                return null;
            var priceList = await _refRepo.GetPriceList(serviceTypeId, isImport);
            if (priceList == null)
                return null;
            if (serviceTypeModifId != null) {
                var priceListModif = await _refRepo.GetPriceList((int)serviceTypeModifId, isImport);
                return new PriceListModel
                {
                    Id = priceList.Id,
                    Price = priceList.Price,
                    ValueAddedTax = valueAddedTax.Value,
                    IsImport = priceList.IsImport,
                    priceListModificationModels = new PriceListModificationModel
                    {
                        Id = priceListModif.Id,
                        Price = priceListModif.Price,
                        ValueAddedTax = valueAddedTax.Value,
                        IsImport = priceListModif.IsImport
                    }
                };
            }
            return new PriceListModel
            {
                Id = priceList.Id,
                Price = priceList.Price,
                ValueAddedTax = valueAddedTax.Value,
                IsImport = priceList.IsImport
            };
        }

    }
}
