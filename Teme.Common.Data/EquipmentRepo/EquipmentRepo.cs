using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.RefBaseRepo;

namespace Teme.Common.Data.EquipmentRepo
{
    public class EquipmentRepo : RefBaseRepo<PaymentEquipment>, IEquipmentRepo<PaymentEquipment>
    {
        public EquipmentRepo(TemeContext context) : base(context)
        {
        }
    }
}
