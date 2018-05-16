using System.Collections.Generic;
using System.Linq;
using Teme.Shared.Data.Context;

namespace Teme.Common.Data.OutDto
{
    /// <summary>
    /// 
    /// </summary>
    public class ConvertOutDtoRepo : IConvertOutDtoRepo
    {
        public IEnumerable<IconRecordOutDto> ConvertEntityToIconRecord(ICollection<IconRecord> irs)
        {
            return irs.Select(ir => new IconRecordOutDto{
                id = ir.Id,
                UserName = ir.AuthUser.FirstName,
                RoleName = ir.AuthRoleId.ToString(),
                ValueField = ir.ValueField,
                DisplayField = ir.DisplayField,
                Note = ir.Note,
                DateCreate = ir.DateCreate,
            });
        }

        public IconOutDto ConvertEntityToIcon(Icon i)
        {
            return new IconOutDto()
            {
                Id = i.Id,
                ModuleType = i.ModuleType,
                ObjectId = i.ObjectId,
                FieldName = i.FieldName,
                IconRecords = i?.IconRecords != null ? ConvertEntityToIconRecord(i.IconRecords) : null
            };
        }

        
    }
}
