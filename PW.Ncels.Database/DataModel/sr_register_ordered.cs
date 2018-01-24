namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_ordered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int reestr { get; set; }

        [StringLength(50)]
        public string law_number { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? law_date { get; set; }

        [StringLength(200)]
        public string law_description { get; set; }

        public short reestr_drug_type { get; set; }

        [Required]
        [StringLength(100)]
        public string registration_number { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime reg_date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? val_date { get; set; }

        public int ats { get; set; }

        public int mnn { get; set; }

        public int pharm_name { get; set; }

        public int pharm_form { get; set; }

        public int firm { get; set; }

        public short country { get; set; }

        public int? package { get; set; }

        public int? dose { get; set; }

        public int? concentrant { get; set; }

        public bool? recept { get; set; }

        [StringLength(8000)]
        public string content { get; set; }

        public short? in_control { get; set; }

        public bool? list_imp_life { get; set; }

        public int? parent { get; set; }

        public short? registration_type { get; set; }

        [Column(TypeName = "money")]
        public decimal? expired { get; set; }

        [StringLength(8000)]
        public string structure { get; set; }

        public int? volume_of_fill { get; set; }

        public int? npp { get; set; }

        public short? val_year { get; set; }

        public short? primary_packing { get; set; }

        public short? secondary_packing { get; set; }

        public int? firm_packing { get; set; }

        public short? country_packing { get; set; }

        public int? firm_owner { get; set; }

        public short? country_owner { get; set; }

        [Required]
        [StringLength(50)]
        public string reestr_drug_type_code { get; set; }

        [Required]
        [StringLength(50)]
        public string reestr_drug_type_name { get; set; }

        [Required]
        [StringLength(50)]
        public string ats_code { get; set; }

        [Required]
        [StringLength(500)]
        public string ats_name { get; set; }

        [Required]
        [StringLength(1000)]
        public string MNN_NAME { get; set; }

        [StringLength(4000)]
        public string PHARM_NAMES_NAME { get; set; }

        public short pharm_name_category { get; set; }

        [Required]
        [StringLength(200)]
        public string PHARM_NAME_CATEGORY_NAME { get; set; }

        [Required]
        [StringLength(500)]
        public string PHARM_FORM_NAME { get; set; }

        [StringLength(500)]
        public string PHARM_FORM_NAME_KAZ { get; set; }

        [StringLength(1000)]
        public string FIRM_NAME { get; set; }

        [Required]
        [StringLength(200)]
        public string COUNTRY_NAME { get; set; }

        [StringLength(50)]
        public string PACKAGES_NAME { get; set; }

        [StringLength(50)]
        public string DOSES_NAME { get; set; }

        [StringLength(50)]
        public string DOSES_UNIT_NAME { get; set; }

        [StringLength(50)]
        public string CONC_NAME { get; set; }

        [StringLength(100)]
        public string IN_CONTROL_NAME { get; set; }

        [StringLength(50)]
        public string color { get; set; }

        [StringLength(50)]
        public string REGISTRATION_TYPE_NAME { get; set; }

        [StringLength(200)]
        public string VOF_NAME { get; set; }

        [StringLength(50)]
        public string VOF_UNIT_NAME { get; set; }

        [StringLength(50)]
        public string country_type { get; set; }

        public int? ats_parent { get; set; }

        [StringLength(8000)]
        public string reestr_text { get; set; }

        [StringLength(500)]
        public string pharm_form_union_name { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? package_union { get; set; }

        [StringLength(100)]
        public string dose_union { get; set; }

        [StringLength(50)]
        public string conc_union { get; set; }

        [StringLength(100)]
        public string vof_union { get; set; }

        [StringLength(1000)]
        public string primary_packing_name { get; set; }

        [StringLength(1000)]
        public string secondary_packing_name { get; set; }

        [StringLength(1000)]
        public string firm_owner_name { get; set; }

        [StringLength(1000)]
        public string firm_packing_name { get; set; }

        [StringLength(200)]
        public string country_owner_name { get; set; }

        [StringLength(200)]
        public string country_packing_name { get; set; }

        [StringLength(51)]
        public string pkg_txt { get; set; }

        [StringLength(1000)]
        public string license_owner_firm_name { get; set; }

        [StringLength(200)]
        public string license_owner_country_name { get; set; }

        public int? firm_license_owner { get; set; }

        public short? country_license_owner { get; set; }

        public short? reestr_change_type { get; set; }

        [StringLength(100)]
        public string change_type_name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? expire_date { get; set; }
    }
}
