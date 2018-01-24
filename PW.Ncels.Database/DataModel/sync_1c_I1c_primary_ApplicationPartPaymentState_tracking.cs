namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DbSync1C.sync_1c_I1c_primary_ApplicationPartPaymentState_tracking")]
    public partial class sync_1c_I1c_primary_ApplicationPartPaymentState_tracking
    {
        public Guid Id { get; set; }

        public int? update_scope_local_id { get; set; }

        public int? scope_update_peer_key { get; set; }

        public long? scope_update_peer_timestamp { get; set; }

        public int local_update_peer_key { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] local_update_peer_timestamp { get; set; }

        public int? create_scope_local_id { get; set; }

        public int? scope_create_peer_key { get; set; }

        public long? scope_create_peer_timestamp { get; set; }

        public int local_create_peer_key { get; set; }

        public long local_create_peer_timestamp { get; set; }

        public int sync_row_is_tombstone { get; set; }

        public long? restore_timestamp { get; set; }

        public DateTime? last_change_datetime { get; set; }
    }
}
