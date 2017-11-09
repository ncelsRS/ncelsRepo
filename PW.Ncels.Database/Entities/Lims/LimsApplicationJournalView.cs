using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsApplicationJournalViewMetadata))]
    public partial class LimsApplicationJournalView
    {
        public partial class LimsApplicationJournalViewMetadata
        {
            public System.Guid Id { get; set; }

            [Display(Name = "Оборудование")]
            public Nullable<System.Guid> EquipmentId { get; set; }

            [Display(Name = "Дата заявки")]
            public Nullable<System.DateTime> ApplicationDate { get; set; }

            [Display(Name = "Вид неисправности")]
            public string TypeOfMalfunction { get; set; }

            [Display(Name = "Ф.И.О. специалиста, оставившего заявку")]
            public Nullable<System.Guid> ApplicantId { get; set; }
            public Nullable<System.DateTime> ApplicationSignDate { get; set; }

            [Display(Name = "Ф.И.О. инженера принявшего заявку")]
            public Nullable<System.Guid> EngineerId { get; set; }
            public Nullable<System.DateTime> EngineerSignDate { get; set; }

            [Display(Name = "Ф.И.О. специалиста принявшего заявку после ремонта")]
            public Nullable<System.Guid> AccepterId { get; set; }
            public Nullable<System.DateTime> AccepterSignDate { get; set; }

            [Display(Name = "Результат обслуживания и/или ремонта")]
            public string Result { get; set; }

            [Display(Name = "Примечание")]
            public string Note { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }
            public string LimsEquipmentName { get; set; }
            public string LimsEquipmentInventoryNumber { get; set; }
            public string LimsEquipmentSerialNumber { get; set; }
            public string LimsEquipmentLocationName { get; set; }
            public string LimsEquipmentLaboratoryName { get; set; }
            public string ApplicationEmpName { get; set; }
            public string EngineerEmpName { get; set; }
            public string AccepterEmpName { get; set; }
        }

        [Display(Name = "Подпись специалиста, оставившего заявку")]
        public bool IsApplicantSign
        {
            get { return ApplicationSignDate != null; }
            set
            {
                ApplicationSignDate = value ? DateTime.Now : (DateTime?)null;
            }
        }

        [Display(Name = "Подпись инженера принявшего заявку")]
        public bool IsEngineerSign
        {
            get { return EngineerSignDate != null; }
            set
            {
                EngineerSignDate = value ? DateTime.Now : (DateTime?)null;
            }
        }

        [Display(Name = "Подпись специалиста принявшего заявку после ремонта")]
        public bool IsAccepterSign
        {
            get { return AccepterSignDate != null; }
            set
            {
                AccepterSignDate = value ? DateTime.Now : (DateTime?)null;
            }
        }
    }
}