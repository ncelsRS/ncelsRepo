using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKExpertiseConclusionNegative
    {
        public Guid AssessmentDeclarationId { get; set; }
        public IEnumerable<SelectListItem> Reasons { get; set; }
    }
}
