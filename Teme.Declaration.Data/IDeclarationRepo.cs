using System; 
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Repos.DeclarationRepo;

namespace Teme.Declaration.Data
{
    public interface IDeclarationRepo : IDeclarationBaseRepo
    {
        Task UpdateDeclarationSql(Type data, string fieldName, object value, int id);
    }
}
