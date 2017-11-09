using System.ComponentModel.DataAnnotations;
using System.IO;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentActMetadata))]
    public partial class LimsEquipmentAct : ISoftDeleteEntity
    {
        protected class LimsEquipmentActMetadata
        {
            public System.Guid Id { get; set; }
            public System.Guid EquipmentId { get; set; }
            public Guid ActTypeId { get; set; }
            public string Reason { get; set; }

            [Display(Name = "Состояние")]
            public string State { get; set; }

            [Display(Name = "Заведующий лабораторией")]
            public Nullable<System.Guid> HeadOfLaboratoryId { get; set; }

            [Display(Name = "Заведующий лабораторией")]
            public string HeadOfLaboratoryName { get; set; }

            [Display(Name = "Директор Испытательного центра")]
            public Nullable<System.Guid> DirectorRCId { get; set; }

            [Display(Name = "Директор Испытательного центра")]
            public string DirectorRCName { get; set; }

            [Display(Name = "Инженер ОпоОЛО")]
            public Nullable<System.Guid> EngineerId { get; set; }

            [Display(Name = "Инженер ОпоОЛО")]
            public string EngineerName { get; set; }

            [Display(Name = "Количество")]
            public Nullable<int> Quantity { get; set; }

            [Display(Name = "Дата")]
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }
        }

        public bool IsNew { get; set; }

        [Display(Name = "Наименование документа")]
        public string ActTypeDicName
        {
            get
            {
                string name = string.Empty;
                if (this.ActTypeDic != null)
                    name = this.ActTypeDic.Name;
                return name;
            }
        }

        [Display(Name = "Код акта")]
        public string ActTypeDicCode
        {
            get
            {
                string name = string.Empty;
                if (this.ActTypeDic != null)
                    name = this.ActTypeDic.Code;
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

        public FileLink FileLinkData { get; set; }

        /*
        public static class ActTypeConts
        {
            public static string ActForRepair = "ActForRepair";
            public static string ActOfConversation = "ActOfConversation";
            public static string ActOfSpareParts = "ActOfSpareParts";
            public static string ProtocolOfQualification = "ProtocolOfQualification";
        }
        */
    }
}