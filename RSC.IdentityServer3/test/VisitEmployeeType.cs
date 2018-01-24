namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VisitEmployeeType
    {
        public int Id { get; set; }

        public Guid EmployeeId { get; set; }

        public int VisitTypeId { get; set; }

        public bool IsEnable { get; set; }

        public virtual VisitType VisitType { get; set; }
    }
}
