using Microsoft.EntityFrameworkCore;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
    public class TemeContext : DbContext
    {
        public TemeContext()
        {
        }
        public TemeContext(DbContextOptions<TemeContext> options)
            : base(options)
        {
        }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Ref_StorageCondition> Ref_StorageConditions { get; set; }
        public DbSet<Ref_ClassifierMedicalArea> Ref_ClassifierMedicalAreas { get; set; }
        public DbSet<Ref_NomenclatureCodeMedProduct> Ref_NomenclatureCodeMedProducts { get; set; }
        public DbSet<Ref_DegreeRiskClass> Ref_DegreeRiskClasses { get; set; }
        public DbSet<Ref_OrganizationForm> Ref_OrganizationForms { get; set; }
        public DbSet<Ref_Bank> Ref_Banks { get; set; }
        public DbSet<Ref_Currency> Ref_Currencies { get; set; }
        public DbSet<Ref_Country> Ref_Countries { get; set; }
    }
}
