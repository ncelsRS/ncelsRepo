namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActionLogsView")]
    public partial class ActionLogsView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Date { get; set; }

        public int? PlaceId { get; set; }

        [StringLength(200)]
        public string PlaceName { get; set; }

        [StringLength(1000)]
        public string Text { get; set; }

        public string AdditionalText { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(1000)]
        public string IpAddress { get; set; }

        public Guid? EmployeeId { get; set; }

        [StringLength(4000)]
        public string EmployeeLastName { get; set; }

        [StringLength(4000)]
        public string EmployeeFirstName { get; set; }

        [StringLength(4000)]
        public string EmployeeMiddleName { get; set; }

        [StringLength(255)]
        public string EmployeeLogin { get; set; }
    }
}
