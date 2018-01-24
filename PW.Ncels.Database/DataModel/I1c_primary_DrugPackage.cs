namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_DrugPackage
    {
        public Guid Id { get; set; }

        public int? WrappingTypeId { get; set; }

        public string WrappingTypeNameRu { get; set; }

        public string WrappingTypeNameKz { get; set; }

        public int? WrappingKindId { get; set; }

        public string WrappingKindNameRu { get; set; }

        public string WrappingKindNameKz { get; set; }

        public double? WrappingSize { get; set; }

        public long? SizeMeasureId { get; set; }

        public string SizeMeasureNameRu { get; set; }

        public string SizeMeasureNameKz { get; set; }

        public double? WrappingVolume { get; set; }

        public long? VolumeMeasureId { get; set; }

        public string VolumeMeasureNameRu { get; set; }

        public string VolumeMeasureNameKz { get; set; }

        public int? CountUnit { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [StringLength(500)]
        public string WrappingSizeStr { get; set; }

        [StringLength(500)]
        public string WrappingVolumeStr { get; set; }

        public long? DosageId { get; set; }
    }
}
