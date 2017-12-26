using System;
using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure.ContractStage
{
    public class EmpContractCozStageProcessor : IEmpContractStageProcessor
    {
        private IUnitOfWork _uow;

        public EmpContractCozStageProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(EMP_ContractStage stage, bool result)
        {
            if (stage.EMP_Ref_StageStatus.Code != CodeConstManager.EmpContractStageStatus.ApprovalRequired) return;

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

            var legalStage =  stage.EMP_Contract.EMP_ContractStage.FirstOrDefault(x =>
                x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.LegalDepartmant);
            
            if (!result || IsRejectedInOtherStages(stage.EMP_Contract))
            {
                if (legalStage != null)
                    handler.SetStageRejected(legalStage);
            }
            else
            {
                if (legalStage != null)
                    legalStage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.Approved).Select(x => x.Id).FirstOrDefault();

                var ceoStage = new EMP_ContractStage
                {
                    Id = Guid.NewGuid(),
                    ContractId = stage.ContractId,
                    DateCreate = DateTime.Now,
                    StageId = _uow.GetQueryable<EMP_Ref_Stage>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStage.Ceo).Select(x => x.Id)
                        .FirstOrDefault(),
                    StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired)
                        .Select(x => x.Id).FirstOrDefault()
                };
                _uow.Insert(ceoStage);

                var ceoExecutor = new EMP_ContractStageExecutors
                {
                    Id = Guid.NewGuid(),
                    EMP_ContractStage = ceoStage,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER,
                    ExecutorId = _uow.GetQueryable<Unit>()
                        .Where(x => x.Code == "ncels_deputyceo" && x.EmployeeId != null)
                        .Select(x => x.EmployeeId.Value).FirstOrDefault()
                };
                _uow.Insert(ceoExecutor);
            }

            _uow.Save();
        }

        private bool IsRejectedInOtherStages(EMP_Contract contract)
        {
            var skipOtherStages = contract.EMP_ContractHistory.Any(x =>
                x.OBK_Ref_ContractHistoryStatus.Code == OBK_Ref_ContractHistoryStatus.Returned);
            if (skipOtherStages) return false;

            var validationGroupStage = contract.EMP_ContractStage.First(x =>
                x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.ValidationGroup);
            var defStage = contract.EMP_ContractStage.First(x =>
                x.EMP_Ref_Stage.Code == CodeConstManager.EmpContractStage.Def);

            var codes = new[] { validationGroupStage.EMP_Ref_StageStatus.Code, defStage.EMP_Ref_StageStatus.Code };
            return codes.Contains(CodeConstManager.EmpContractStageStatus.NotApproved);
        }
    }
}
