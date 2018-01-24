namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_pharmacological_actions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_pharmacological_actions()
        {
            sr_pharmacological_actions1 = new HashSet<sr_pharmacological_actions>();
            sr_register_pharmacological_actions = new HashSet<sr_register_pharmacological_actions>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(250)]
        public string name { get; set; }

        [Required]
        [StringLength(250)]
        public string name_kz { get; set; }

        public int? parent_id { get; set; }

        public bool block_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_pharmacological_actions> sr_pharmacological_actions1 { get; set; }

        public virtual sr_pharmacological_actions sr_pharmacological_actions2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_pharmacological_actions> sr_register_pharmacological_actions { get; set; }
    }
}
