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

        public static class TmcInStatuses
        {
            /// <summary>
            /// Отклонен
            /// </summary>
            public const int Rejected = -1;

            /// <summary>
            /// Новый
            /// </summary>
            public const int New = 0;

            /// <summary>
            /// Отправлен в 1С
            /// </summary>
            public const int Sended1C = 1;

            /// <summary>
            /// Получена из 1С
            /// </summary>
            public const int Received1C = 2;

            /// <summary>
            /// Закрыт
            /// </summary>
            public const int Closed = 3;

            /// <summary>
            /// Аннулирована
            /// </summary>
            public const int Repeal = 4;

            /// <summary>
            /// Отправлены данные о принятии в 1С
            /// </summary>
            public const int SendedAdmission1C = 5;


            /// <summary>
            /// Согласование ИЦ
            /// </summary>
            public const int AgreedResearchCenter = 10;

            /// <summary>
            /// Согласование Бухгалтерия
            /// </summary>
            public const int AgreedAccount = 11;

            /// <summary>
            /// Согласование Руководство
            /// </summary>
            public const int AgreedHead = 12;

        }
    }
}