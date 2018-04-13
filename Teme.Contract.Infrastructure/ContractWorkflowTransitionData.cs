using System.ComponentModel.DataAnnotations;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Infrastructure
{
    public class ContractWorkflowTransitionData
    {
        [Required]
        public string AwaiterKey { get; set; }
        public object Value { get; set; }
        //public string ExecutorType { get; set; }
        [Required]
        public ContractTypeEnum ContractType { get; set; }
        [Required]
        public string ExecutorId { get; set; }
        public bool IsSignedByDeclarant { get; set; }
        
    }

    public class ContractWorkflowEventData
    {
        public string AwaiterKey { get; set; }
        public object Value { get; set; }
    }
}
