namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_drugs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_register_drugs()
        {
            sr_register_pharmacological_actions = new HashSet<sr_register_pharmacological_actions>();
            sr_register_use_methods = new HashSet<sr_register_use_methods>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int drug_type_id { get; set; }

        public int int_name_id { get; set; }

        [Column("_int_name")]
        [StringLength(255)]
        public string C_int_name { get; set; }

        public int atc_id { get; set; }

        [Column("_atc_code")]
        [StringLength(50)]
        public string C_atc_code { get; set; }

        [Column("_atc_name")]
        [StringLength(255)]
        public string C_atc_name { get; set; }

        public int dosage_form_id { get; set; }

        [StringLength(500)]
        public string dosage_comment { get; set; }

        [StringLength(1000)]
        public string dosage_comment_kz { get; set; }

        [Column("_dosage_form_name")]
        [StringLength(500)]
        public string C_dosage_form_name { get; set; }

        public decimal? dosage_value { get; set; }

        public long? dosage_measure_id { get; set; }

        [Column("_dosage")]
        [StringLength(250)]
        public string C_dosage { get; set; }

        [StringLength(500)]
        public string concentration { get; set; }

        [StringLength(1000)]
        public string concentration_kz { get; set; }

        [Column("_unit_count")]
        [StringLength(50)]
        public string C_unit_count { get; set; }

        public bool recipe_sign { get; set; }

        public bool generic_sign { get; set; }

        public int life_type_id { get; set; }

        public int? category_id { get; set; }

        [StringLength(250)]
        public string category_pos { get; set; }

        public int? nd_name_id { get; set; }

        [StringLength(50)]
        public string nd_name { get; set; }

        [StringLength(50)]
        public string nd_number { get; set; }

        [StringLength(250)]
        public string nd_comment { get; set; }

        [StringLength(1000)]
        public string substance { get; set; }

        public bool biosimilar_sign { get; set; }

        public bool auto_generic_sign { get; set; }

        public bool orphan_sign { get; set; }

        public virtual sr_atc_codes sr_atc_codes { get; set; }

        public virtual sr_categories sr_categories { get; set; }

        public virtual sr_dosage_forms sr_dosage_forms { get; set; }

        public virtual sr_drug_types sr_drug_types { get; set; }

        public virtual sr_international_names sr_international_names { get; set; }

        public virtual sr_life_types sr_life_types { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual sr_nd_names sr_nd_names { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_pharmacological_actions> sr_register_pharmacological_actions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_use_methods> sr_register_use_methods { get; set; }
    }
}
