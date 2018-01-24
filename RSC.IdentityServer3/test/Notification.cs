namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string EmployeesId { get; set; }

        public bool IsRead { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        [StringLength(500)]
        public string TableName { get; set; }

        [StringLength(500)]
        public string ObjectId { get; set; }

        public int? StateType { get; set; }

        [StringLength(4000)]
        public string ModifiedUser { get; set; }

        [StringLength(510)]
        public string Email { get; set; }

        public bool? IsSend { get; set; }
    }
}
