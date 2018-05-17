using System;
using System.Linq;
using System.Threading.Tasks;
using Teme.Payment.Data.DTO;
using Teme.Shared.Data.Repos.PaymentRepo;

namespace Teme.Payment.Data
{
    public interface IPaymentRepo : IBasePaymentRepo
    {
        Task CreatePayment(Shared.Data.Context.Payment payment);
        Task UpdateContractSql(Type data, string fieldName, object value, int id);
        Task<IQueryable<Shared.Data.Context.Payment>> GetListPayments(int contractId);
        Task<PaymentDto> GetPayment(int id);
    }
}
