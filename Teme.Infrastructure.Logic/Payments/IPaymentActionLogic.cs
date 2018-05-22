using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Logic;

namespace Teme.Infrastructure.Logic.Payments
{
    public interface IPaymentActionLogic : IBaseLogic
    {
        Task<object> CreatePayment();
        Task<object> PublishUserAction(string userPromt, string userOption, object value, string workflowId, IEnumerable<string> executors = null, Dictionary<string, bool> agreements = null);
    }
}
