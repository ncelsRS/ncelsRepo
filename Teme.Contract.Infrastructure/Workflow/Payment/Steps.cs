using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Logic.Clients;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    /// <summary>
    /// Отправка заявки в НЦЭЛС с подписью или без
    /// </summary>
    public class SendToNcels : StepBody
    {
        private readonly IStepStatusLogic _ss;
        public SendToNcels(IStepStatusLogic ss)
        {
            _ss = ss;
        }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreements { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                //_ss.SendToNcels(context.Workflow.Id);
                //var attrs = context.GetParentScope().ExtensionAttributes;
                //if (attrs == null || attrs.Count <= 0)
                //    throw new NullReferenceException();
                //if (attrs.TryGetValue("Data", out var contractType))
                //    ContractType = (ContractTypeEnum)Convert.ToInt32(contractType);
                //if (attrs.TryGetValue("ExecutorsIds", out var executorsValue))
                //    ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;
                //if (attrs.TryGetValue("Agreements", out var agreementsValue))
                //    Agreements = agreementsValue as Dictionary<string, bool>;
                //Log.Information($"SendToNcels, ContractType = {ContractType.ToString()}");
            }
            catch (Exception ex)
            {
                Log.Error("SendToNcels", ex);
            }
            return ExecutionResult.Next();
        }
    }
}
