using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Enums{

    public enum ProjectType{

        /// <summary>
        /// Входящий документ
        /// </summary>
        Incoming = 0,

        /// <summary>
        /// Исходящий документ
        /// </summary>
        Outgoing = 1,

        /// <summary>
        /// Обращение граждан
        /// </summary>
        Citizen = 2,

        /// <summary>
        /// ОРД
        /// </summary>
        Administrative = 3
    }
}
