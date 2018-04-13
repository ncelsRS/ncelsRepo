using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Api.Controllers
{

    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private ContractLogic _logic { get; }

        public TestController(ContractLogic logic)
        {
            _logic = logic;
        }

        [Route("start")]
        public async Task<string> Start()
        {
            return await _logic.StartWorkflow();
        }

        [Route("useractions")]
        [HttpGet]
        public async Task<IEnumerable<OpenUserAction>> UserActions([FromQuery]string workflowId)
        {
            return await _logic.OpenUserActions(workflowId);
        }

        [Route("publish/useraction")]
        public async Task<string> PublishUserAction([FromQuery] string key, [FromQuery] string username, [FromQuery] string chosenValue, [FromQuery]object value)
        {
            return await _logic.PublishUserAction(key, username, chosenValue, value);
        }

    }
}
