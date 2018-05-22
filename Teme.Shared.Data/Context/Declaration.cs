using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context.References;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Заявление
    /// </summary>
    public class Declaration : BaseEntity
    {
        /// <summary>
        /// Договор
        /// </summary>
        public Contract Contract { get; set; }

        /// <summary>
        /// Вид регистрации
        /// </summary>
        public ContractFormEnum DeclarationForm { get; set; }

        /// <summary>
        /// № письма
        /// </summary>
        public string LetterNumber { get; set; }

        /// <summary>
        /// Дата письма
        /// </summary>
        public DateTime? LetterDate { get; set; }

        /// <summary>
        /// Код Номенклатуры медицинских изделий Республики Казахстан (НМИ РК)
        /// </summary>
        public Ref_NomenclatureCodeMedProduct Nomenclature { get; set; }
    }
}
