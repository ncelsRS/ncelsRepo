using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractCoz
{
    public class SelectExecutorsFirst : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public SelectExecutorsFirst(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                Log.Verbose("Run SelectExecutorsFirst");
                var attrs = context.GetParentScope(1).ExtensionAttributes;
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                //var pointerId = ""; // Helpers.GetUserTaskPointer(context);
                //var pointer = context.Workflow.ExecutionPointers.Find(x => x.Id == pointerId);

                //if (pointer.ExtensionAttributes.TryGetValue("ExecutorsIds", out object executorsValue))
                //{
                //    var executors = executorsValue as Dictionary<string, IEnumerable<string>>;
                //    executors.Keys.ToList().ForEach(key =>
                //    {
                //        ExecutorsIds.Remove(key);
                //        ExecutorsIds.Add(key, executors[key]);
                //    });
                //}
                _ss.SelectExecutorsFirst(context.Workflow.Id);                
            }
            catch (Exception ex)
            {
                Log.Error("SelectExecutorsFirst", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class CozExecutorMeetReq : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CozExecutorMeetReq(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public bool Agreed { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Verbose("Run CozExecutorMeetReq");
            var attrs = context.GetParentScope(1).ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Data", out var agreedValue))
                Agreed = (bool)agreedValue;
            //_ss.CozExecutorMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CozBossMeetReq : StepBody
    {
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public bool Agreed { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Verbose("Run CozBossMeetReq");
            var attrs = context.GetParentScope(1).ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Data", out var agreedValue))
                Agreed = (bool)agreedValue;
            //var pointerId = ""; // Helpers.GetUserTaskPointer(context);
            //var pointer = context.Workflow.ExecutionPointers.Find(x => x.Id == pointerId);

                //if (pointer.ExtensionAttributes.TryGetValue("ExecutorsIds", out object executorsValue))
                //{
                //    var executors = executorsValue as Dictionary<string, IEnumerable<string>>;
                //    executors.Keys.ToList().ForEach(key =>
                //    {
                //        ExecutorsIds.Remove(key);
                //        ExecutorsIds.Add(key, executors[key]);
                //    });
                //}

            return ExecutionResult.Next();
        }
    }
}
