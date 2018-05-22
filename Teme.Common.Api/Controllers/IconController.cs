using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Common.Data.InDto;
using Teme.Common.Logic.Icons;
using Teme.SharedApi.Controllers;

namespace Teme.Common.Api.Controllers
{
    public class IconController : BaseController<IIconLogic>
    {
        public IconController(IIconLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Созадение коментарии
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateIcon")]
        public async Task<IActionResult> CreateIcon([FromBody][Required]IconCreateInDto iconCreateInDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CreateIconRecord(iconCreateInDto);
                return Json(r);
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
        [Route("GetIconRecords")]
        public async Task<IActionResult> GetIconRecords([FromQuery] [Required] IconGetInDto gid)
        {
            try
            {
                var result = await Logic.GetIconRecords(gid);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Изменение статуса Айки
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateIconError")]
        public async Task<IActionResult> UpdateIconError([FromBody] IconCreateInDto iconCreateInDt)
        {
            try
            {
                var result = await Logic.UpdateIconError(iconCreateInDt);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
