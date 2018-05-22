using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos
{
    public interface IBaseRepo<TEntity>
    {
        IQueryable<AuthUser> GetCurrentUser(int userId);
        Task Add(TEntity entity);
        Task<TEntity> GetById(int id);
    }
}
