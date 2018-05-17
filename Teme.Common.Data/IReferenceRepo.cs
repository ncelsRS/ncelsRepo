using System.Threading.Tasks;
using Teme.Shared.Data.Repos;

namespace Teme.Common.Data
{
    public interface IReferenceRepo : IEntityRepo
    {
        Task<object> GetGosRegistry(string name, string culture, int page, int counter);
    }
}
