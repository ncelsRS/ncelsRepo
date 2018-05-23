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
        public DbSet<Role> Roles { get; set; }
        public DbSet<AuthUserRoles> AuthUserRoles { get; set; }
        public DbSet<RolesPermissions> Permissions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<StatePolicy> StatePolicies { get; set; }
        public DbSet<UserForAction> UserForActions { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Declarant> Declarants { get; set; }
        public DbSet<DeclarantDetail> DeclarantDetails { get; set; }
        public DbSet<CostWork> CostWorks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentEquipment> PaymentEquipments { get; set; }
        public DbSet<PaymentPackaging> PaymentPackagings { get; set; }
        public DbSet<PaymentPlatform> PaymentPlatforms { get; set; }
        public DbSet<Ref_StorageCondition> Ref_StorageConditions { get; set; }
        public DbSet<Ref_ClassifierMedicalArea> Ref_ClassifierMedicalAreas { get; set; }
        public DbSet<Ref_NomenclatureCodeMedProduct> Ref_NomenclatureCodeMedProducts { get; set; }
        public DbSet<Ref_DegreeRiskClass> Ref_DegreeRiskClasses { get; set; }
        public DbSet<Ref_OrganizationForm> Ref_OrganizationForms { get; set; }
        public DbSet<Ref_Bank> Ref_Banks { get; set; }
        public DbSet<Ref_Currency> Ref_Currencies { get; set; }
        public DbSet<Ref_Country> Ref_Countries { get; set; }
        public DbSet<Ref_ApplicationType> Ref_ApplicationTypes { get; set; }
        public DbSet<Ref_ServiceType> Ref_ServiceTypes { get; set; }
        public DbSet<Ref_PriceType> Ref_PriceTypes { get; set; }
        public DbSet<Ref_PriceList> Ref_PriceLists { get; set; }
        public DbSet<Ref_ValueAddedTax> Ref_ValueAddedTaxes { get; set; }
        public DbSet<Ref_EquipmentType> Ref_EquipmentTypes { get; set; }
        public DbSet<Ref_PackagingType> Ref_PackagingTypes { get; set; }
        public DbSet<Ref_Measure> Ref_Measures { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<IconRecord> IconRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Declarant>()
                .HasMany(e => e.DeclarantContract)
                .WithOne(e => e.Declarant)
                .HasForeignKey(e => e.DeclarantId);
            modelBuilder.Entity<Declarant>()
                .HasMany(e => e.ManufacturContract)
                .WithOne(e => e.Manufactur)
                .HasForeignKey(e => e.ManufacturId);
            modelBuilder.Entity<Declarant>()
                .HasMany(e => e.PayerContract)
                .WithOne(e => e.Payer)
                .HasForeignKey(e => e.PayerId);

            modelBuilder.Entity<DeclarantDetail>()
                .HasMany(e => e.DeclarantDetailContract)
                .WithOne(e => e.DeclarantDetail)
                .HasForeignKey(e => e.DeclarantDetailId);
            modelBuilder.Entity<DeclarantDetail>()
                .HasMany(e => e.ManufacturDetailContract)
                .WithOne(e => e.ManufacturDetail)
                .HasForeignKey(e => e.ManufacturDetailId);
            modelBuilder.Entity<DeclarantDetail>()
                .HasMany(e => e.PayerDetailContract)
                .WithOne(e => e.PayerDetail)
                .HasForeignKey(e => e.PayerDetailId);

            //modelBuilder.Entity<Ref_EquipmentType>()
            //      .Property(b => b.DateUpdate)
            //      .HasDefaultValueSql("getdate()");

        }
    }
}
