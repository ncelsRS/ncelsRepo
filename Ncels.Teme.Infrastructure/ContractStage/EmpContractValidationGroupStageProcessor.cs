using System;
using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure.ContractStage
{
    public class EmpContractValidationGroupStageProcessor : IEmpContractStageProcessor
    {
        private IUnitOfWork _uow;

        public EmpContractValidationGroupStageProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(EMP_ContractStage stage, bool result)
        {
            var handler = new EmpContractStageHistoryHandler(_uow);

            if (stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.InWork)
            {
                stage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                    .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired).Select(x => x.Id)
                    .FirstOrDefault();

                if (result) handler.AddHistoryApproved(stage.ContractId);
                else handler.AddHistoryRejected(stage.ContractId);

                _uow.Save();
                return;
            }

            if (stage.EMP_Ref_StageStatus.Code == CodeConstManager.EmpContractStageStatus.ApprovalRequired)
            {
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

                var defStage = new EMP_ContractStage
                {
                    Id = Guid.NewGuid(),
                    DateCreate = DateTime.Now,
                    ContractId = stage.ContractId,
                    StageId = _uow.GetQueryable<EMP_Ref_Stage>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStage.Def).Select(x => x.Id)
                        .FirstOrDefault(),
                    StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                        .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.New).Select(x => x.Id)
                        .FirstOrDefault()
                };
                _uow.Insert(defStage);

                var defUnit = _uow.GetQueryable<Unit>().First(x => x.Code == "finance");
                _uow.Insert(new EMP_ContractStageExecutors
                {
                    Id = Guid.NewGuid(),
                    EMP_ContractStage = defStage,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR,
                    ExecutorId = Guid.Parse(defUnit.BossId)
                });

                _uow.Save();
                return;
            }
        }
    }
}
