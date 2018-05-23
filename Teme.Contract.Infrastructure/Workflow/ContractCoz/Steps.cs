using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractCoz
{




    public class Test : StepBody
    {
        public Test()
        {
        }

        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
        }
    }

    public class Test1 : StepBody
    {
        public Test1()
        {
        }

        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
        }
    }







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
                Log.Information("Run SelectExecutorsFirst");
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
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
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CozExecutorMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreedValue))
                Agreements = agreedValue as Dictionary<string, bool>;
            _ss.CozExecutorMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CozExecutorNotMeetReq : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CozExecutorNotMeetReq(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CozExecutorMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreedValue))
                Agreements = agreedValue as Dictionary<string, bool>;
            _ss.CozExecutorNotMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CozReturnToDeclarant : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CozReturnToDeclarant(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CozExecutorMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreedValue))
                Agreements = agreedValue as Dictionary<string, bool>;
            _ss.CozReturnToDeclarant(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CozBossMeetReq : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CozBossMeetReq(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CozBossMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreementsValue))
                Agreements = agreementsValue as Dictionary<string, bool>;
            _ss.CozBossMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CozBossNotMeetReq : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CozBossNotMeetReq(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CozBossMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreementsValue))
                Agreements = agreementsValue as Dictionary<string, bool>;
            _ss.CozBossNotMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CeoMeetReq : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CeoMeetReq(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CeoMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreementsValue))
                Agreements = agreementsValue as Dictionary<string, bool>;
            _ss.CeoMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class CeoNotMeetReq : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public CeoNotMeetReq(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information("Run CeoMeetReq");
            var attrs = context.GetParentScope().ExtensionAttributes;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
            if (attrs.TryGetValue("Agreements", out var agreementsValue))
                Agreements = agreementsValue as Dictionary<string, bool>;
            _ss.CeoNotMeetReq(context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }
}
