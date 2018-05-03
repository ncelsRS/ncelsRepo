using System;
using System.Threading.Tasks;
using Teme.Payment.Data;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic.PaymentLogic;

namespace Teme.Payment.Logic
{
    public class PaymentLogic : BasePaymentLogic<IPaymentRepo>, IPaymentLogic
    {
        private readonly IPaymentRepo _repo;
        public PaymentLogic(IPaymentRepo repo) : base(repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Создание заявка на платеж
        /// </summary>
        /// <returns></returns>
        public async Task<object> Create(int contractId)
        {
            //var workflowId = await _wflogic.Create();
            Shared.Data.Context.Payment contract = new Shared.Data.Context.Payment()
            {
                //WorkflowId = workflowId.GetType().GetProperty("workflowId").GetValue(workflowId).ToString(),
                ContractId = contractId,
            };
            await _repo.CreatePayment(contract);
            //await _repo.SaveStatePolice(_contractSp.GetStatePolicy("DeclarantCreateContract", contract.Id), contract.Id);
            return new
            {
                contract.Id,
            };

        }
    }
}
