namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractExtHistory
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public Guid StatusId { get; set; }

        public Guid ContractId { get; set; }

        public Guid? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        public virtual OBK_Ref_ContractExtHistoryStatus OBK_Ref_ContractExtHistoryStatus { get; set; }
    }
}
