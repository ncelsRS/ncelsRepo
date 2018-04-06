using Serilog;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure
{

    public class ContractWorkflow : IWorkflow<ContractWorkflowTransitionData>
    {
        public string Id => "Contract";

        public int Version => 0; // Test version

        public void Build(IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder
                .StartWith<SetWorkflowId>()
                    .Input(step => step.AwaiterKey, data => data.AwaiterKey)
                .UserTask("Filling contract", data => data.ExecutorId) // TODO emplement check for executorId
                    .WithOption("sendWithoutSign", "").Do(then => then
                        .StartWith<SendWithoutSign>()
                    )
                    .WithOption("sendWithSign", "").Do(then => then
                        .StartWith<SendWithSign>()
                    )
                    .Then(context => ExecutionResult.Next());
        }
    }
}
