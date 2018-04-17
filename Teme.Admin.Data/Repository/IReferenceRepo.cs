using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Admin.Data.Model;
using Teme.Shared.Data.Context.References;

namespace Teme.Admin.Data.Repository
{
    public interface IReferenceRepo
    {
        IQueryable<Ref_StorageCondition> StorageCondition();
        IQueryable<Ref_ClassifierMedicalArea> ClassifierMedicalArea();
        IQueryable<Ref_NomenclatureCodeMedProduct> NomenclatureCodeMedProduct(string name, string culture);
        IQueryable<Ref_DegreeRiskClass> DegreeRiskClass();
        IQueryable<Ref_OrganizationForm> OrganizationForm();
        int SaveOrganizationForm(string nameRu, string nameKz, bool isConfirmed);
        IQueryable<Ref_Bank> Banks();
        int SaveBank(string nameRu, string nameKz, bool isConfirmed);
        IQueryable<Ref_Currency> Currency();
        IQueryable<Ref_Country> Country();
    }
}