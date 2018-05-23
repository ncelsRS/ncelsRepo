using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Payment.Data.InDto;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic.PaymentLogic;

namespace Teme.Payment.Logic
{
    public interface IPaymentLogic : IBasePaymentLogic
    {
        Task<object> Create(PaymentCreateModel createModel);
        Task<object> ChangeModel(PaymentUpdateModel value);
        Task<object> GetListPayments(int contractId);
        Task<object> GetPaymentById(int paymentId);
    }
}
