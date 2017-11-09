using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(TmcIn.TmcInMetadata))]
    public partial class TmcIn
    {
        public class TmcInMetadata
        {
            public System.Guid Id { get; set; }
            public System.DateTime CreatedDate { get; set; }
            public System.Guid CreatedEmployeeId { get; set; }
            

            /// <summary>
            /// Статус
            /// -1 - Отклонена
            /// 0 - новый
            /// 1 - отправлен в 1С
            /// 2 - Получена из 1С
            /// 3 - Закрыт
            /// 10 - Согласование ИЦ
            /// 12 - Согласование Бухгалтерия
            /// 13 - Согласование Руководство
            /// </summary>
            public int StateType { get; set; }
            public Nullable<System.Guid> OwnerEmployeeId { get; set; }
            public string Provider { get; set; }
            public string ContractNumber { get; set; }
            public Nullable<System.DateTime> ContractDate { get; set; }
            public Nullable<System.DateTime> LastDeliveryDate { get; set; }
            public bool IsFullDelivery { get; set; }
            public string PowerOfAttorney { get; set; }
            public string ProviderBin { get; set; }
            public Nullable<System.Guid> ExecutorEmployeeId { get; set; }
            public Nullable<System.Guid> AgreementEmployeeId { get; set; }
            public bool IsScan { get; set; }
            public Nullable<System.Guid> AccountantEmployeeId { get; set; }
        }
    }
}