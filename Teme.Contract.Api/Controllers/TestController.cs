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
        public async Task<IActionResult> Start()
        {
            return Json(await _logic.StartWorkflow());
        }

        [Route("useractions")]
        [HttpGet]
        public async Task<IEnumerable<OpenUserAction>> UserActions([FromQuery]string workflowId)
        {
            return await _logic.OpenUserActions(workflowId);
        }

        public class PublishUserEventDto
        {
            public string Key { get; set; }
            public string ChosenValue { get; set; }
            public Dictionary<string, IEnumerable<string>> ExecutersIds { get; set; }
            public object Value { get; set; }
        }

        [Route("publish/useraction")]
        [HttpPost]
        public async Task<IActionResult> PublishUserAction([FromBody] PublishUserEventDto dto)
        {
            var res = await _logic.PublishUserAction(dto.Key, dto.ChosenValue, dto.ExecutersIds, dto.Value);
            return Json(res);
        }

    }
}
