using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PW.Ncels.Database.Recources;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(TmcViewMetadata))]
    public partial class TmcView
    {

        public Guid MainTmcId
        {
            get
            {
               return this.Id;
            }
        }

        public string StatusIcon
        {
            get
            {
                if (ExpiryDate.HasValue)
                {
                    // [TODO] Put to settings
                    if (DateTime.Now >= ExpiryDate.Value.AddDays(-90)
                        && DateTime.Now < ExpiryDate.Value)
                    {
                        return "ONEXPIRY";
                    }
                    if (DateTime.Now > ExpiryDate.Value)
                    {
                        return "EXPIRY";
                    }
                }
                return "NOTEXPIRY";
            }
        }

        public string StatusIconStr
        {
            get
            {
                if (ExpiryDate.HasValue)
                {
                    // [TODO] Put to settings
                    if (DateTime.Now > ExpiryDate.Value.AddDays(-6)
                        && DateTime.Now < ExpiryDate.Value)
                    {
                        return "Срок годности. Осталось дней: " + (ExpiryDate.Value.Subtract(DateTime.Now).Days);
                    }
                    if (DateTime.Now > ExpiryDate.Value)
                    {
                        return "Срок годности истек";
                    }
                }
                return "Срок годности";
            }
        }

        public class TmcViewMetadata
        {
            public System.Guid Id { get; set; }
            public System.DateTime CreatedDate { get; set; }
            public System.Guid CreatedEmployeeId { get; set; }
            public int StateType { get; set; }
            public string StateTypeValue { get; set; }
            public System.Guid TmcInId { get; set; }

            [DisplayName("Номер")]
            public string Number { get; set; }

            [DisplayName("Наименование")]
            public string Name { get; set; }

            [DisplayName("Код")]
            public string Code { get; set; }

            [DisplayName("Производитель")]
            public string Manufacturer { get; set; }

            [DisplayName("№ серии, партии, ГОСТ или LOT")]
            public string Serial { get; set; }

            [DisplayName("Количество/объем")]
            public decimal Count { get; set; }

            [DisplayName("Единица измерения (по заявке)")]
            public Nullable<System.Guid> MeasureTypeDicId { get; set; }
            public string MeasureTypeDicValue { get; set; }
            public decimal CountFact { get; set; }
            public decimal CountConvert { get; set; }
            public Nullable<System.Guid> MeasureTypeConvertDicId { get; set; }
            public string MeasureTypeConvertDicValue { get; set; }
            public decimal CountActual { get; set; }

            [DisplayName("Дата изготовления")]
            public Nullable<System.DateTime> ManufactureDate { get; set; }

            [Display(Name = "Column_TMC_ExpiryDate", ResourceType = typeof(HistoryStr))]
            public Nullable<System.DateTime> ExpiryDate { get; set; }

            [DisplayName("Упаковка")]
            public Nullable<System.Guid> PackageDicId { get; set; }
            public string PackageDicValue { get; set; }

            [DisplayName("Вид ТМЦ")]
            public Nullable<System.Guid> TmcTypeDicId { get; set; }
            public string TmcTypeDicValue { get; set; }

            [DisplayName("Место хранения")]
            public Nullable<System.Guid> StorageDicId { get; set; }
            public string StorageDicValue { get; set; }

            [DisplayName("Сейф, Шкаф")]
            public string Safe { get; set; }

            [DisplayName("Полка")]
            public string Rack { get; set; }
            public Nullable<System.Guid> OwnerEmployeeId { get; set; }
            public string OwnerEmployeeValue { get; set; }
            public Nullable<System.DateTime> ReceivingDate { get; set; }
        }
    }
}
