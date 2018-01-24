namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_PriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_PriceList()
        {
            OBK_ContractPrice = new HashSet<OBK_ContractPrice>();
        }

        public Guid Id { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(255)]
        public string NameKz { get; set; }

        public Guid UnitId { get; set; }

        public double Price { get; set; }

        public Guid ServiceTypeId { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? DegreeRiskId { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractPrice> OBK_ContractPrice { get; set; }

        public virtual OBK_Ref_DegreeRisk OBK_Ref_DegreeRisk { get; set; }

        public virtual OBK_Ref_ServiceType OBK_Ref_ServiceType { get; set; }

        public virtual OBK_Ref_Type OBK_Ref_Type { get; set; }
    }
}
