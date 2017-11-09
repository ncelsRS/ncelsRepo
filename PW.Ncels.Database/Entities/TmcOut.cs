using System;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{
    /// <summary>
    /// Заявки на получение
    /// </summary>
    public partial class TmcOut : ITmcRequest
    {
        public static class TmcOutStatuses
        {
            /// <summary>
            /// Новый
            /// </summary>
            public const int New = 0;

            /// <summary>
            /// Отклонен
            /// </summary>
            public const int Rejected = 1;

            /// <summary>
            /// Выдан
            /// </summary>
            public const int Issued = 2;

            /// <summary>
            /// Отправлен
            /// </summary>
            public const int Sended = 3;
            
            /// <summary>
            /// Заказан
            /// </summary>
            public const int Ordered = 10;

            /// <summary>
            /// Аннулирован
            /// </summary>
            public const int Repealed = 11;
        }
    }
}