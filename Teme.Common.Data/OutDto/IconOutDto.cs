using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Icon;

namespace Teme.Common.Data.OutDto
{
    public class IconOutDto
    {
        //public IconOutDto(Icon i)
        //{
        //    Id = i.Id;
        //    ModuleType = i.ModuleType;
        //    ObjectId = i.ObjectId;
        //    FieldName = i.FieldName;
        //    IconRecordDtos = i?.IconRecords != null ? i.IconRecords.Select(ir => new IconRecordOutDto(ir)) : null;
        //    //IconRecordDtos = i?.IconRecords != null ? ConvertEntityToIconRecord(i.IconRecords) : null;
        //}
        public int Id { get; set; }
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
        /// Наименование поля
        /// </summary>
        public string FieldName { get; set; }

        public IEnumerable<IconRecordOutDto> IconRecords { get; set; }

    }
}
