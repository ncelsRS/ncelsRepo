using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentJournalRecordViewMetadata))]
    public partial class LimsEquipmentJournalRecordView
    {
        public partial class LimsEquipmentJournalRecordViewMetadata
        {
            public System.Guid Id { get; set; }

            [Display(Name = "Наименование оборудования")]
            public System.Guid EquipmentId { get; set; }
            public Nullable<System.Guid> JournalId { get; set; }
            public Nullable<System.DateTime> ActionDate { get; set; }
            public Nullable<System.DateTime> NextActionDate { get; set; }
            public string ActionInfo { get; set; }
            public string ActionResult { get; set; }

            [Display(Name = "Примечание")]
            public string Note { get; set; }

            [Display(Name = "Исполнитель")]
            public Nullable<System.Guid> ExecutorId { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }
            public string EquipmentName { get; set; }
            public string EquipmentModelName { get; set; }
            public string EquipmentProducerName { get; set; }
            public Nullable<int> EquipmentYearInstallation { get; set; }
            public string ExecutorName { get; set; }
        }
    }
}