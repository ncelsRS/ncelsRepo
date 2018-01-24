namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_certifications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public obk_certifications()
        {
            obk_appendix = new HashSet<obk_appendix>();
            obk_certification_copies = new HashSet<obk_certification_copies>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public long product_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? reg_date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? end_date { get; set; }

        [Required]
        [StringLength(50)]
        public string reg_number { get; set; }

        [Required]
        [StringLength(50)]
        public string reg_number_kz { get; set; }

        [Required]
        [StringLength(50)]
        public string blank_number { get; set; }

        [Required]
        public string product_name { get; set; }

        public string product_name_kz { get; set; }

        [Required]
        public string add_info { get; set; }

        public string add_info_kz { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date_prolongation { get; set; }

        public bool annulment_sign { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? annulment_date { get; set; }

        public string annulment_reason { get; set; }

        public bool dublicate_sign { get; set; }

        public long? cert_dublicate_id { get; set; }

        public bool refuse_sign { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? refuse_date { get; set; }

        public string refuse_reason { get; set; }

        public string argument { get; set; }

        public string argument_kz { get; set; }

        public long? user_id { get; set; }

        public bool? unlimited_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obk_appendix> obk_appendix { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obk_certification_copies> obk_certification_copies { get; set; }

        public virtual obk_products obk_products { get; set; }
    }
}
