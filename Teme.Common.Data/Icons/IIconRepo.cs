using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Common.Data.InDto;
using Teme.Common.Data.OutDto;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.IconRepo;

namespace Teme.Common.Data.Icons
{
    public interface IIconRepo : IBaseIconRepo
    {
        Task<Icon> GetIcon(IconGetInDto cm);
        Task<IconOutDto> GetIconOutDto(IconGetInDto cm);
        Task CreateIcon(Icon icon);
        Task UpdateIcon(Icon icon);        
        Task CreateIconRecord(IconRecord iconRecord);
    }
}
