namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OBKTaskListRegisterView")]
    public partial class OBKTaskListRegisterView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string TaskNumber { get; set; }

        [StringLength(50)]
        public string RegisterDate { get; set; }

        public DateTime? TaskEndDate { get; set; }

        [StringLength(4000)]
        public string ShortName { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UnitId { get; set; }

        public int? StageId { get; set; }

        public Guid? ExecutorId { get; set; }

        [StringLength(2000)]
        public string StatusNameRu { get; set; }

        [StringLength(2000)]
        public string StatusNameKz { get; set; }
    }
}
