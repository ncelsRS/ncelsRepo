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
        public bool ToShow { get; set; }
        public Guid AssessmentDeclarationId { get; set; }
        public int ProductSeriesId { get; set; }
        public string ExpReasonNameRu { get; set; }
        public string ExpReasonNameKz { get; set; }
        public int RefReasonId { get; set; }
        public IEnumerable<SelectListItem> Reasons { get; set; }
    }
}
