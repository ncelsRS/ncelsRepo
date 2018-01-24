namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_register()
        {
            EXP_DrugDeclaration = new HashSet<EXP_DrugDeclaration>();
            EXP_DrugDosage = new HashSet<EXP_DrugDosage>();
            sr_drug_forms = new HashSet<sr_drug_forms>();
            sr_register_boxes = new HashSet<sr_register_boxes>();
            sr_register_names = new HashSet<sr_register_names>();
            sr_register_producers = new HashSet<sr_register_producers>();
            sr_register_substances = new HashSet<sr_register_substances>();
            sr_register_instructions = new HashSet<sr_register_instructions>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int reg_type_id { get; set; }

        public int reg_action_id { get; set; }

        [StringLength(50)]
        public string reg_number { get; set; }

        [StringLength(100)]
        public string reg_number_kz { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime reg_date { get; set; }

        public short reg_term { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? expiration_date { get; set; }

        [StringLength(3000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string name_kz { get; set; }

        [Column("_producer_name")]
        [StringLength(500)]
        public string C_producer_name { get; set; }

        [Column("_producer_name_kz")]
        [StringLength(1000)]
        public string C_producer_name_kz { get; set; }

        [Column("_country_name")]
        [StringLength(50)]
        public string C_country_name { get; set; }

        [Column("_country_name_kz")]
        [StringLength(100)]
        public string C_country_name_kz { get; set; }

        public bool gmp_sign { get; set; }

        public bool trademark_sign { get; set; }

        public bool patent_sign { get; set; }

        public decimal? storage_term { get; set; }

        public long? storage_measure_id { get; set; }

        [StringLength(500)]
        public string comment { get; set; }

        [StringLength(1000)]
        public string comment_kz { get; set; }

        public bool? unlimited_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDosage> EXP_DrugDosage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_drug_forms> sr_drug_forms { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual sr_reg_actions sr_reg_actions { get; set; }

        public virtual sr_reg_types sr_reg_types { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_boxes> sr_register_boxes { get; set; }

        public virtual sr_register_mt sr_register_mt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_names> sr_register_names { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_producers> sr_register_producers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_substances> sr_register_substances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_instructions> sr_register_instructions { get; set; }
    }
}
