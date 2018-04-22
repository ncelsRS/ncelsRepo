using System.Collections.Generic;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractCoz
{
    public class SelectExecutorsFirst : StepBody
    {
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var pointerId = Helpers.GetUserTaskPointer(context);
            var pointer = context.Workflow.ExecutionPointers.Find(x => x.Id == pointerId);

            if (pointer.ExtensionAttributes.TryGetValue("ExecutorsIds", out object executorsValue))
            {
                var executors = executorsValue as Dictionary<string, IEnumerable<string>>;
                executors.Keys.ToList().ForEach(key =>
                {
                    ExecutorsIds.Remove(key);
                    ExecutorsIds.Add(key, executors[key]);
                });
            }



            // TODO Contract states

            return ExecutionResult.Next();
        }
    }

    public class CozBossMeetReq : StepBody
    {

        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var pointerId = Helpers.GetUserTaskPointer(context);
            var pointer = context.Workflow.ExecutionPointers.Find(x => x.Id == pointerId);

            if (pointer.ExtensionAttributes.TryGetValue("ExecutorsIds", out object executorsValue))
            {
                var executors = executorsValue as Dictionary<string, IEnumerable<string>>;
                executors.Keys.ToList().ForEach(key =>
                {
                    ExecutorsIds.Remove(key);
                    ExecutorsIds.Add(key, executors[key]);
                });
            }

            return ExecutionResult.Next();
        }
    }
}
