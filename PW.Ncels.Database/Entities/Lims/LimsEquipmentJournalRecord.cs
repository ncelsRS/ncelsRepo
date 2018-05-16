using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentJournalRecordMetadata))]
    public partial class LimsEquipmentJournalRecord
    {
        public partial class LimsEquipmentJournalRecordMetadata
        {
            public System.Guid Id { get; set; }
            [Display(Name = "Наименование оборудования")]
            public System.Guid EquipmentId { get; set; }
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

            public virtual Employee ExecutorEmp { get; set; }
            public virtual LimsEquipment LimsEquipment { get; set; }
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

        [Display(Name = "Модель")]
        public string LimsEquipmentModelName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null)
                    name = this.LimsEquipment.ModelDicName;
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

        [Display(Name = "Срок инсталляции")]
        public string LimsEquipmentYear
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null && LimsEquipment.YearInstallation != null)
                    name = this.LimsEquipment.YearInstallation.ToString();
                return name;
            }
        }

        [Display(Name = "Исполнитель")]
        public string ExecutorEmpName
        {
            get
            {
                string name = string.Empty;
                if (this.ExecutorEmp != null)
                    name = this.ExecutorEmp.DisplayName;
                return name;
            }
        }
    }
}