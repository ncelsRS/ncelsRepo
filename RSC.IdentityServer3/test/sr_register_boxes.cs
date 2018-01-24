namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_boxes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_register_boxes()
        {
            sr_drug_forms = new HashSet<sr_drug_forms>();
            sr_drug_forms1 = new HashSet<sr_drug_forms>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int register_id { get; set; }

        public int box_id { get; set; }

        public bool? inner_sign { get; set; }

        public decimal? volume { get; set; }

        public long? volume_measure_id { get; set; }

        public int unit_count { get; set; }

        [StringLength(50)]
        public string box_size { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        public virtual sr_boxes sr_boxes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_drug_forms> sr_drug_forms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_drug_forms> sr_drug_forms1 { get; set; }

        public virtual sr_register sr_register { get; set; }

        public virtual sr_register_boxes_rk_ls sr_register_boxes_rk_ls { get; set; }
    }
}
