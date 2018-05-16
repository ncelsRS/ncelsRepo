using System;
using System.Collections.Generic;
using Ncels.Teme.Contracts;
using Ncels.Teme.Infrastructure.ContractStage;
using PW.Ncels.Database.Constants;

namespace Ncels.Teme.Infrastructure
{
    public class EmpContractStageProcessorFactory
    {
        private readonly Dictionary<string, Func<IEmpContractStageProcessor>> _processors;

        public EmpContractStageProcessorFactory(IUnitOfWork uow)
        {
            _processors = new Dictionary<string, Func<IEmpContractStageProcessor>>
            {
                {CodeConstManager.EmpContractStage.Coz, () => new EmpContractCozStageProcessor(uow) },
                {CodeConstManager.EmpContractStage.LegalDepartmant, () => new EmpContractLegalDepartmentStageProcessor(uow) },
                {CodeConstManager.EmpContractStage.ValidationGroup, () => new EmpContractValidationGroupStageProcessor(uow) },
                {CodeConstManager.EmpContractStage.Def, () => new EmpContractDefStageProcessor(uow) },
                {CodeConstManager.EmpContractStage.Ceo, () => new EmpContractCeoStageProcessor(uow) }
            };
        }

        public IEmpContractStageProcessor Create(string stageCode)
        {
            return _processors[stageCode].Invoke();
        }
    }
}
