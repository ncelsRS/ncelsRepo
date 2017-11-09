using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Enums{

    public enum PriceType{

        /// <summary>
        /// Текущая цена регистрации ЛС
        /// </summary>
        LsCurrentPrice = 0,

        /// <summary>
        /// Цена регистрации ЛС
        /// </summary>
        LsPrice = 1,

        /// <summary>
        /// Текущая цена регистрации ИМН
        /// </summary>
        ImnCurrentPrice = 2,

        /// <summary>
        /// Цена регистрации ИМН
        /// </summary>
        ImnPrice = 3,

        /// <summary>
        /// Текущая цена внесения изменений на регистрацию ИМН
        /// </summary>
        ReImnCurrentPrice = 4,

        /// <summary>
        /// Цена внесения изменений на регистрацию ИМН
        /// </summary>
        ReImnPrice = 5,

        /// <summary>
        /// Текущая цена внесения изменений на регистрацию ЛС
        /// </summary>
        ReLsCurrentPrice = 6,

        /// <summary>
        /// Цена внесения изменений на регистрацию ЛС
        /// </summary>
        ReLsPrice = 7,

        /// <summary>
        /// После переговоров
        /// </summary>
        AfterNegotiation = 8,

        /// <summary>
        /// Внешнее референтное ценообразование
        /// </summary>
        ExternalRef = 99
    }
}
