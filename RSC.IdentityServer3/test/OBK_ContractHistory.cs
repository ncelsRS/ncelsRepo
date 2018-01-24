namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractHistory
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public Guid EmployeeId { get; set; }

        [StringLength(4000)]
        public string UnitName { get; set; }

        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string RefuseReason { get; set; }

        public Guid ContractId { get; set; }

        public virtual OBK_Ref_ContractHistoryStatus OBK_Ref_ContractHistoryStatus { get; set; }
    }
}
