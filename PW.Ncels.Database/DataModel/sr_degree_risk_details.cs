namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_degree_risk_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_degree_risk_details()
        {
            sr_register_mt = new HashSet<sr_register_mt>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int degree_risk_id { get; set; }

        [StringLength(1500)]
        public string name { get; set; }

        [StringLength(3000)]
        public string name_kz { get; set; }

        public virtual sr_degree_risks sr_degree_risks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_mt> sr_register_mt { get; set; }
    }
}
