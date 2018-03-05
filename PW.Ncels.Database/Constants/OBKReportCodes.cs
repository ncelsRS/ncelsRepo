using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public static class OBKReportCodes
    {
        /// <summary>
        /// Журнал учета заявок
        /// </summary>
        public const int DECLARATION_REPORT = 1;

        /// <summary>
        /// Журнал учета бланков заключений о безопасности и качестве
        /// </summary>
        public const int ZBK_REPORT = 2;

        /// <summary>
        /// Журнал учета бланков копий заключений о безопасности и качестве
        /// </summary>
        public const int ZBK_COPY_REPORT = 3;

        /// <summary>
        /// Журнал учета выписанных счетов
        /// </summary>
        public const int DIRECTION_PAYMENTS_REPORT = 4;

        /// <summary>
        /// Журнал учета бланков заключений о безопасности и качестве
        /// </summary>
        public const int ZBK_DETAIL_REPORT = 5;

        /// <summary>
        /// Отчет о выдаче забракованной продукции
        /// </summary>
        public const int ZBK_DEFECTIVE_PRODUCT = 6;

        /// <summary>
        /// GMP отчет
        /// </summary>
        public const int GMP_REPORT = 7;

        /// <summary>
        /// Сводный отчет по ТФ
        /// </summary>
        public const int SUMMARY_REPORT_TF = 8;

        /// <summary>
        /// Отчет о проведенных лабораторных испытаниях по оценке безопасности и качества
        /// </summary>
        public const int LABORATORY_TESTS_REPORT = 9;

        /// <summary>
        /// Отчет по специалистам
        /// </summary>
        public const int SPECIALISTS_REPORT = 10;

    }
}
