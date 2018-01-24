namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DbSync1C.sync_1c_scope_info")]
    public partial class sync_1c_scope_info
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int scope_local_id { get; set; }

        public Guid scope_id { get; set; }

        [Key]
        [StringLength(100)]
        public string sync_scope_name { get; set; }

        public byte[] scope_sync_knowledge { get; set; }

        public byte[] scope_tombstone_cleanup_knowledge { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] scope_timestamp { get; set; }

        public Guid? scope_config_id { get; set; }

        public int scope_restore_count { get; set; }

        public string scope_user_comment { get; set; }
    }
}
