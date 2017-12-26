using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure.ContractStage
{
    public class EmpContractCeoStageProcessor : IEmpContractStageProcessor
    {
        private IUnitOfWork _uow;

        public EmpContractCeoStageProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(EMP_ContractStage stage, bool result)
        {
            if (stage.EMP_Ref_StageStatus.Code != CodeConstManager.EmpContractStageStatus.ApprovalRequired) return;

            var handler = new EmpContractStageHistoryHandler(_uow);
            var legalStage = stage.EMP_Contract.EMP_ContractStage.FirstOrDefault(x =>
                x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.LegalDepartmant);

            if (result)
            {
                handler.SetStageApproved(stage);
                handler.AddHistoryApproved(stage.ContractId);

                if (legalStage != null)
                    legalStage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.RegistrationRequired)
                        .Select(x => x.Id).FirstOrDefault();
            }
            else
            {
                handler.SetStageRejected(stage);
                handler.AddHistoryRejected(stage.ContractId);
                if (legalStage != null)
                    handler.SetStageRejected(legalStage);
            }

            _uow.Save();
        }
    }
}
