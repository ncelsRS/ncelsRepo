using System.ComponentModel.DataAnnotations;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Infrastructure
{
    public class ContractWorkflowTransitionData
    {
        public int ContractId { get; set; }
        public bool IsSignedByDeclarant { get; set; }
        public ContractTypeEnum ContractType { get; set; }
        public object Value { get; set; }
    }

    public class ContractWorkflowEventData
    {
        public string AwaiterKey { get; set; }
        public object Value { get; set; }
    }
}
