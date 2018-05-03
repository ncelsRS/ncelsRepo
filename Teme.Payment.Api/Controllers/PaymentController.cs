using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Payment.Data;
using Teme.Payment.Logic;
using Teme.SharedApi.Controllers;

namespace Teme.Payment.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Payment")]
    public class PaymentController //: BaseController <IPaymentLogic>
    {
        private readonly IPaymentRepo _repo;
    }
}
