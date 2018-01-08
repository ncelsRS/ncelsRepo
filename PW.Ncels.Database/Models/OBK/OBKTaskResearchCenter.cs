using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKTaskResearchCenter
    {
        public Guid AssessmentDeclarationId { get; set; }
        public Guid TaskId { get; set; }
        public Guid UnitLaboratoryId { get; set; }
        public string TaskNumber { get; set; }
        public string RegisterDate { get; set; }
        public string ActNumber { get; set; }
        public string UnitName { get; set; }
        public int? TaskStatusId { get; set; }
        public bool IsShow { get; set; }
        public IEnumerable<SelectListItem> StorageConditions { get; set; }
        public IEnumerable<SelectListItem> ExternalConditions { get; set; }
        public IEnumerable<SelectListItem> Researchcenters { get; set; }
        public List<OBKTaskListResearchCenter> TaskListResearchCenter { get; set; }
    }
}
