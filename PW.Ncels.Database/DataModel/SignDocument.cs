namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SignDocument")]
    public partial class SignDocument
    {
        public long Id { get; set; }

        [StringLength(500)]
        public string ClassName { get; set; }

        [StringLength(50)]
        public string ObjectId { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid? EmployeeId { get; set; }

        [Column(TypeName = "ntext")]
        public string SignXml { get; set; }
    }
}
