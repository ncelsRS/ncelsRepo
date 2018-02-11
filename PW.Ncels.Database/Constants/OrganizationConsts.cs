using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public class OrganizationConsts
    {
        /// <summary>
        /// Код руоводства организации
        /// </summary>
        public const string HeadCode = "01";

        /// <summary>
        /// Исследовательский центр
        /// </summary>
        public const string ReasearchCenterCode = "researchcenter";

        /// <summary>
        /// Руководство Исследовательского цента
        /// </summary>
        public const string ReasearchCenterHeadCode = "researchcenter-01";

        /// <summary>
        /// Департамент экономики и финансов
        /// </summary>
        public const string FinanceDepartmentCode = "finance";

        /// <summary>
        /// Управление бухгалтерского учета и отчетности
        /// </summary>
        public const string AccountantCode = "accounting";

        /// <summary>
        /// Отдел снабжения
        /// </summary>
        public const string SupplyDivision = "supplyDivision";

        #region Территориальные филиалы 
        /// <summary>
        /// NCELS
        /// </summary>
        public const string NCELS = "00";

        /// <summary>
        /// Территориальный филиал г.Караганда
        /// </summary>
        public const string Karaganda = "00_09";

        /// <summary>
        /// Территориальный филиал г.Костанай
        /// </summary>
        public const string Kostanai = "00_10";

        /// <summary>
        /// Территориальный филиал г.Усть-Каменогорск
        /// </summary>
        public const string UstKamenogorsk = "00_16";

        /// <summary>
        /// Территориальный филиал г.Павлодар
        /// </summary>
        public const string Pavlodar = "00_14";

        /// <summary>
        /// Территориальный филиал г. Тараз
        /// </summary>
        public const string Taraz = "00_08";

        /// <summary>
        /// Территориальный филиал г.Шымкент
        /// </summary>
        public const string Shymkent = "00_13";

        /// <summary>
        /// Территориальный филиал г.Астана
        /// </summary>
        public const string Astana = "00_01";

        /// <summary>
        /// Территориальный филиал г.Актобе
        /// </summary>
        public const string Aktobe = "04";

        public static readonly List<string> Filials = new List<string>()
        {
            NCELS,
            Kostanai,
            Karaganda,
            UstKamenogorsk,
            Pavlodar,
            Taraz,
            Shymkent,
            Astana,
            Aktobe
        };

        #endregion

    }
}
