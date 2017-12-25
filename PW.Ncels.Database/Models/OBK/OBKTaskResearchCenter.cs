using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<OBKTaskListResearchCenter> TaskListResearchCenter { get; set; }
    }
}
