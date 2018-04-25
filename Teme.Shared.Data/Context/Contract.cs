using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Shared.Data.Context
{
    public class Contract : BaseEntity
    {
        public string WorkflowId { get; set; }
        public ContractTypeEnum ContractType { get; set; }
    }
}