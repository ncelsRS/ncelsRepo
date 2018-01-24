namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDeclarationCom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DrugDeclarationCom()
        {
            EXP_DrugDeclarationComRecord = new HashSet<EXP_DrugDeclarationComRecord>();
        }

        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public bool IsError { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        [Required]
        [StringLength(500)]
        public string ControlId { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationComRecord> EXP_DrugDeclarationComRecord { get; set; }
    }
}
