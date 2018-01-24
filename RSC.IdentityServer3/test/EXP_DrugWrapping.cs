namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugWrapping
    {
        public long Id { get; set; }

        public int? WrappingTypeId { get; set; }

        public int? WrappingKindId { get; set; }

        public double? WrappingSize { get; set; }

        public long? SizeMeasureId { get; set; }

        public double? WrappingVolume { get; set; }

        public long? VolumeMeasureId { get; set; }

        public int? CountUnit { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public long DrugDosageId { get; set; }

        [StringLength(500)]
        public string WrappingSizeStr { get; set; }

        [StringLength(500)]
        public string WrappingVolumeStr { get; set; }

        public virtual EXP_DIC_WrappingType EXP_DIC_WrappingType { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }

        public virtual sr_boxes sr_boxes { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual sr_measures sr_measures1 { get; set; }
    }
}
