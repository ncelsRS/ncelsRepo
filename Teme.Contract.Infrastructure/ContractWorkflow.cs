using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure
{
    public class ContractWorkflow : IWorkflow<int>
    {
        public string Id => "Contract";

        public int Version => 0; // Test version

        public void Build(IWorkflowBuilder<int> builder)
        {
            builder.StartWith<A1Step>()
                .While(data => data > 15)
                    .Do(x => x
                        .StartWith<CounterIncrement>()
                            .Input(step => step.InputCounter, data => data)
                            .Output(data => data, step => step.OutputCounter)
                        .Then<CounterPrint>()
                            .Input(step => step.InputCounter, data => data)
                            .Output(data => data, step => step.OutputCounter))
                .Then<Finish>();
        }
    }
}
