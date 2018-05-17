using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic.RefBaseLogic;
using Teme.SharedApi.Controllers;

namespace Teme.Common.Api.Controllers
{
    public class PackagingController : BaseCrudController<PaymentPackaging>
    {
        public PackagingController(IRefBaseLogic<PaymentPackaging> logic) : base(logic) { }
    }
}
