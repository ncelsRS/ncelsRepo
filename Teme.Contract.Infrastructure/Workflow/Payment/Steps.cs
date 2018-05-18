using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    /// <summary>
    /// Отправка заявки в НЦЭЛС с подписью или без
    /// </summary>
    public class SendPaymentToNcels : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public SendPaymentToNcels(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.SendPaymentToNcels(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class SelectPaymentExecutors : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public SelectPaymentExecutors(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.SelectPaymentExecutors(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class GvExecutorMeet : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public GvExecutorMeet(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.GvExecutorMeet(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class GvExecutorNotMeet : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public GvExecutorNotMeet(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.GvExecutorNotMeet(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }


    public class GvBossMeet : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public GvBossMeet(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.GvBossMeet(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class GvBossNotMeet : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public GvBossNotMeet(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.GvBossNotMeet(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class DefExecutorMeet : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public DefExecutorMeet(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.DefExecutorMeet(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class DefExecutorNotMeet : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public DefExecutorNotMeet(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.DefExecutorNotMeet(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }

    public class RegisterPayment : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public RegisterPayment(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                _ss.RegisterPayment(context.Workflow.Id);
                var attrs = context.GetParentScope().ExtensionAttributes;
                if (attrs == null || attrs.Count <= 0)
                    throw new NullReferenceException();
                if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                if (attrs.TryGetValue("Agreements", out var agreementsValue))
                    Agreements = agreementsValue as Dictionary<string, bool>;
                Log.Information("SendToNcels");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }
}
