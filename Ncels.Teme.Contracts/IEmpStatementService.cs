using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncels.Teme.Contracts.ViewModels;

namespace Ncels.Teme.Contracts
{
    public interface IEmpStatementService
    {
        IEnumerable<EmpStatementViewModel> GetStatements();
    }
}
