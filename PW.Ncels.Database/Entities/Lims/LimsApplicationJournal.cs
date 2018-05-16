using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsApplicationJournalMetadata))]
    public partial class LimsApplicationJournal : ISoftDeleteEntity
    {
        public partial class LimsApplicationJournalMetadata
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

            public virtual Employee AccepterEmp { get; set; }
            public virtual Employee ApplicationEmp { get; set; }
            public virtual Employee EngineerEmp { get; set; }
            public virtual LimsEquipment LimsEquipment { get; set; }
        }

        [Display(Name = "Наименование оборудования")]
        public string LimsEquipmentName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.Name;
                return name;
            }
        }

        [Display(Name = "Инвентарный номер")]
        public string LimsEquipmentInventoryNumber
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.InventoryNumber;
                return name;
            }
        }

        [Display(Name = "Лаборатория")]
        public string LimsEquipmentLaboratoryName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.LaboratoryDicName;
                return name;
            }
        }

        [Display(Name = "Местонахождение")]
        public string LimsEquipmentLocationName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.LocationDicName;
                return name;
            }
        }

        [Display(Name = "Серийный номер")]
        public string LimsEquipmentSerialNumber
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.SerialNumber;
                return name;
            }
        }
        
        [Display(Name = "Производство")]
        public string LimsEquipmentProduction
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.ProducerDicName;
                return name;
            }
        }

        [Display(Name = "Ответственный за прибор")]
        public string LimsEquipmentResponcibleName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.ResponsiblePersonName;
                return name;
            }
        }

        [Display(Name = "Ф.И.О. специалиста, оставившего заявку")]
        public string ApplicationEmpName
        {
            get
            {
                string name = string.Empty;
                if (this.ApplicationEmp != null)
                    name = this.ApplicationEmp.DisplayName;
                return name;
            }
        }

        [Display(Name = "Ф.И.О. инженера принявшего заявку")]
        public string EngineerEmpName
        {
            get
            {
                string name = string.Empty;
                if (this.EngineerEmp != null)
                    name = this.EngineerEmp.DisplayName;
                return name;
            }
        }

        [Display(Name = "Ф.И.О. специалиста принявшего заявку после ремонта")]
        public string AccepterEmpName
        {
            get
            {
                string name = string.Empty;
                if (this.AccepterEmp != null)
                    name = this.AccepterEmp.DisplayName;
                return name;
            }
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
                EngineerSignDate = value ? DateTime.Now: (DateTime?)null;
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
