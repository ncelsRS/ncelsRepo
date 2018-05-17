using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.ContractCoz.Logic;
using Teme.SharedApi.Controllers;

namespace Teme.ContractCoz.Api.Controllers
{
    public class ContractController : BaseController<IContractCozLogic>
    {
        public ContractController(IContractCozLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Получение договора по ID
        /// </summary>
        /// <param name="id">Id договора</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetContractById")]
        public async Task<IActionResult> GetContractById([FromQuery][Required] int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await Logic.GetContractById(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Заявитель, Производитель или Плательщик по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDeclarantById")]
        public async Task<IActionResult> GetDeclarantById([FromQuery][Required] int id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await Logic.GetDeclarantById(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Получение списка договоров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListContract")]
        public async Task<IActionResult> GetListContract([FromQuery][Required] string statusCode, [FromQuery][Required] string permission)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await Logic.GetListContract(statusCode, permission);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
