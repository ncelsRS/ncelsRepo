using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKManagerResearchCenter
    {
        public Guid AssessmentDeclarationId { get; set; }
        public Guid TaskId { get; set; }
        public Guid BossId { get; set; }
        public IEnumerable<SelectListItem> BossSelectList { get; set; }
        public List<UnitLaboratories> UnitLaboratory { get; set; }
        public List<ManagerLaboratories> ManagerLaboratory { get; set; }
    }

    public class UnitLaboratories
    {
        public Guid Id { get; set; }
        public string UnitDisplayName { get; set; }
        public IEnumerable<SelectListItem> ExecutorLaboratory { get; set; }
    }

    public class ManagerLaboratories
    {
        public Guid UnitLaboratoryId { get; set; }
        public Guid ExecutorId { get; set; }
    }
}
