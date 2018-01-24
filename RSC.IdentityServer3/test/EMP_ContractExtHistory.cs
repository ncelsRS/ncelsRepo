namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_ContractExtHistory
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid StatusId { get; set; }

        public Guid ContractId { get; set; }

        public Guid? EmployeeId { get; set; }

        public virtual EMP_Contract EMP_Contract { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
