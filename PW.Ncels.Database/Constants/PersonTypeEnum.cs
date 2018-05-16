using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.Constants
{
    public enum PersonTypeEnum
    {
        /// <summary>
        /// Физическое лицо
        /// </summary>
        NaturalPerson = 0,
 
        /// <summary>
        /// Юридическое лицо
        /// </summary>
        LegalPerson = 1,

        /// <summary>
        /// Не резидент РК
        /// </summary>
        ForeignLegalPerson = 2,

        /// <summary>
        /// Не резидент РК
        /// </summary>
        ForeignNaturalPerson = 3
    }
}
