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

        /// <summary>
        /// Данные ИМН/МТ
        /// </summary>
        public MedicalDeviceData MedicalDeviceData { get; set; }

        /// <summary>
        /// Производитель для заявки на платеж и заявления(не должен быть пустой если договор один к одному)
        /// </summary>
        public MedicalDeviceManufacturer MedicalDeviceManufacturer { get; set; }
    }
}
