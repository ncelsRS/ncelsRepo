using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(LimsEquipmentPlanMetadata))]
    public partial class LimsEquipmentPlan : ISoftDeleteEntity
    {
        public partial class LimsEquipmentPlanMetadata
        {
            public System.Guid Id { get; set; }

            [Display(Name = "Вид плана")]
            public System.Guid PlanTypeId { get; set; }
            [Display(Name = "Год")]
            public Nullable<int> Year { get; set; }

            [Display(Name = "Директор ИЦ")]
            public Nullable<System.Guid> DirectorRcId { get; set; }

            [Display(Name = "Заведующий ОпоЛО")]
            public Nullable<System.Guid> HeadOfOpoloId { get; set; }
            public Nullable<System.DateTime> CreateDate { get; set; }
            public Nullable<System.DateTime> DeleteDate { get; set; }

            public virtual Dictionary PlanTypeDic { get; set; }
            public virtual Employee DirectorRcEmp { get; set; }
            public virtual Employee HeadOfOpoloEmp { get; set; }
            public virtual ICollection<LimsPlanEquipmentLink> LimsPlanEquipmentLinks { get; set; }
        }

        [Display(Name = "Наименование плана")]
        public string PlanTypeName
        {
            get
            {
                string name = string.Empty;
                if (this.PlanTypeDic != null)
                    name = this.PlanTypeDic.Name;
                return name;
            }
        }

        [Display(Name = "Директор ИЦ")]
        public string DirectorRcName
        {
            get
            {
                string name = string.Empty;
                if (this.DirectorRcEmp != null)
                    name = this.DirectorRcEmp.DisplayName;
                return name;
            }
        }

        [Display(Name = "Заведующий ОпоЛО")]
        public string HeadOfOpoloName
        {
            get
            {
                string name = string.Empty;
                if (this.HeadOfOpoloEmp != null)
                    name = this.HeadOfOpoloEmp.DisplayName;
                return name;
            }
        }

        [Display(Name = "Код плана")]
        public string PlanTypeCode
        {
            get
            {
                string value = string.Empty;
                if (this.PlanTypeDic != null)
                    value = this.PlanTypeDic.Code;
                return value;
            }
        }

    }
}