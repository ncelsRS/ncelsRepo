using System.Collections.Generic;
using Teme.Shared.Data.Context;

namespace Teme.Common.Data.OutDto
{
    public interface IConvertOutDtoRepo
    {
        IEnumerable<IconRecordOutDto> ConvertEntityToIconRecord(ICollection<IconRecord> irs);
        IconOutDto ConvertEntityToIcon(Icon i);
    }
}
