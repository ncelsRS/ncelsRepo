using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var scs = await Task.Run(()=> _refRepo.StorageCondition());
            return scs.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public IEnumerable<ReferenceModel> GetClassifierMedicalArea()
        {
            var cmas = _refRepo.ClassifierMedicalArea();
            return cmas.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public IEnumerable<NomenclatureCodeMedProductModel> GetNomenclatureCodeMedProduct(string name, string culture)
        {
            var ncmps = _refRepo.NomenclatureCodeMedProduct(name, culture);
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

        public IEnumerable<object> GetDegreeRiskClass()
        {
            var drcs = _refRepo.DegreeRiskClass();
            return drcs.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public IEnumerable<object> GetOrganizationForm()
        {
            var ofs = _refRepo.OrganizationForm();
            return ofs.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public int SaveOrganizationForm(string nameRu, string nameKz)
        {
            return _refRepo.SaveOrganizationForm(nameRu, nameKz, false);
        }

        public IEnumerable<object> GetBank()
        {
            var banks = _refRepo.Banks();
            return banks.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public int SaveBank(string nameRu, string nameKz)
        {
            return _refRepo.SaveBank(nameRu, nameKz, false);
        }

        public IEnumerable<object> GetCurrency()
        {
            var currencies = _refRepo.Currency();
            return currencies.Select(x => new ReferenceModel
            {
                Id = x.Id,
                Code = x.Code,
                NameKz = x.NameRu,
                NameRu = x.NameRu
            });
        }

        public IEnumerable<object> GetCountry()
        {
            var countries = _refRepo.Country();
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
