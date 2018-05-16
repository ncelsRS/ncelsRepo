using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;

namespace Teme.Shared.Logic
{
    public class EntityLogic : BaseLogic<IEntityRepo, BaseEntity>
    {
        public EntityLogic(IEntityRepo repo) : base(repo)
        {
        }
    }
}
