using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Models.Expertise;

namespace PW.Ncels.Database.DataModel
{
    /// <summary>
    /// Заявление на ЛС
    /// </summary>
    public partial class PriceProjectCom
    {
        public List<PriceProjectFieldHistory> PriceProjectFieldHistories { get; set; }
    }
}
