namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_producers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_producers()
        {
            sr_register_mt_parts = new HashSet<sr_register_mt_parts>();
            sr_register_producers = new HashSet<sr_register_producers>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public long? form_type_id { get; set; }

        [StringLength(1000)]
        public string name { get; set; }

        [StringLength(1000)]
        public string name_kz { get; set; }

        [StringLength(500)]
        public string name_eng { get; set; }

        [StringLength(12)]
        public string rnn { get; set; }

        [StringLength(250)]
        public string bin { get; set; }

        [StringLength(250)]
        public string iin { get; set; }

        public int type_id { get; set; }

        public bool block_sign { get; set; }

        public virtual sr_form_types sr_form_types { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_mt_parts> sr_register_mt_parts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_producers> sr_register_producers { get; set; }
    }
}
