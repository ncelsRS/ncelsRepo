namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsTmcTempView")]
    public partial class LimsTmcTempView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        public string TmcName { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal Count { get; set; }

        public Guid? MeasureTypeDicId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeDicName { get; set; }

        [StringLength(4000)]
        public string MeasureTypeDicNameKz { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid TmcInId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public decimal? CountRequest { get; set; }

        public decimal? CountReceived { get; set; }

        public bool? IsSelected { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal CountFact { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal CountConvert { get; set; }

        public Guid? MeasureTypeConvertDicId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertDicName { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertDicNameKz { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceivingDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }
    }
}
