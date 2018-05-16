using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Teme.Template.Logic;

namespace Teme.Template.Api.Controllers
{
    [Route("api/[controller]")]
    public class TemplateController : Controller
    {
        private TemplateLogic _templateLogic;

        public TemplateController(TemplateLogic templateLogic)
        {
            _templateLogic = templateLogic;
        }

        [Route("GetContract")]
        [HttpGet]
        public async Task<ActionResult> GetContract(int contractId)
        {
            return File(await _templateLogic.GetContract(contractId), "application/pdf");
            //return File(_templateLogic.GetContract(), "application/msword");
        }
    }
}
