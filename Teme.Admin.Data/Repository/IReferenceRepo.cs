using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Context.References;

namespace Teme.Admin.Data.Repository
{
    public interface IReferenceRepo
    {
        Task<IEnumerable<Ref_Bank>> BanksAsync();
        Task<IEnumerable<Ref_ClassifierMedicalArea>> ClassifierMedicalAreaAsync();
        Task<IEnumerable<Ref_Country>> CountryAsync();
        Task<IEnumerable<Ref_Currency>> CurrencyAsync();
        Task<IEnumerable<Ref_DegreeRiskClass>> DegreeRiskClassAsync();
        Task<IQueryable<Ref_NomenclatureCodeMedProduct>> NomenclatureCodeMedProductAsync(string name, string culture);
        Task<IEnumerable<Ref_OrganizationForm>> OrganizationFormAsync();
        Task<int> SaveBankAsync(string nameRu, string nameKz, bool isConfirmed);
        Task<int> SaveOrganizationFormAsync(string nameRu, string nameKz, bool isConfirmed);
        Task<IEnumerable<Ref_StorageCondition>> StorageConditionAsync();
    }
}