using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKProductViewModel
    {
        /// <summary>
        /// id серии
        /// </summary>
        public int Id { get; set; }
        public string ProductNameRu { get; set; }
        public string ProductNameKz { get; set; }
        public string Series { get; set; }
        public string SeriesMeasure { get; set; }
        public string SeriesStartdate { get; set; }
        public string SeriesEndDate { get; set; }
        public int? Quantity { get; set; }
        /// <summary>
        /// номер задания
        /// </summary>
        public string TaskNumber { get; set; }
        /// <summary>
        /// выбранные лаборатории
        /// </summary>
        public List<string> LaboratoryTypeIds { get; set; }
        public string LaboratoryName { get; set; }

        #region задания на уровне цоз

        public string Comment { get; set; }
        public bool SelectProductSeries { get; set; }

        #endregion

    }
}