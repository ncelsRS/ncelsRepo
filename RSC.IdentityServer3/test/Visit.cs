namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Visit
    {
        public int Id { get; set; }

        public int VisitTypeId { get; set; }

        public Guid VisitorId { get; set; }

        public Guid EmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int TimeBegin { get; set; }

        public int Duration { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        public int VisitStatusId { get; set; }

        public int? RatingValue { get; set; }

        [StringLength(4000)]
        public string RatingComment { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual VisitStatus VisitStatus { get; set; }

        public virtual VisitType VisitType { get; set; }
    }
}
