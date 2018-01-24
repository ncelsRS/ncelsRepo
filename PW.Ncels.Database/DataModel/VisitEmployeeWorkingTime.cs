namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VisitEmployeeWorkingTime
    {
        public int Id { get; set; }

        public Guid EmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int TimeBegin { get; set; }

        public int TimeEnd { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
