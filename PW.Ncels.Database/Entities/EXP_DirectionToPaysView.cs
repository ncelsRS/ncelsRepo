using System.ComponentModel.DataAnnotations;
using System.Globalization;
using PW.Ncels.Database.Recources;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(EXP_DirectionToPaysViewMetadata))]
    public partial class EXP_DirectionToPaysView
    {
        [Display(Name = "Торговое название")]
        public string TradeName
        {
            get
            {
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "kz")
                {
                    return TradeNameKz;
                }
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    return TradeNameEn;
                }
                return TradeNameRu;
            }
        }

        [Display(Name = "Лекарственная форма")]
        public string DrugForm
        {
            get
            {
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "kz")
                {
                    return DrugFormKz;
                }
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    return DrugFormEn;
                }
                return DrugFormRu;
            }
        }

        [Display(Name = "Производитель")]
        public string Manufacture
        {
            get
            {
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "kz")
                {
                    return ManufactureKz;
                }
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    return ManufactureEn;
                }
                return ManufactureRu;
            }
        }

        [Display(Name = "Прейскуранты")]
        public string PriceListName
        {
            get
            {
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "kz")
                {
                    return PriceListNameKz;
                }
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "en")
                {
                    return PriceListNameEn;
                }
                return PriceListNameRu;
            }
        }
        
        public string StatusStr
        {
            get { return this.StatusName; }
        }
        
        [Display(Name = "Тип направления")]
        public string TypeStr
        {
            get
            {
                if (Type == ExpertWorkType)
                {
                    return "Экспертные работы";
                }
                if (Type == TranslateType)
                    return "Переводы";

                return "Дополнения";
            }
        }

        public List<EXP_DrugDeclaration> DrugDeclarations { get; set; }
        public List<EXP_PriceList> PriceLists { get; set; }

        public class EXP_DirectionToPaysViewMetadata
        {
            public System.Guid Id { get; set; }

            [Display(ResourceType = typeof(Messages), Name = "Property_Номер_373__00")]
            public string Number { get; set; }

            [Display(ResourceType = typeof(Messages), Name = "Property_Дата_391__00")]
            public System.DateTime DirectionDate { get; set; }

            [Display(Name = "Номера заявок")]
            public string ApplicationNumbers { get; set; }

            [Display(Name = "Даты заявок")]
            public string ApplicationDates { get; set; }

            [Display(Name = "Форма")]
            public string RegistrationForm { get; set; }
            public string TradeNameKz { get; set; }
            public string TradeNameRu { get; set; }
            public string TradeNameEn { get; set; }
            public string DrugFormKz { get; set; }
            public string DrugFormRu { get; set; }
            public string DrugFormEn { get; set; }
            public string ManufactureKz { get; set; }
            public string ManufactureRu { get; set; }
            public string ManufactureEn { get; set; }

            [Display(Name = "Эксперт")]
            public string ExpertDisplayName { get; set; }
            public Nullable<decimal> Price { get; set; }

            [Display(Name = "Сумма")]
            public Nullable<decimal> TotalPrice { get; set; }
            public string PriceListNameKz { get; set; }
            public string PriceListNameRu { get; set; }
            public string PriceListNameEn { get; set; }
            public string PriceListValue { get; set; }

            [Display(Name = "Валюта")]
            public string Currency { get; set; }

            [Display(Name = "Номер счета")]
            public string InvoiceNumber1C { get; set; }

            [Display(Name = "Дата выписки")]
            public Nullable<System.DateTime> InvoiceDatetime1C { get; set; }

            [Display(Name = "Дата оплаты")]
            public Nullable<System.DateTime> PaymentDatetime1C { get; set; }

            [Display(Name = "Сумма оплаты")]
            public Nullable<decimal> PaymentValue1C { get; set; }

            [Display(ResourceType = typeof(HistoryStr), Name = "Property_Комментарий")]
            public string PaymentComment1C { get; set; }

            [Display(Name = "Тип направления")]
            public int Type { get; set; }

        }

        [Display(Name="Экспертные работы")]
        public static readonly int ExpertWorkType = 1;

        [Display(Name = "Перевод")]
        public static int TranslateType = 2;

        [Display(Name = "Доплата по переводам")]
        public static int AdditionalTranslateType = 3;
    }
}