using System.Threading.Tasks;

namespace Teme.Shared.Data.Repos
{
    public interface IBaseRepo<TEntity>
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(int id);
    }
}