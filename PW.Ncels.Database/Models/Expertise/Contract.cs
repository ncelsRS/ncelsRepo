using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Repository;

namespace PW.Ncels.Database.DataModel
{
    /// <summary>
    /// Договора
    /// </summary>
    public partial class Contract : IEntity
    {
        /// <summary>
        /// Описание договора
        /// </summary>
        public string ContractInfo { get; set; }

    }
}
