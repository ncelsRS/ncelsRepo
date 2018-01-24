namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public obk_products()
        {
            obk_appendix_series = new HashSet<obk_appendix_series>();
            obk_certifications = new HashSet<obk_certifications>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public long? application_id { get; set; }

        [StringLength(2000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string name_kz { get; set; }

        [StringLength(1000)]
        public string producer_name { get; set; }

        [StringLength(2000)]
        public string producer_name_kz { get; set; }

        [StringLength(150)]
        public string country_name { get; set; }

        [StringLength(300)]
        public string country_name_kz { get; set; }

        [Column(TypeName = "money")]
        public decimal? party_count { get; set; }

        public int? party_measure_id { get; set; }

        [StringLength(20)]
        public string tnved_code { get; set; }

        [StringLength(20)]
        public string kpved_code { get; set; }

        public bool mandatory_sign { get; set; }

        [StringLength(2000)]
        public string register_nd { get; set; }

        [StringLength(2000)]
        public string nd { get; set; }

        [StringLength(4000)]
        public string nd_kz { get; set; }

        [StringLength(50)]
        public string reg_number { get; set; }

        public long? register_id { get; set; }

        public long? drug_form_id { get; set; }

        public bool? cert_sign { get; set; }

        public int? types_no_reg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obk_appendix_series> obk_appendix_series { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obk_certifications> obk_certifications { get; set; }

        public virtual obk_product_cost obk_product_cost { get; set; }
    }
}
