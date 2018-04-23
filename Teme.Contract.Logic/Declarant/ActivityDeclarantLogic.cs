using System.Threading.Tasks;
using Teme.Contract.Infrastructure;

namespace Teme.Contract.Logic.Declarant
{
    public class ActivityDeclarantLogic : IActivityDeclarantLogic
    {
        private readonly string _repo;
        private readonly IContractWorkflowLogic _wflogic;

        public ActivityDeclarantLogic(string repo, IContractWorkflowLogic wflogic)
        {
            _repo = repo;
            _wflogic = wflogic;
        }

        public async Task<string> Create()
        {
            // TODO Save contract and return needed fields
            var workflowId = await _wflogic.Create();
            return workflowId;
        }

        public Task<object> SendToNcels(string contractId)
        {
            throw new System.NotImplementedException();
        }
    }
}