using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentActSparePartMetadata))]
    public partial class LimsEquipmentActSparePart : ISoftDeleteEntity
    {
        public class LimsEquipmentActSparePartMetadata
        {
            public System.Guid Id { get; set; }

            [Display(Name = "Наименование запасных частей")]
            public string Name { get; set; }

            [Display(Name = "Количество")]
            public Nullable<int> Quantity { get; set; }

            [Display(Name = "Инвентарный номер")]
            public string InventoryNumber { get; set; }

            [Display(Name = "Место установки")]
            public Nullable<System.Guid> LocationId { get; set; }
            public Nullable<System.Guid> EquipmentActId { get; set; }

            [Display(Name = "Дата")]
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }

            public virtual Dictionary LocationDic { get; set; }
            public virtual LimsEquipmentAct LimsEquipmentAct { get; set; }
        }

        public Guid? EquipmentId { get; set; }

        [Display(Name = "Место расположение")]
        public string LocationDicName
        {
            get
            {
                string name = string.Empty;
                if (this.LocationDic != null)
                    name = this.LocationDic.Name;
                return name;
            }
        }
    }
}
