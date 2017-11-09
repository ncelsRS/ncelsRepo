using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_ExpertiseStageDosage
    {
        public EXP_ExpertisePrimaryFinalDoc EXP_ExpertisePrimaryFinalDoc { get; set; }
        public EXP_ExpertisePharmaceuticalFinalDoc PharmaceuticalFinalDoc { get; set; }
        public EXP_ExpertisePharmacologicalFinalDoc ExpertisePharmacologicalFinalDoc { get; set; }
        public EXP_ExpertiseSafetyreportFinalDoc ExpertiseSafetyreportFinalDoc { get; set; }
        public List<EXP_DrugAnaliseIndicator> ExpDrugAnaliseIndicators { get; set; }
    }
}
