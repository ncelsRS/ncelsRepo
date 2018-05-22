using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Teme.Shared.Data.Primitives.Icon;

namespace Teme.Shared.Data.Context
{
    public class Icon : BaseEntity
    {
        //public Icon()
        //{
        //    IconRecords = new HashSet<IconRecord>();
        //}
        /// <summary>
        /// Тип модуля
        /// </summary>
        public ModuleTypeEnum ModuleType { get; set; }
        /// <summary>
        /// Ид объекта 
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Признак ошибки
        /// </summary>
        public bool isError { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool isDeleted { get; set; } = false;
        [MaxLength(500)]
        /// <summary>
        /// Наименование поля
        /// </summary>
        public string FieldName { get; set; }

        public virtual ICollection<IconRecord> IconRecords { get; set; }
    }
}
