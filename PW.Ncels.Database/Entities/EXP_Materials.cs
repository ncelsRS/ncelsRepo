using System;
using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    [MetadataType(typeof(EXP_MaterialsMetadata))]
    public partial class EXP_Materials : ISoftDeleteEntity
    {
        [Display(Name = "Тип")]
        public string TypeName
        {
            get
            {
                if (this.MaterialType == null)
                    return string.Empty;
                return this.MaterialType.Name;
            }
        }
        
        [Display(Name = "Лекарственная форма")]
        public string DrugFormName
        {
            get
            {
                if (this.sr_dosage_forms == null)
                    return string.Empty;
                return this.sr_dosage_forms.name;
            }
        }


        [Display(Name = "Единица измерения")]
        public string UnitName
        {
            get
            {
                if (this.Unit == null)
                    return string.Empty;
                return this.Unit.Name;
            }
        }


        [Display(Name = "Единица измерения")]
        public string DosageUnitName
        {
            get
            {
                if (this.DosageUnit == null)
                    return string.Empty;
                return this.DosageUnit.Name;
            }
        }


        [Display(Name = "Единица измерения")]
        public string VolumeUnitName
        {
            get
            {
                if (this.VolumeUnit == null)
                    return string.Empty;
                return this.VolumeUnit.Name;
            }
        }

        public string StorageName
        {
            get
            {
                if (this.DIC_Storages == null)
                    return string.Empty;
                return this.DIC_Storages.NameRu;
            }
        }

        [Display(Name = "Условия хранения")]
        public string StorageConditionName
        {
            get
            {
                if (this.DIC_Storages == null)
                    return string.Empty;
                return this.StorageCondition.Name;
            }
        }

        [Display(Name = "Производитель")]
        public string ProducerName
        {
            get
            {
                if (this.Producer == null)
                    return string.Empty;
                return this.Producer.Name;
            }
        }

        [Display(Name = "Страна")]
        public string CountryName
        {
            get
            {
                if (this.Country == null)
                    return string.Empty;
                return this.Country.Name;
            }
        }

        [Display(Name = "Температура хранения с ... по ...")]
        public string StorageTemperatureStr
        {
            get
            {
                string from = this.StorageTemperatureFrom > 0
                    ? "+" + this.StorageTemperatureFrom
                    : "" + this.StorageTemperatureFrom;

                string to = this.StorageTemperatureTo > 0
                    ? "+" + this.StorageTemperatureTo
                    : "" + this.StorageTemperatureTo;

                return $"{from} ... {to}";
            }
        }

        [Display(Name = "Донос")]
        public string IsAdditionalStr => GetBoolStr(IsAdditional);

        [Display(Name = "Сертификат/паспорт")]
        public string IsCertificatePassportStr => GetBoolStr(IsCertificatePassport);

        [Display(Name = "Содержит НПП")]
        public string IsContainNPPStr => GetBoolStr(IsContainNPP);

        [Display(Name = "Внешнее состояние")]
        public string ExternalStateStr
        {
            get
            {
                if (ExternalState != null)
                    return ExternalState.Name;
                return string.Empty;
            }
        }
        
        [Display(Name = "Заявление")]
        public string DrugDeclarationNumber
        {
            get
            {
                if (EXP_DrugDeclaration != null)
                    return EXP_DrugDeclaration.Number;
                return string.Empty;
            }
        }

        [Display(Name = "Вид регистрации")]
        public string DrugDeclarationTypeName
        {
            get
            {
                if (EXP_DrugDeclaration != null && EXP_DrugDeclaration.EXP_DIC_Type != null)
                    return EXP_DrugDeclaration.EXP_DIC_Type.NameRu;
                return string.Empty;
            }
        }

        [Display(Name = "Дата регистрации")]
        public string DrugDeclarationCreatedDate
        {
            get
            {
                if (EXP_DrugDeclaration != null)
                    return EXP_DrugDeclaration.CreatedDate.ToShortDateString();
                return string.Empty;
            }
        }

        [Display(Name = "Торговое название")]
        public string DrugDeclarationName
        {
            get
            {
                if (EXP_DrugDeclaration != null)
                    return EXP_DrugDeclaration.NameRu;
                return string.Empty;
            }
        }

        [Display(Name = "Номер нарпавления")]
        public string DirectionNumber { get; set; }


        public int? DrugFormIdDefaultValue { get; set; }

        public class EXP_MaterialsMetadata
        {
            [Display(Name = "ИД")]
            public System.Guid Id { get; set; }

            [Display(Name = "Дата регистрации")]
            public Nullable<System.DateTime> RegistrationDate { get; set; }

            [Display(Name = "Дата создания")]
            public Nullable<System.DateTime> CreatedDate { get; set; }

            [Display(Name = "Наименование")]
            [Required(ErrorMessage = "Необходимо заполнить")]
            public string Name { get; set; }

            [Display(Name = "Тип")]
            public System.Guid TypeId { get; set; }

            [Display(Name = "Лекарственная форма")]
            public Nullable<int> DrugFormId { get; set; }

            [Display(Name = "Дозировка")]
            public Nullable<decimal> Dosage { get; set; }

            [Display(Name = "Единица измерения")]
            public Nullable<System.Guid> DosageUnitId { get; set; }

            [Display(Name = "Количество доз в упаковке")]
            public Nullable<int> DosageQuantity { get; set; }

            [Display(Name = "Концентрация")]
            public string Concentration { get; set; }

            [Display(Name = "Единица измерения концентрации")]
            public Guid ConcentrationUnitId { get; set; }

            [Display(Name = "Объем")]
            public Nullable<decimal> Volume { get; set; }

            [Display(Name = "Единица измерения")]
            public Nullable<System.Guid> VolumeUnitId { get; set; }

            [Display(Name = "Содержит НПП")]
            public bool IsContainNPP { get; set; }

            [Display(Name = "Производитель")]
            public System.Guid? ProducerId { get; set; }

            [Display(Name = "Страна")]
            public System.Guid? CountryId { get; set; }

            [Display(Name = "Количество")]
            public int Quantity { get; set; }

            [Display(Name = "Единица измерения")]
            [Required(ErrorMessage = "Необходимо заполнить")]
            public System.Guid UnitId { get; set; }

            [Display(Name = "Серия/Партия")]
            [Required(ErrorMessage = "Необходимо заполнить")]
            public string Batch { get; set; }

            [Display(Name = "Дата производства")]
            public Nullable<System.DateTime> DateOfManufacture { get; set; }

            [Display(Name = "Срок годности")]
            public Nullable<System.DateTime> ExpirationDate { get; set; }

            [Display(Name = "Дата ре-теста")]
            public Nullable<System.DateTime> RetestDate { get; set; }

            [Display(Name = "Сертификат/паспорт")]
            public bool IsCertificatePassport { get; set; }

            [Display(Name = "Условия хранения")]
            public Nullable<System.Guid> StorageConditionId { get; set; }

            public Nullable<System.Guid> StorageId { get; set; }

            [Display(Name = "Температура хранения с")]
            public Nullable<decimal> StorageTemperatureFrom { get; set; }

            [Display(Name = "по")]
            public Nullable<decimal> StorageTemperatureTo { get; set; }

            [Display(Name = "% содержания активного вещества")]
            public Nullable<decimal> ActiveSubstancePercent { get; set; }

            [Display(Name = "% содержания воды")]
            public Nullable<decimal> WaterContentPercent { get; set; }

            [Display(Name = "Заявление")]
            public System.Guid DrugDeclarationId { get; set; }

            [Display(Name = "Донос")]
            public bool IsAdditional { get; set; }

            [Display(Name = "Статус")]
            public Nullable<System.Guid> StatusId { get; set; }

            [Display(Name = "Внешнее состояние")]
            public Nullable<System.Guid> ExternalStateId { get; set; }

            [Display(Name = "Заключение о соответствии / несоответствии")]
            public Nullable<System.Guid> ConcordanceStatementId { get; set; }

            [Display(Name = "Дата вскрытия")]
            public Nullable<System.DateTime> OpeningDate { get; set; }

            [Display(Name = "Дата окончания срока хранения после вскрытия")]
            public Nullable<System.DateTime> ExpirationAfterOpeningDate { get; set; }

            public Nullable<System.DateTime> DeleteDate { get; set; }
        }

        private string GetBoolStr(bool? b)
        {
            return b.HasValue && b.Value ? "Да" : "Нет";
        }

    }
}
