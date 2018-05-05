using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic.ContractLogic;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    /// <summary>
    /// Удаление договора
    /// </summary>
    public class Delete : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public Delete(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _ss.DeleteContract(context.Workflow.Id);
            TaskCompletionService.ReleaseAll(context.Workflow.Id);
            Log.Verbose($"Delete contract, workflowId: {context.Workflow.Id}");

            return ExecutionResult.Next();
        }
    }

    /// <summary>
    /// Отправка договора в НЦЭЛС с подписью или без
    /// </summary>
    public class SendToNcels : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public SendToNcels(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public ContractTypeEnum ContractType { get; set; }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var workflowId = context.Workflow.Id;
            _ss.SendToNcels(workflowId);
            var attrs = context.GetParentScope(1).ExtensionAttributes;
            if (attrs.TryGetValue("Data", out var contractType))
                ContractType = (ContractTypeEnum)contractType;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;

            Log.Information($"SendToNcels, ContractType = {ContractType.ToString()}");
            return ExecutionResult.Next();
        }
    }
}