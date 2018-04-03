using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Teme.Contract.Logic;

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

        [Route("test")]
        public async Task<string> Test()
        {
            return await _logic.Test();
        }
    }
}
