using Microsoft.AspNetCore.Mvc;
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
        public ActionResult GetContract()
        {
            return File(_templateLogic.GetContract(), "application/pdf");
            //return File(_templateLogic.GetContract(), "application/msword");
        }
    }
}
