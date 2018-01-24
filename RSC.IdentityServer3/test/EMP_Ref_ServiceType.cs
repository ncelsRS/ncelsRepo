namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Ref_ServiceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMP_Ref_ServiceType()
        {
            EMP_Ref_PriceList = new HashSet<EMP_Ref_PriceList>();
            EMP_Ref_ServiceType1 = new HashSet<EMP_Ref_ServiceType>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(255)]
        public string NameKz { get; set; }

        public Guid? ParentId { get; set; }

        public bool ChangeType { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? DegreeRiskId { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public virtual EMP_Ref_DegreeRisk EMP_Ref_DegreeRisk { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Ref_PriceList> EMP_Ref_PriceList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Ref_ServiceType> EMP_Ref_ServiceType1 { get; set; }

        public virtual EMP_Ref_ServiceType EMP_Ref_ServiceType2 { get; set; }
    }
}
