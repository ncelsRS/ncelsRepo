namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisitsView")]
    public partial class VisitsView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VisitId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime VisitDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VisitDuration { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VisitTimeBegin { get; set; }

        [StringLength(4000)]
        public string VisitComment { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VisitStatusId { get; set; }

        [StringLength(4000)]
        public string VisitRatingComment { get; set; }

        public int? VisitRatingValue { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(200)]
        public string VisitStatusName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VisitTypeId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(4000)]
        public string VisitTypeName { get; set; }

        [StringLength(4000)]
        public string VisitTypeGroup { get; set; }

        [Key]
        [Column(Order = 8)]
        public Guid VisitorId { get; set; }

        [StringLength(4000)]
        public string VisitorLastName { get; set; }

        [StringLength(4000)]
        public string VisitorFirstName { get; set; }

        [StringLength(4000)]
        public string VisitorMiddleName { get; set; }

        [Key]
        [Column(Order = 9)]
        public Guid EmployeeId { get; set; }

        [StringLength(4000)]
        public string EmployeeLastName { get; set; }

        [StringLength(4000)]
        public string EmployeeFirstName { get; set; }

        [StringLength(4000)]
        public string EmployeeMiddleName { get; set; }
    }
}
