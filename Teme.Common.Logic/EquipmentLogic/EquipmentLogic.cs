using System;
using System.Collections.Generic;
using System.Text;
using Teme.Common.Data.EquipmentRepo;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic.RefBaseLogic;

namespace Teme.Common.Logic.EquipmentLogic
{
    class EquipmentLogic : RefBaseLogic<IEquipmentRepo<PaymentEquipment>, PaymentEquipment>, IEquipmentLogic<PaymentEquipment>
    {
        public EquipmentLogic(IEquipmentRepo<PaymentEquipment> repo) : base(repo) { }
    }
}
