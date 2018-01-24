namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_boxes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sr_boxes()
        {
            EXP_DrugWrapping = new HashSet<EXP_DrugWrapping>();
            sr_boxes1 = new HashSet<sr_boxes>();
            sr_register_boxes = new HashSet<sr_register_boxes>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(500)]
        public string name_kz { get; set; }

        public int? parent_id { get; set; }

        public bool block_sign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugWrapping> EXP_DrugWrapping { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_boxes> sr_boxes1 { get; set; }

        public virtual sr_boxes sr_boxes2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sr_register_boxes> sr_register_boxes { get; set; }
    }
}
