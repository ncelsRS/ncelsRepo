using System.Linq;

namespace Ncels.Teme.Contracts
{
    public interface IUnitOfWork
    {
        IQueryable<T> GetQueryable<T>() where T : class;
        void Insert<T>(T entity) where T : class;
        void Save();
    }
}
