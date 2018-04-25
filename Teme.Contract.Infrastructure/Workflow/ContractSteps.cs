using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    /// <summary>
    /// Отправка договора в НЦЭЛС
    /// </summary>
    public class SendToNcels : StepBody
    {
        public ContractTypeEnum ContractType { get; set; }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var attrs = context.GetParentScope(2).ExtensionAttributes;

            if (attrs.TryGetValue("Data", out var contractType))
                ContractType = (ContractTypeEnum) contractType;
            if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;

            Log.Information($"SendToNcels, ContractType = {ContractType.ToString()}");
            return ExecutionResult.Next();
        }
    }
}