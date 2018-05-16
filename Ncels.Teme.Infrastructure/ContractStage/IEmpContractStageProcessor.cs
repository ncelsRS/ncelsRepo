using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure.ContractStage
{
    public interface IEmpContractStageProcessor
    {
        void Handle(EMP_ContractStage stage, bool result);
    }
}
