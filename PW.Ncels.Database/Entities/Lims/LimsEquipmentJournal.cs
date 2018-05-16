using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentJournalMetadata))]
    public partial class LimsEquipmentJournal
    {
        public partial class LimsEquipmentJournalMetadata
        {
            public System.Guid Id { get; set; }
            public System.Guid JournalTypeId { get; set; }

            [Display(Name = "Год")]
            public Nullable<int> Year { get; set; }

            [Display(Name = "Дата")]
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }

            public virtual Dictionary JournalTypeDic { get; set; }
        }

        [Display(Name = "Наименование журнала")]
        public string JournalTypeName
        {
            get
            {
                string name = string.Empty;
                if (this.JournalTypeDic != null)
                    name = this.JournalTypeDic.Name;
                return name;
            }
        }

        public string JournalTypeCode
        {
            get
            {
                string name = string.Empty;
                if (this.JournalTypeDic != null)
                    name = this.JournalTypeDic.Code;
                return name;
            }
        }
    }
}
