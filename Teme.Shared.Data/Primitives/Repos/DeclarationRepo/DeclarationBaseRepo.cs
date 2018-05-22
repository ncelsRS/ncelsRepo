using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;

namespace Teme.Shared.Data.Primitives.Repos.DeclarationRepo
{
    public abstract class DeclarationBaseRepo : BaseRepo<Declaration>, IDeclarationBaseRepo
    {
        public DeclarationBaseRepo(TemeContext context) : base(context)
        {
        }
    }
}
