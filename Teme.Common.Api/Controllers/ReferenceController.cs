using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Common.Logic;
using Teme.SharedApi.Controllers;

namespace Teme.Common.Api.Controllers
{
    public class ReferenceController : BaseController<IReferenceLogic>
    {
        public ReferenceController(IReferenceLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <param name="page"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public async Task<IActionResult> GosRegistrySearch(string name, string culture, int page, int counter)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.GetGosRegistry(name, culture, page, counter);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
