using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Repos.PaymentRepo;
using Context = Teme.Shared.Data.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        /// <summary>
        /// Автосохранение платежа
        /// </summary>
        /// <param name="data">Type</param>
        /// <param name="fieldName">наименование поля</param>
        /// <param name="value">значение</param>
        /// <param name="id">id платежа</param>
        /// <returns></returns>
        public async Task UpdateContractSql(Type data, string fieldName, object value, int id)
        {
            await Context.Database.ExecuteSqlCommandAsync(string.Format(@"UPDATE {0} SET {1} = '{2}' WHERE Id = {3}", data.Name + "s", fieldName, value, id));
        }

        /// <summary>
        /// Получение списка заявок на платеж
        /// </summary>
        /// <param name="contractId">Id Договора</param>
        /// <returns></returns>
        public async Task<IQueryable<Context.Payment>> GetListPayments(int contractId)
        {
            return await Task.Run(() => Context.Payments.Where(e => !e.isDeleted && e.ContractId == contractId));
        }

        public async Task<Context.Payment> GetPayment(int id)
        {
            return await Context.Payments.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
