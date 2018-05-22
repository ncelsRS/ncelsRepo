using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teme.Declaration.Data.Model;
using Teme.Declaration.Logic;
using Teme.SharedApi.Controllers;

namespace Teme.Declaration.Api.Controllers
{
    public class DeclarationController : BaseController<IDeclarationLogic>
    {
        public DeclarationController(IDeclarationLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Сохранение заявления
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeModel")]
        public async Task<IActionResult> ChangeModel([FromBody][Required] DeclarationUpdateModel value)
        {
            try
            {
                var result = await Logic.ChangeModel(value);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
