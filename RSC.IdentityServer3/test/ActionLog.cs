namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActionLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int? PlaceId { get; set; }

        [StringLength(1000)]
        public string Text { get; set; }

        public string AdditionalText { get; set; }

        public Guid? EmployeeId { get; set; }

        public int Type { get; set; }

        [StringLength(1000)]
        public string IpAddress { get; set; }
    }
}
