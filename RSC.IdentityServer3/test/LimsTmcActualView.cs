namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsTmcActualView")]
    public partial class LimsTmcActualView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid CreatedEmployeeId { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        [StringLength(450)]
        public string Number { get; set; }

        public string Name { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertName { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertNameKz { get; set; }

        public decimal? TmcCount { get; set; }

        public decimal? TmcCountFact { get; set; }

        public decimal? TmcCountConvert { get; set; }

        public decimal? TmcCountActual { get; set; }

        public decimal? CountSum { get; set; }

        public decimal? CountFactSum { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal CountIssuedActual { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal CountUseActual { get; set; }

        public decimal? CountActual { get; set; }

        [StringLength(4000)]
        public string CreatedEmployeeFullName { get; set; }
    }
}
