using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.PaymentRepo
{
    public abstract class BasePaymentRepo : BaseRepo<Payment>, IBasePaymentRepo
    {
        protected BasePaymentRepo(TemeContext context) : base(context)
        {
        }
    }
}
