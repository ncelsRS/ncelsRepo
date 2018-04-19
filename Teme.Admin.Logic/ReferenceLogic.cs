using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Admin.Data.Model;
using Teme.Admin.Data.Repository;
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
    }
}
