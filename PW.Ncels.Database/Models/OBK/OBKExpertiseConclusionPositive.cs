using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKExpertiseConclusionPositive
    {
        public bool ToShow { get; set; }
        public Guid AssessmentDeclarationId { get; set; }
        public int ProductSeriesId { get; set; }
        /// <summary>
        /// Дата регистрации
        /// </summary>
        public string ecStartDate { get; set; }
        /// <summary>
        /// Дата истечения
        /// </summary>
        public string ecEndDate { get; set; }
        /// <summary>
        /// Основание на русском языке
        /// </summary>
        public string ecReasonNameRu { get; set; }
        /// <summary>
        /// Основание на казахском языке
        /// </summary>
        public string ecReasonNameKz { get; set; }
        /// <summary>
        /// Наименование на русском языке
        /// </summary>
        public string ecProductNameRu { get; set; }
        /// <summary>
        /// Наименование на казахском языке
        /// </summary>
        public string ecProductNameKz { get; set; }
        /// <summary>
        /// Доп. информация на русском языке
        /// </summary>
        public string ecAdditionalInfoRu { get; set; }
        /// <summary>
        /// Доп. информация на казахском языке
        /// </summary>
        public string ecAdditionalInfoKz { get; set; }
        /// <summary>
        /// № заключения
        /// </summary>
        public string ecNumber { get; set; }

        public string ecApplicationNumber { get; set; }
        public string ecBlankNumber { get; set; }

        public bool ecExpApplication { get; set; }
    }
}
