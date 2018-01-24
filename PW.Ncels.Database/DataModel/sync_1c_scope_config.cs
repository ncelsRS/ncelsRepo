namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DbSync1C.sync_1c_scope_config")]
    public partial class sync_1c_scope_config
    {
        [Key]
        public Guid config_id { get; set; }

        [Column(TypeName = "xml")]
        [Required]
        public string config_data { get; set; }

        [StringLength(1)]
        public string scope_status { get; set; }
    }
}
