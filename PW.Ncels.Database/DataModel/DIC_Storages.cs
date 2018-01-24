namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_Storages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIC_Storages()
        {
            DIC_Storages1 = new HashSet<DIC_Storages>();
            EXP_Materials = new HashSet<EXP_Materials>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(512)]
        public string NameRu { get; set; }

        [StringLength(512)]
        public string NameKz { get; set; }

        [StringLength(512)]
        public string NameEn { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public Guid? ParentId { get; set; }

        public Guid OrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIC_Storages> DIC_Storages1 { get; set; }

        public virtual DIC_Storages DIC_Storages2 { get; set; }

        public virtual Unit Unit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials { get; set; }
    }
}
