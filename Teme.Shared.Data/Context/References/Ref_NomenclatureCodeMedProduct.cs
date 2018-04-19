using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Код Номенклатуры медицинских изделий Республики Казахстан 
    /// </summary>
    public class Ref_NomenclatureCodeMedProduct : Reference
    {
        /// <summary>
        /// Описание на русском
        /// </summary>
        public string DefenitionNameRu { get; set; }
        /// <summary>
        /// Описание на казахском
        /// </summary>
        public string DefenitionNameKz { get; set; }
    }
}
