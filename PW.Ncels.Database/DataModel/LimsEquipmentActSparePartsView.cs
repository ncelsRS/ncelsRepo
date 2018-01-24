namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsEquipmentActSparePartsView")]
    public partial class LimsEquipmentActSparePartsView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [StringLength(500)]
        public string InventoryNumber { get; set; }

        public Guid? LocationId { get; set; }

        [StringLength(4000)]
        public string LocationName { get; set; }

        [StringLength(4000)]
        public string LocationNameKz { get; set; }

        public Guid? EquipmentActId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
