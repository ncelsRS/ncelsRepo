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
