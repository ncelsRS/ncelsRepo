using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DIC_PrimaryOTD
    {
        public const string Module1 = "МОДУЛЬ 1.";
        public const string Module2 = "МОДУЛЬ 2.";
        public const string Module3 = "МОДУЛЬ 3. ";
        public const string Module4 = "МОДУЛЬ 4.";
        public const string Module5 = "МОДУЛЬ 5.";
        public string FullName => "[" + Code + "]-" + NameRu;

        public string ParentFullName;
        //=> EXP_DIC_PrimaryOTD2 != null ? EXP_DIC_PrimaryOTD2.FullName : "-";

        public string WindowTitle;
    }

}
