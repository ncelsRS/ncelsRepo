namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Holiday
    {
        public long Id { get; set; }

        [StringLength(900)]
        public string EmployeeId { get; set; }

        [StringLength(900)]
        public string EmployeeValue { get; set; }

        [StringLength(900)]
        public string HolidayTypeDictionaryId { get; set; }

        [StringLength(900)]
        public string HolidayTypeDictionaryValue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PeriodStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PeriodEnd { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }

        [StringLength(900)]
        public string DocumentId { get; set; }

        [StringLength(900)]
        public string DocumentValue { get; set; }

        [StringLength(900)]
        public string Note { get; set; }

        public int Count { get; set; }
    }
}
