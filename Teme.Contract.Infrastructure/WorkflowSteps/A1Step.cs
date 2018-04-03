using Serilog;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.WorkflowSteps
{
    public class A1Step : StepBody
    {
        public int Counter { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("A1Step");
            Counter = 0;
            return ExecutionResult.Next();
        }
    }

    public class CounterIncrement : StepBodyAsync
    {
        public int Counter { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Log.Information("CounterIncrement");
            await Task.Run(() =>
            {
                Counter++;
            });
            return ExecutionResult.Next();
        }
    }

    public class CounterPrint : StepBodyAsync
    {
        public int Counter { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Log.Information("CounterPrint");
            await Task.Run(() =>
            {
                Log.Information($"Counter value is: {Counter}");
                Counter = Counter;
            });
            return ExecutionResult.Next();
        }
    }

    public class Finish : StepBodyAsync
    {
        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Log.Information("Finish");
            await Task.Run(() =>
            {
                Log.Information("Job finisht");
            });
            return ExecutionResult.Next();
        }
    }
}
