using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class MaterialRepository : ARepositoryGeneric<EXP_Materials>
    {
        public MaterialRepository(bool isProxy = true):base(isProxy)
        { }
        
        public IQueryable<sr_dosage_forms> GetDosageForm(Expression<Func<sr_dosage_forms, bool>> filter = null)
        {
            if (filter != null) return AppContext.sr_dosage_forms.Where(filter);
            return AppContext.sr_dosage_forms.AsQueryable();
        }

        public IQueryable<DIC_Storages> GetStorage(Expression<Func<DIC_Storages, bool>> filter = null)
        {
            if (filter != null) return AppContext.DIC_Storages.Where(filter);
            return AppContext.DIC_Storages.AsQueryable();
        }

        public IQueryable<Dictionary> GetMaterialType(Expression<Func<Dictionary, bool>> filter = null)
        {
            if (filter != null) return AppContext.Dictionaries.Where(filter);
            return AppContext.Dictionaries.AsQueryable();
        }

        public IQueryable<EXP_DrugDeclaration> GetDrugDeclaration(Expression<Func<EXP_DrugDeclaration, bool>> filter = null)
        {
            if (filter != null) return AppContext.EXP_DrugDeclaration.Where(filter);
            return AppContext.EXP_DrugDeclaration.AsQueryable();
        }

        public bool GetIsControlFormDrugDeclaration(Guid drugDeclarationId)
        {
            var isControlList = AppContext.EXP_DrugSubstance.Where(ds => ds.EXP_DrugDosage.EXP_DrugDeclaration.Id == drugDeclarationId)
                .Select(d => d.IsControl).ToList();

            bool isControl = false;

            foreach (var isc in isControlList)
                isControl |= isc;

            return isControl;
        }


        public IQueryable<Organization> GetOrganizations(Expression<Func<Organization, bool>> filter = null)
        {
            if (filter != null) return AppContext.Organizations.Where(filter);
            return AppContext.Organizations.AsQueryable();
        }
        
        public Organization GetManufactureOrganization(Guid contractId)
        {
            var contract = AppContext.Contracts.Include(c => c.ManufacturerOrganization).FirstOrDefault(c => c.Id == contractId);
            return contract?.ManufacturerOrganization;
        }
    }
}
