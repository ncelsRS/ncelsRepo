using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure.ContractStage
{
    public class EmpContractLegalDepartmentStageProcessor : IEmpContractStageProcessor
    {
        private IUnitOfWork _uow;

        public EmpContractLegalDepartmentStageProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(EMP_ContractStage stage, bool result)
        {
            if (stage.EMP_Ref_StageStatus.Code != CodeConstManager.EmpContractStageStatus.InWork) return;

            var handler = new EmpContractStageHistoryHandler(_uow);
            
            if (result)
            {
                handler.SetStageApproved(stage);
                handler.AddHistoryApproved(stage.ContractId);

                var wasInAdjustment = stage.EMP_Contract.EMP_ContractHistory.Any(x =>
                    x.OBK_Ref_ContractHistoryStatus.Code == OBK_Ref_ContractHistoryStatus.Returned);
                if (wasInAdjustment)
                {
                    var cozStage = stage.EMP_Contract.EMP_ContractStage.First(x => x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Coz);
                    cozStage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired).Select(x => x.Id)
                        .FirstOrDefault();
                }
            }
            else
            {
                handler.SetStageRejected(stage);
                handler.AddHistoryRejected(stage.ContractId);
            }

            _uow.Save();
        }
    }
}
