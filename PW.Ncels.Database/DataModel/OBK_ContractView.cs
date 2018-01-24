namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(61)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        [StringLength(2000)]
        public string StatusNameRu { get; set; }

        [StringLength(255)]
        public string DeclarantNameRu { get; set; }

        public string Type { get; set; }

        public string TypeKz { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? EmployeeId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string DocType { get; set; }
    }
}
