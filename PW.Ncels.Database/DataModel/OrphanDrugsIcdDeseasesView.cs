namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrphanDrugsIcdDeseasesView")]
    public partial class OrphanDrugsIcdDeseasesView
    {
        public Guid? Id { get; set; }

        public long? LinkDicId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OrphanDrugsId { get; set; }

        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreateDatetime { get; set; }

        public long? IcdDeseasesId { get; set; }

        [StringLength(512)]
        public string CodeICD { get; set; }

        public string DiseaseOfICD { get; set; }

        public string SysnonimAndRareDesease { get; set; }
    }
}
