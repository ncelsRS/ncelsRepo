using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Data.Model;
using Teme.Contract.Logic;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Api.Controllers
{
    public class ContractController : BaseController<IContractLogic>
    {
        public ContractController(IContractLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Созадение договора
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromQuery] ContractTypeEnum contractType, [FromQuery] string contractScope)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
                var r = await Logic.Create(contractType, contractScope);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Сохранение договора
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeModel")]
        public async Task<IActionResult> ChangeModel([FromBody] ContractUpdateModel value)
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

        /// <summary>
        /// Поиск договора по ИИН
        /// </summary>
        /// <param name="iin">ИИН</param>
        /// <param name="className">Наименованиеи класса</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchDeclarantResident")]
        public async Task<IActionResult> SearchDeclarantResident(string iin)
        {
            try
            {
                var result = await Logic.SearchDeclarantResident(iin);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Поиск договора по Странам
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchDeclarantNonResident")]
        public async Task<IActionResult> SearchDeclarantNonResident(int countryId)
        {
            try
            {
                var result = await Logic.SearchDeclarantNonResident(countryId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Добавить нового заявителя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AddDeclarant")]
        public async Task<IActionResult> AddDeclarant(int contractId, string code)
        {
            try
            {
                var result = await Logic.AddDeclarant(contractId, code);
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
        public async Task<IActionResult> GetDeclarantById(int id)
        {
            try
            {
                var result = await Logic.GetDeclarantById(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Сохранение прайс листа по договору
        /// </summary>
        /// <param name="costWorkModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("SaveCostWork")]
        public async Task<IActionResult> SaveCostWork([FromBody] CostWorkModel[] costWorkModel)
        {
            try
            {
                var result = await Logic.SaveCostWork(costWorkModel);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Удаление данных по прайслисту с договора
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteCostWork")]
        public async Task<IActionResult> DeleteCostWork([FromQuery] int contractId)
        {
            try
            {
                await Logic.DeleteCostWork(contractId);
                return Json(Ok());
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
        [Route("GetListContracts")]
        public async Task<IActionResult> GetListContracts([FromQuery] [Required] int userId)
        {
            try
            {
                //await Logic.GetListContracts();//userId
                return Json(Ok());
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}