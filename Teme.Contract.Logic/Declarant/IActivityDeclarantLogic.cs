using System.Threading.Tasks;

namespace Teme.Contract.Logic.Declarant
{
    public interface IActivityDeclarantLogic
    {
        Task<string> Create();
        Task<object> SendToNcels(string contractId); 
    }
}