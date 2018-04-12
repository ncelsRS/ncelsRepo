using System.ComponentModel.DataAnnotations;

namespace Teme.Contract.Infrastructure
{
    public class ContractWorkflowTransitionData
    {
        [Required]
        public string AwaiterKey { get; set; }
        public object Value { get; set; }
        //public string ExecutorType { get; set; }
        [Required]
        public int ContractType { get; set; }
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
