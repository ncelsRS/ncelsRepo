using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Infrastructure.Primitives
{
    public class ContractWorkflowTransitionData
    {
        public int ContractId { get; set; }
        public bool IsSignedByDeclarant { get; set; }
        public ContractTypeEnum ContractType { get; set; }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public object Value { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }
    }

    public class ContractWorkflowEventData
    {
        public string AwaiterKey { get; set; }
        public object Value { get; set; }
    }
}
