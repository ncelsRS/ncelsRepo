using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKTaskListResearchCenter
    {
        public int ProductSeriesId { get; set; }
        public string ProductNameRu { get; set; }
        public string ProductNameKz { get; set; }
        public string Series { get; set; }
        public string SeriesMeasure { get; set; }
        public string SeriesStartdate { get; set; }
        public string SeriesEndDate { get; set; }
        public string SeriesParty { get; set; }
        public int? Quantity { get; set; }
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// условия хранения
        /// </summary>
        public Guid StorageCondition { get; set; }
        /// <summary>
        /// Внешнее состояние
        /// </summary>
        public Guid ExternalCondition { get; set; }
        /// <summary>
        /// выбранные лаборатории
        /// </summary>
        public List<OBKLaboratory> Laboratory { get; set; }

        public List<SelectedResearchCenter> SelectedResearchCenter { get; set; }

        public List<SelectedQuantity> SelectedQuantity { get; set; }

        /// <summary>
        /// Размерность ИМН
        /// </summary>
        public string DimensionIMN { get; set; }

    }
    public class OBKLaboratory
    {
        public Guid LaboratoryId { get; set; }
        public string LaboratoryName { get; set; }
    }
    /// <summary>
    /// Наименование лабораторий
    /// </summary>
    public class SelectedResearchCenter
    {
        public Guid LaboratoryId { get; set; }
        public Guid ResearchcenterId { get; set; }
    }

    /// <summary>
    /// Кол-во переденных образцов по лоборавториям 
    /// </summary>
    public class SelectedQuantity
    {
        public Guid LaboratoryId { get; set; }
        public int Quantity { get; set; }
    }
}
