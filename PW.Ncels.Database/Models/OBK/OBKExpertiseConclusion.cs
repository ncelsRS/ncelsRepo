using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKExpertiseConclusion
    {
        public Guid AssessmentDeclarationId { get; set; }
        public string AssessmentDeclarationType { get; set; }
        public List<ExpertiseConclusion> ExpertiseConclusion { get; set; }
    }

    public class ExpertiseConclusion
    {
        public int ProductSeriesId { get; set; }
        public string ProductNameRu { get; set; }
        public string ProductNameKz { get; set; }
        public string ProductSeries { get; set; }
        public string SeriesParty { get; set; }
        public string ResearchCenterResultName { get; set; }
        public int ResearchCenterResult { get; set; }

        //public bool BtnValid { get; set; }
    }
}
