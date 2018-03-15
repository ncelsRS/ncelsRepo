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
        /// Код директора для ГО и ТФ
        /// </summary>
        public const string Сeo = "ncels_ceo";

        /// <summary>
        /// Код заместителя директора для ГО и ТФ
        /// </summary>
        public const string Deputyceo = "ncels_deputyceo";

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

        /// <summary>
        /// Управление по оценке безопасности и качества ЛС и ИМН
        /// </summary>
        public const string UobkDepartament = "UOBK-DEPARTMENT";

        /// <summary>
        /// Центр по обслуживанию заявителей
        /// </summary>
        public const string CozDepartament = "coz";

        /// <summary>
        /// Управление по внедрению и развитию надлежащих фармацевтических практик и международных стандартов
        /// </summary>
        public const string PpisDepartament = "PPIS-DEPARTMENT";

        #region Испытательные лаборатории в ИЦ
        /// <summary>
        /// Лаборатория биологических испытаний
        /// </summary>
        public const string Researchcenter3 = "researchcenter3";
        /// <summary>
        /// Отдел по обслуживанию лабораторного оборудования
        /// </summary>
        public const string Researchcenter4 = "researchcenter4";
        /// <summary>
        /// Физико-химическая лаборатория
        /// </summary>
        public const string Researchcenter5 = "researchcenter5";
        /// <summary>
        /// Лаборатория фармакологических испытаний
        /// </summary>
        public const string Researchcenter6 = "researchcenter6";
        /// <summary>
        /// Лаборатория  испытаний медицинских изделий
        /// </summary>
        public const string Researchcenter7 = "researchcenter7";

        public static readonly List<string> Researchcenters = new List<string>()
        {
            Researchcenter3,
            Researchcenter4,
            Researchcenter5,
            Researchcenter6,
            Researchcenter7
        };

        #endregion

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
        public const string Aktobe = "00_04";

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
