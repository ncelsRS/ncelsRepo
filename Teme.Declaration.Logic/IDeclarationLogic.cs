using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Declaration.Data.Model;
using Teme.Shared.Logic.DeclarationLogic;

namespace Teme.Declaration.Logic
{
    public interface IDeclarationLogic : IBaseDeclarationLogic
    {
        Task<object> ChangeModel(DeclarationUpdateModel value);
    }
}
