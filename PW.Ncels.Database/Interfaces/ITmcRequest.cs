using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Interfaces
{
    public interface ITmcRequest
    {
        Guid Id { get; set; }
        Guid CreatedEmployeeId { get; set; }
        Nullable<System.Guid> OwnerEmployeeId { get; set; }
    }
}
