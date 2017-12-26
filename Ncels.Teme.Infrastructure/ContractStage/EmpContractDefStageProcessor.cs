using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure.ContractStage
{
    public class EmpContractDefStageProcessor : IEmpContractStageProcessor
    {
        private IUnitOfWork _uow;

        public EmpContractDefStageProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(EMP_ContractStage stage, bool result)
        {
            if (stage.EMP_Ref_StageStatus.Code != CodeConstManager.EmpContractStageStatus.New) return;

            var handler = new EmpContractStageHistoryHandler(_uow);

            if (result)
            {
                handler.SetStageApproved(stage);
                handler.AddHistoryApproved(stage.ContractId);
            }
            else
            {
                handler.SetStageRejected(stage);
                handler.AddHistoryRejected(stage.ContractId);
                var legalStage = stage.EMP_Contract.EMP_ContractStage.FirstOrDefault(x =>
                    x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.LegalDepartmant);
                if (legalStage != null)
                    handler.SetStageRejected(legalStage);
            }

            var cozStage = stage.EMP_Contract.EMP_ContractStage.First(x => x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Coz);
            cozStage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired).Select(x => x.Id)
                .FirstOrDefault();

            _uow.Save();
        }
    }
}
