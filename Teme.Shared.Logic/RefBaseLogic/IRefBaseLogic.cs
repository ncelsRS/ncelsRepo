using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Teme.Shared.Logic.RefBaseLogic
{
    public interface IRefBaseLogic<TEntity> : IBaseLogic
    {
        Task<int> Add(TEntity entity);
        Task Save();
        Task<TEntity> GetById(int id);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
    }
}
