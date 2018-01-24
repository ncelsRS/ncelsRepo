namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_atc_codes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_atc_codes()
        {
            EXP_DrugDeclaration = new HashSet<EXP_DrugDeclaration>();
            sr_atc_codes1 = new HashSet<sr_atc_codes>();
            sr_register_drugs = new HashSet<sr_register_drugs>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(10)]
        public string code { get; set; }

        [StringLength(500)]
        public string name { get; set; }

        [StringLength(1000)]
        public string name_kz { get; set; }

        public int? parent_id { get; set; }

        public bool block_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_atc_codes> sr_atc_codes1 { get; set; }

        public virtual sr_atc_codes sr_atc_codes2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_drugs> sr_register_drugs { get; set; }
    }
}
