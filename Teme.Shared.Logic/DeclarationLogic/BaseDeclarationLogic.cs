using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Primitives.Repos.DeclarationRepo;

namespace Teme.Shared.Logic.DeclarationLogic
{
    public class BaseDeclarationLogic<TIRepo> : BaseLogic<TIRepo, Data.Context.Declaration>, IBaseDeclarationLogic where TIRepo : IDeclarationBaseRepo
    {
        public BaseDeclarationLogic(TIRepo repo) : base(repo)
        {
        }
    }
}
