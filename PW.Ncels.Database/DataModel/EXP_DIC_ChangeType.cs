namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DIC_ChangeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DIC_ChangeType()
        {
            EXP_DrugChangeType = new HashSet<EXP_DrugChangeType>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(2000)]
        public string ChangeName { get; set; }

        [StringLength(2000)]
        public string ChangeType { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateEdit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugChangeType> EXP_DrugChangeType { get; set; }
    }
}
