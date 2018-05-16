using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Interfaces;

namespace PW.Ncels.Database.DataModel
{

    public partial class TmcOutView : ITmcRequest
    {
        public string Comment { get; set; }
        public Guid TmcOutId { get; set; }

    }
}
