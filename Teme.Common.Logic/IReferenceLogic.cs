using System.Threading.Tasks;
using Teme.Shared.Logic;

namespace Teme.Common.Logic
{
    public interface IReferenceLogic : IEntityLogic
    {
        Task<object> GetGosRegistry(string name, string culture, int page, int counter);
    }
}
