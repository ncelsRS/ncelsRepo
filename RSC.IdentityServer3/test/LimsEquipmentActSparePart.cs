namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LimsEquipmentActSparePart
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [StringLength(500)]
        public string InventoryNumber { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? EquipmentActId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public virtual Dictionary LocationDic { get; set; }

        public virtual LimsEquipmentAct LimsEquipmentAct { get; set; }
    }
}
