using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Shared.Logic;
using Teme.Shared.Logic.RefBaseLogic;
using Teme.SharedApi.Controllers;

namespace Teme.SharedApi.Controllers
{
    public class BaseRefCrudController<TEntity> : BaseController<IRefBaseLogic<TEntity>>
    {
        public BaseRefCrudController(IRefBaseLogic<TEntity> logic) : base(logic)
        {
        }

        [HttpPut]
        [Route("Add")]
        public async Task<IActionResult> Add(TEntity entity)
        {
            try
            {
                return Json(await Logic.Add(entity));
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Logic.Delete(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(TEntity entity)
        {
            try
            {
                await Logic.Update(entity);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var aa = await Logic.GetById(id);
                return Ok(await Logic.GetById(id));
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
