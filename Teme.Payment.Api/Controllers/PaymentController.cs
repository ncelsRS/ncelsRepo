using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Payment.Data;
using Teme.Payment.Data.Model;
using Teme.Payment.Logic;
using Teme.SharedApi.Controllers;

namespace Teme.Payment.Api.Controllers
{
    public class PaymentController : BaseController <IPaymentLogic>
    {
        public PaymentController(IPaymentLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Созадение договора
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody][Required]PaymentCreateModel createModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.Create(createModel);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Сохранение платежа
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeModel")]
        public async Task<IActionResult> ChangeModel([FromBody] PaymentUpdateModel value)
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
        /// Получение списка заявок на платеж
        /// </summary>
        /// <param name="contractId">Id Договора</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListPayments")]
        public async Task<IActionResult> GetListPayments([FromQuery] [Required] int contractId)
        {
            try
            {
                var result = await Logic.GetListPayments(contractId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        [HttpGet]
        [Route("GetPaymentById")]
        public async Task<IActionResult> GetContractById(int paymentId)
        {
            try
            {
                var result = await Logic.GetPaymentById(paymentId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                var res = "test";
                return Json(res);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }


}
