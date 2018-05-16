using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class OBK_Ref_Reason
    {
        public string Name => NameRu + "/" + NameKz;
    }
}
