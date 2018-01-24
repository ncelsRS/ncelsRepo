namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_international_names
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_international_names()
        {
            EXP_DrugDeclaration = new HashSet<EXP_DrugDeclaration>();
            sr_register_drugs = new HashSet<sr_register_drugs>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string name_rus { get; set; }

        [StringLength(255)]
        public string name_eng { get; set; }

        [StringLength(255)]
        public string name_lat { get; set; }

        [StringLength(510)]
        public string name_kz { get; set; }

        public bool block_sign { get; set; }

        public bool new_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_drugs> sr_register_drugs { get; set; }
    }
}
