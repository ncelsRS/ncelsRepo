using System.Collections.Generic;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models.Expertise
{
    public class StageModel
    {
        public EXP_ExpertiseStage CurrentStage { get; set; }
        public IEnumerable<EXP_DIC_StageResult> StageResults { get; set; }
    }
}