using System.Threading.Tasks;

namespace Teme.Contract.Logic.Declarant
{
    public interface IActivityDeclarantLogic
    {
        Task<object> Create();
        Task<object> SendToNcels(string contractId); 
    }
}