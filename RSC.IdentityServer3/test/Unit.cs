namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unit()
        {
            DIC_Storages = new HashSet<DIC_Storages>();
            Employees = new HashSet<Employee>();
            Employees1 = new HashSet<Employee>();
            OBK_Contract = new HashSet<OBK_Contract>();
            OBK_TaskMaterial = new HashSet<OBK_TaskMaterial>();
            OBK_Tasks = new HashSet<OBK_Tasks>();
            OBK_TaskStatus = new HashSet<OBK_TaskStatus>();
            Units1 = new HashSet<Unit>();
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(510)]
        public string Code { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string ShortName { get; set; }

        [StringLength(510)]
        public string Path { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? EmployeeId { get; set; }

        public int PositionState { get; set; }

        [StringLength(4000)]
        public string ManagerId { get; set; }

        [StringLength(4000)]
        public string SecretaryId { get; set; }

        [StringLength(4000)]
        public string BossId { get; set; }

        [StringLength(4000)]
        public string ChancelleryId { get; set; }

        [StringLength(4000)]
        public string UnitTypeDictionaryId { get; set; }

        [StringLength(4000)]
        public string UnitTypeDictionaryValue { get; set; }

        public int Type { get; set; }

        [StringLength(4000)]
        public string ManagerValue { get; set; }

        [StringLength(4000)]
        public string SecretaryValue { get; set; }

        [StringLength(4000)]
        public string BossValue { get; set; }

        [StringLength(4000)]
        public string ChancelleryValue { get; set; }

        [StringLength(4000)]
        public string DisplayName { get; set; }

        [StringLength(4000)]
        public string CuratorId { get; set; }

        [StringLength(4000)]
        public string CuratorValue { get; set; }

        public int Rank { get; set; }

        [StringLength(900)]
        public string Email { get; set; }

        public int PositionType { get; set; }

        public int PositionStaff { get; set; }

        [StringLength(4000)]
        public string NameKz { get; set; }

        [StringLength(200)]
        public string LegalAddress { get; set; }

        [StringLength(200)]
        public string ActualAddress { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        public Guid? CountryId { get; set; }

        [StringLength(100)]
        public string Iin { get; set; }

        [StringLength(100)]
        public string Bin { get; set; }

        [StringLength(100)]
        public string ApplicationNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIC_Storages> DIC_Storages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Contract> OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskMaterial> OBK_TaskMaterial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Tasks> OBK_Tasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskStatus> OBK_TaskStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Units1 { get; set; }

        public virtual Unit Parent { get; set; }
    }
}
