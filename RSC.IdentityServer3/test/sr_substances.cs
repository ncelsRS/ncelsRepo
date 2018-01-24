namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_substances
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_substances()
        {
            EXP_DrugSubstance = new HashSet<EXP_DrugSubstance>();
            sr_register_substances = new HashSet<sr_register_substances>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(500)]
        public string name_kz { get; set; }

        [StringLength(250)]
        public string name_eng { get; set; }

        public bool animal_sign { get; set; }

        public int? category_id { get; set; }

        [StringLength(50)]
        public string category_pos { get; set; }

        public bool block_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugSubstance> EXP_DrugSubstance { get; set; }

        public virtual sr_categories sr_categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_substances> sr_register_substances { get; set; }
    }
}
