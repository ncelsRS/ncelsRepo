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
        public int InputCounter { get; set; }
        public int OutputCounter { get; set; }

        public CounterIncrement(int? InputCounter)
        {
            this.InputCounter = InputCounter ?? 0;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Log.Information("CounterIncrement");
            await Task.Run(() =>
            {
                OutputCounter = ++InputCounter;
            });
            return ExecutionResult.Next();
        }
    }

    public class CounterPrint : StepBodyAsync
    {
        public int InputCounter { get; set; }
        public int OutputCounter { get; set; }

        public CounterPrint(int? InputCounter)
        {
            this.InputCounter = InputCounter ?? 0;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Log.Information("CounterPrint");
            await Task.Run(() =>
            {
                Log.Information($"Counter value is: {InputCounter}");
                OutputCounter = InputCounter;
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
