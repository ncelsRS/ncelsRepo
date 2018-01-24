namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractHistoryView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Created { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid EmployeeId { get; set; }

        [StringLength(4000)]
        public string UnitName { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string RefuseReason { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid ContractId { get; set; }

        [StringLength(4000)]
        public string EmployeeFullName { get; set; }

        [StringLength(4000)]
        public string EmployeeShortName { get; set; }

        [StringLength(50)]
        public string StatusCode { get; set; }

        [StringLength(2000)]
        public string StatusNameRu { get; set; }

        [StringLength(2000)]
        public string StatusNameKz { get; set; }
    }
}
