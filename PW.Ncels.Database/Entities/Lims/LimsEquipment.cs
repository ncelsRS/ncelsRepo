using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentMetadata))]
    public partial class LimsEquipment : ISoftDeleteEntity
    {
        public class LimsEquipmentMetadata
        {
            [Required]
            public System.Guid Id { get; set; }

            [Display(Name = "Наименование оборудования")]
            public string Name { get; set; }

            [Display(Name = "Инвентарный номер")]
            [Required]
            public string InventoryNumber { get; set; }

            [Display(Name = "Заводской (серийный) номер")]
            public string SerialNumber { get; set; }

            [Display(Name = "Год инсталляции")]
            public Nullable<int> YearInstallation { get; set; }

            [Display(Name = "Модель")]
            public Nullable<System.Guid> ModelId { get; set; }

            [Display(Name = "Фирма производитель")]
            public Nullable<System.Guid> ProducerId { get; set; }

            [Display(Name = "Страна происхождения")]
            public Nullable<System.Guid> CountryProductionId { get; set; }

            [Display(Name = "Тип оборудования")]
            public Nullable<System.Guid> EquipmentTypeId { get; set; }

            [Display(Name = "Место установки")]
            public Nullable<System.Guid> LocationId { get; set; }

            [Display(Name = "Статус")]
            public Nullable<System.Guid> StatusId { get; set; }

            [Display(Name = "Место расположение")]
            public Nullable<System.Guid> LaboratoryId { get; set; }

            [Display(Name = "Отвественный")]
            public Nullable<System.Guid> ResponsiblePersonId { get; set; }
        }

        [Display(Name = "Страна происхождения")]
        public string CountryProductionDicName
        {
            get
            {
                string name = string.Empty;
                if (this.CountryProductionDic != null)
                    name = this.CountryProductionDic.Name;
                return name;
            }
        }

        [Display(Name = "Тип оборудования")]
        public string EquipmentTypeDicName
        {
            get
            {
                string name = string.Empty;
                if (this.EquipmentTypeDic != null)
                    name = this.EquipmentTypeDic.Name;
                return name;
            }
        }

        [Display(Name = "Место установки")]
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

        [Display(Name = "Место расположение")]
        public string LaboratoryDicName
        {
            get
            {
                string name = string.Empty;
                if (this.LaboratoryDic != null)
                    name = this.LaboratoryDic.Name;
                return name;
            }
        }

        [Display(Name = "Модель")]
        public string ModelDicName
        {
            get
            {
                string name = string.Empty;
                if (this.ModelDic != null)
                    name = this.ModelDic.Name;
                return name;
            }
        }

        [Display(Name = "Фирма производитель")]
        public string ProducerDicName
        {
            get
            {
                string name = string.Empty;
                if (this.ProducerDic != null)
                    name = this.ProducerDic.Name;
                return name;
            }
        }

        [Display(Name = "Статус")]
        public string StatusDicName
        {
            get
            {
                string name = string.Empty;
                if (this.StatusDic != null)
                    name = this.StatusDic.Name;
                return name;
            }
        }

        
        [Display(Name = "Ответственный")]
        public string ResponsiblePersonName
        {
            get
            {
                string name = string.Empty;
                if (this.ResponsiblePersonEmp != null)
                    name = this.ResponsiblePersonEmp.FullName;
                return name;
            }
        }

        public FileLink FileLinkData { get; set; }
    }
}