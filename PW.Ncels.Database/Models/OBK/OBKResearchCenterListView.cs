using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKResearchCenterListView
    {
        public Guid TaskId { get; set; }
        public string TaskNumber { get; set; }
        public string RegisterDate { get; set; }
        public string TaskEndDate { get; set; }
        public string LaboratoryExecutorShortName { get; set; }
        public string StageStatusCode { get; set; }
        public Guid UnitLaboratoryId { get; set; }
    }
}
