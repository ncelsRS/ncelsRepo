using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Payment.Data;
using Teme.Payment.Data.DTO;
using Teme.Payment.Data.Model;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic.PaymentLogic;

namespace Teme.Payment.Logic
{
    public class PaymentLogic : BasePaymentLogic<IPaymentRepo>, IPaymentLogic
    {
        private readonly IPaymentRepo _repo;
        private readonly IConvertDtoRepo _dtoRepo;
        public PaymentLogic(IPaymentRepo repo, IConvertDtoRepo dtoRepo) : base(repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Создание заявка на платеж
        /// </summary>
        /// <returns></returns>
        public async Task<object> Create(PaymentCreateModel createModel)
        {
            //var workflowId = await _wflogic.Create();
            Shared.Data.Context.Payment payment = new Shared.Data.Context.Payment()
            {
                //WorkflowId = workflowId.GetType().GetProperty("workflowId").GetValue(workflowId).ToString(),
                ContractId = createModel.ContractId,
            };
            await _repo.CreatePayment(payment);
            //await _repo.SaveStatePolice(_contractSp.GetStatePolicy("DeclarantCreateContract", contract.Id), contract.Id);
            return new
            {
                payment.Id,
            };

        }

        /// <summary>
        /// Автосохранение платежа
        /// </summary>
        /// <param name="contract">модель</param>
        /// <returns></returns>
        public async Task<object> ChangeModel(PaymentUpdateModel payment)
        {
            var data = Type.GetType(payment.ClassName + ", Teme.Shared.Data");
            List<Task> tasks = new List<Task>();
            Dictionary<string, string> objectList = JsonConvert.DeserializeObject<Dictionary<string, string>>(payment.Fields.ToString());
            foreach (var o in objectList)
            {
                tasks.Add(_repo.UpdateContractSql(data, o.Key, o.Value, payment.Id));
            }
            Task.WaitAll(tasks.ToArray());
            return new { paymentId = payment.Id };
        }

        /// <summary>
        /// Получение списка заявок на платеж
        /// </summary>
        /// <param name="contractId">Id Договора</param>
        /// <returns>Возвращает списка заявок на платеж</returns>
        public async Task<object> GetListPayments(int contractId)
        {
            var result = await _repo.GetListPayments(contractId);
            return result.Select(x => new {
                x.Id,
                x.ContractForm
                //x.Number,
                //x.DateCreate,
                //x.ContractForm,
                //x.StatePolicies.FirstOrDefault(e => e.Scope == OrganizationScopeEnum.Ext).Status,
                //x.Manufactur.NameRu,
                //x.Manufactur.NameKz,
                //x.MedicalDeviceNameRu,
                //x.MedicalDeviceNameKz,
                //x.ContractScope
            });
        }

        /// <summary>
        /// Получение заявки на платеж по Id
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetPaymentById(int paymentId)
        {
            var result = await _repo.GetPayment(paymentId);
            if (result == null)
                return null;
            return _dtoRepo.ConvertEntityToPayment(result);
        }
    }
}
