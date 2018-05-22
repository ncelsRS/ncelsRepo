using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Common.Logic.EquipmentLogic;
using Teme.Common.Logic.PackagingLogic;
using Teme.Shared.Data.Context;
using Teme.SharedApi.Controllers;

namespace Teme.Common.Api.Controllers
{
    public class EquipmentController : BaseRefCrudController<PaymentEquipment>
    {
        public EquipmentController(IEquipmentLogic<PaymentEquipment> logic) : base(logic) { }
    }
}
