using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Repository;

namespace PW.Ncels.Database.DataModel
{
    /// <summary>
    /// ОБК договора
    /// </summary>
    public partial class OBK_Contract : IEntity
    {
        /// <summary>
        /// Описание договора
        /// </summary>
        public string ContractInfo { get; set; }

        public int ObkRsProductCount { get; set; }

    }
}
