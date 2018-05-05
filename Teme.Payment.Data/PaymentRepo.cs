using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Repos.PaymentRepo;
using Context = Teme.Shared.Data.Context;
using System.Threading.Tasks;

namespace Teme.Payment.Data
{
    public class PaymentRepo : BasePaymentRepo, IPaymentRepo
    {
        public PaymentRepo(Context.TemeContext context) : base(context)
        {
        }
        public async Task CreatePayment(Context.Payment payment)
        {
            Context.Payments.Add(payment);
            await Context.SaveChangesAsync();
        }
    }
}
