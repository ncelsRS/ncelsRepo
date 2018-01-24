namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommissionUnit
    {
        public int Id { get; set; }

        public int CommissionId { get; set; }

        public Guid EmployeeId { get; set; }

        public int UnitTypeId { get; set; }

        public virtual Commission Commission { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual CommissionUnitType CommissionUnitType { get; set; }
    }
}
