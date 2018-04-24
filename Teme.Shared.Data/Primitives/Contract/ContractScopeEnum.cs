using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.Contract
{
    /// <summary>
    /// ЕАЭС или Нац
    /// </summary>
    public static class ContractScopeEnum
    {
        /// <summary>
        /// Экспертиза ИМН и МТ по процедуре РК
        /// </summary>
        public const string national = "national";
        /// <summary>
        /// Экспертиза ИМН и МТ в рамках ЕАЭС (РГ)
        /// </summary>
        public const string eaesrg = "eaesrg";
        /// <summary>
        /// Экспертиза ИМН и МТ в рамках ЕАЭС (ГП)
        /// </summary>
        public const string eaesgp = "eaesgp";
    }
}
