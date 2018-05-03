using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic.PaymentLogic;

namespace Teme.Payment.Logic
{
    public interface IPaymentLogic : IBasePaymentLogic
    {
        Task<object> Create(int contractId);
    }
}
