namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("History")]
    public partial class History
    {
        public int Id { get; set; }

        public Guid GroupId { get; set; }

        [StringLength(4000)]
        public string OperationId { get; set; }

        [StringLength(4000)]
        public string TableName { get; set; }

        [StringLength(4000)]
        public string ColumnName { get; set; }

        public Guid ObjectId { get; set; }

        [StringLength(4000)]
        public string Record { get; set; }

        [StringLength(4000)]
        public string OldValue { get; set; }

        [StringLength(4000)]
        public string NewValue { get; set; }

        public DateTime CreatedTime { get; set; }

        [StringLength(4000)]
        public string UserName { get; set; }

        [StringLength(4000)]
        public string Ip { get; set; }
    }
}
