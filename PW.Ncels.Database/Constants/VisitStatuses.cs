using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public enum VisitStatuses
    {
        [Description("Ожидает подтверждения")]
        NeedConfirm = 1,

        [Description("Запланировано")]
        Planned = 2,

        [Description("Никто не пришёл")]
        NoOneCome = 3,

        [Description("Выполнено")]
        Complete = 4,
    }
}
