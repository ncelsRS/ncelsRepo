using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Infrastructure.Primitives
{
    public class UserPromts
    {
        /// <summary>
        /// Действия, доступные Декларанту
        /// </summary>
        public class Declarant
        {
            /// <summary>
            /// Отправить в НЦЭЛС
            /// </summary>
            public const string SendToNcels = "SendToNcels";
        }
         /// <summary>
         /// Выбрать исполнителей
         /// </summary>
        public const string SelectExecutors = "SelectExecutors";
        /// <summary>
        /// Проверить соответствия требованиям
        /// </summary>
        public const string IsMeetRequirements = "IsMeetRequirements";
    }
}
