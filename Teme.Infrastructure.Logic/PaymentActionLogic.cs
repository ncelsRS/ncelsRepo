using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;

namespace Teme.Infrastructure.Logic
{
    public class PaymentActionLogic : EntityLogic, IPaymentActionLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        public PaymentActionLogic(IEntityRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
        }

        public async Task<object> CreatePayment()
        {
            return await _wflogic.CreatePayment("Payment");
        }

        public async Task<object> PublishUserAction(string userPromt, string userOption, object value, string workflowId, IEnumerable<string> executors = null, Dictionary<string, bool> agreements = null)
        {
            var actions = await _wflogic.GetUserActions(workflowId);
            var action = actions
                .FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            if (action == null) throw new ArgumentException("Action not found");
            var executorsIds = new Dictionary<string, IEnumerable<string>> { { ScopeEnum.Gv, executors }, { ScopeEnum.Def, executors } };
            var result = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds, value, agreements);
            return new { result };
        }
    }
}
