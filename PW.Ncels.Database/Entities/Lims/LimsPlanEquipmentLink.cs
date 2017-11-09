using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsPlanEquipmentLinkMetadata))]
    public partial class LimsPlanEquipmentLink : ISoftDeleteEntity
    {
        public partial class LimsPlanEquipmentLinkMetadata
        {
            public System.Guid Id { get; set; }

            [Display(Name = "ID")]
            public string Number { get; set; }

            [Display(Name = "Оборудование")]
            public System.Guid EquipmentId { get; set; }
            public System.Guid EquipmentPlanId { get; set; }

            [Display(Name = "Подпись")]
            public Nullable<bool> IsSign { get; set; }
            public Nullable<System.DateTime> SignDate { get; set; }

            [Display(Name = "Срок")]
            public Nullable<System.DateTime> TermDate { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }

            public virtual LimsEquipment LimsEquipment { get; set; }
            public virtual LimsEquipmentPlan LimsEquipmentPlan { get; set; }
        }

        public bool IsSignLocal
        {
            get { return IsSign.HasValue && IsSign.Value; }
            set { IsSign = value; }
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
        public string ModelDicName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null && this.LimsEquipment.ModelDic != null)
                    name = this.LimsEquipment.ModelDic.Name;
                return name;
            }
        }

        [Display(Name = "Заводской номер")]
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

        [Display(Name = "Фирма производитель")]
        public string ProducerDicName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null && this.LimsEquipment.ProducerDic != null)
                    name = this.LimsEquipment.ProducerDic.Name;
                return name;
            }
        }
        
        [Display(Name = "Страна")]
        public string EquipmentCountryProductionName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null && this.LimsEquipment.CountryProductionDic != null)
                    name = this.LimsEquipment.CountryProductionDic.Name;
                return name;
            }
        }

        [Display(Name = "Лаборатория")]
        public string EquipmentLaboratoryName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null && this.LimsEquipment.LaboratoryDic != null)
                    name = this.LimsEquipment.LaboratoryDic.Name;
                return name;
            }
        }

        [Display(Name = "Тип")]
        public string EquipmentTypeName
        {
            get
            {
                string name = string.Empty;
                if (this.LimsEquipment != null && this.LimsEquipment.EquipmentTypeDic != null)
                    name = this.LimsEquipment.EquipmentTypeDic.Name;
                return name;
            }
        }
    }
}
