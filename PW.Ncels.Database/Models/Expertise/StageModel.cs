using System.Collections.Generic;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Models.Expertise
{
    public class StageModel
    {
        public EXP_ExpertiseStage CurrentStage { get; set; }
        public IEnumerable<EXP_DIC_StageResult> StageResults { get; set; }

        public bool CurrentUserIsHead
        {
            get
            {
                if (CurrentStage == null || CurrentStage.EXP_ExpertiseStageExecutors == null) return false;
                return CurrentStage.EXP_ExpertiseStageExecutors.Any(
                    e => e.IsHead && e.ExecutorId == UserHelper.GetCurrentEmployee().Id);
            }
        }
    }
}