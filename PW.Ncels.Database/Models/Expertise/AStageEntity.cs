using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Models.Expertise
{
    public abstract class AStageEntity : IStageEntity
    {
        public string DrugDeclarationId { get; set; }
        public EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        public Employee Applicant { get; set; }
        public Employee Editor { get; set; }

        public virtual List<EXP_ExpertiseStageRemark> ExpExpertiseStageRemarks { get; set; }
        public virtual List<EXP_DrugCorespondence> ExpDrugCorespondences { get; set; }
        public Guid ExpStageId { get; set; }
        public StageModel CurrentStage { get; set; }
    }

    public interface IStageEntity
    {
        string DrugDeclarationId { get; set; }
        Guid ExpStageId { get; set; }
        List<EXP_ExpertiseStageRemark> ExpExpertiseStageRemarks { get; set; }
        List<EXP_DrugCorespondence> ExpDrugCorespondences { get; set; }
    }

    public class CorespondenceEntity
    {
        public bool IsSuccess { get; set; }
        public int StageType { get; set; }
    }
}
