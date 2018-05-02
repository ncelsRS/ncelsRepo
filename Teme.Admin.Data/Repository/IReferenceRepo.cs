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
        Task<IQueryable<Ref_ApplicationType>> CalculatorApplicationType(string contractScope, string contractForm);
        Task<IQueryable<Ref_ServiceType>> CalculatorServiceType(int applicationTypeId);
        Task<Ref_ValueAddedTax> GetValueAddedTax(int year);
        Task<Ref_PriceList> GetPriceList(int id, bool isImport);
        Task<IEnumerable<Ref_EquipmentType>> EquipmentTypeAsync();
        Task<IEnumerable<Ref_PackagingType>> PackagingTypeAsync();
        Task<IEnumerable<Ref_Measure>> MeasureAsync();
  }
}
