using System;
using System.Threading.Tasks;
using Teme.Shared.Data.Repos.PaymentRepo;

namespace Teme.Payment.Data
{
    public interface IPaymentRepo : IBasePaymentRepo
    {
        Task CreatePayment(Shared.Data.Context.Payment payment);
    }
}
