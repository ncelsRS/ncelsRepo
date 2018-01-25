using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKSubTaskResult
    {
        public Guid Id { get; set; }
        public string ProductNameRu { get; set; }
        public string ProductNameKz { get; set; }
        public int RegisterId { get; set; }
        public string NdProduct { get;set; }
        public string Regulation { get; set; }
        public bool ExpertiseResult { get; set; }
        public string ExpertiseResultName { get; set; }
        public bool IsNew { get; set; }
        public string SubTaskNumber { get; set; }
        public DateTime? SubTaskCreateDate { get; set; }

        public string LaboratoryExpertName { get; set; }
        public string LaboratoryTypeName { get; set; }
        public string UnitLaboratoryName { get; set; }

        public List<SubTaskIndicator> SubTaskIndicator { get; set; }

        public List<SubTaskCoExecutorResults> SubTaskCoExecutorResult { get; set; }
    }

    public class SubTaskDetails
    {
        public List<OBKSubTaskResult> SubTaskResult { get; set; }
    }

    public class SubTaskIndicator
    {
        /// <summary>
        /// Показатель
        /// </summary>
        public Guid LaboratoryMarkId { get; set; }
        public string LaboratoryMarkNameRu { get; set; }
        public string LaboratoryMarkNameKz { get; set; }
        /// <summary>
        /// Обозначение нормативных документов на методы испытаний
        /// </summary>
        public Guid RegulationId { get; set; }
        public string LaboratoryRegulationNameRu { get; set; }
        public string LaboratoryRegulationNameKz { get; set; }
        /// <summary>
        /// Требования НД
        /// </summary>
        public string Claim { get; set; } 
        /// <summary>
        /// Влажность
        /// </summary>
        public string Humidity { get; set; }
        /// <summary>
        /// Фактическии полученный результат
        /// </summary>
        public string FactResult { get; set; }

        public bool ExpertiseResult { get; set; }
        public string ExpertiseResultName { get; set; }
        public bool ExecutorSign { get; set; }
    }

    public class SubTaskCoExecutorResults
    {
        public string SubTaskCoExecutorName { get; set; }
        public List<SubTaskIndicator> SubTaskIndicatorCoExecutor { get; set; }
    }
}
