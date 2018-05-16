using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class OBK_Ref_PaymentStatus
    {
        /// <summary>
        /// Статус "На формировании"
        /// </summary>
        public const string OnFormation = "onFormation";
        ///// <summary>
        ///// Статус "Сформирован"
        ///// </summary>
        //public const string Formed = "formed";
        /// <summary>
        /// Статус "Требует подписания"
        /// </summary>
        public const string ReqSign = "reqSign";
        /// <summary>
        /// Статус "Отправлен на оплату"
        /// </summary>
        public const string SendToPayment = "sendToPayment";
        /// <summary>
        /// Статус "Оплачен"
        /// </summary>
        public const string Paid = "paid";
        /// <summary>
        /// Статус "Оплачен не полностью"
        /// </summary>
        public const string NotFullPaid = "notFullPaid";
        /// <summary>
        /// Статус "Срок оплаты истек"
        /// </summary>
        public const string PaymentExpired = "paymentExpired";
    }
}
