using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.RefBaseRepo;

namespace Teme.Common.Data.EquipmentRepo
{
    public interface IEquipmentRepo<TEntity> : IRefBaseRepo<TEntity> where TEntity : BaseEntity
    {
    }
}
