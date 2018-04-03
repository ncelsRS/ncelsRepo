using Serilog;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.WorkflowSteps
{
    public class A1Step : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("A1Step");
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
        public object Value { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Log.Information("Finish");
            await Task.Run(() =>
            {
                var id = Value.GetType().GetProperty("id").GetValue(Value).ToString();
                Log.Information("Job finisht with id: " + id);
            });
            return ExecutionResult.Next();
        }
    }
}
