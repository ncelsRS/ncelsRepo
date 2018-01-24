namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_mt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_register_mt()
        {
            sr_register_mt_parts = new HashSet<sr_register_mt_parts>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(4000)]
        public string description { get; set; }

        [StringLength(2500)]
        public string purpose { get; set; }

        [StringLength(2500)]
        public string use_area { get; set; }

        public int mt_category_id { get; set; }

        public int degree_risk_id { get; set; }

        public int risk_detail_id { get; set; }

        public bool mt_sign { get; set; }

        public bool sterility_sign { get; set; }

        public bool measurement_sign { get; set; }

        public bool balk_sign { get; set; }

        public virtual sr_degree_risk_details sr_degree_risk_details { get; set; }

        public virtual sr_degree_risks sr_degree_risks { get; set; }

        public virtual sr_mt_categories sr_mt_categories { get; set; }

        public virtual sr_register sr_register { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_mt_parts> sr_register_mt_parts { get; set; }

        public virtual sr_register_mt_kz sr_register_mt_kz { get; set; }
    }
}
