using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Common.Data.InDto;
using Teme.Shared.Logic.IconLogic;

namespace Teme.Common.Logic.Icons
{

    public interface IIconLogic : IBaseIconLogic
    {
        Task<object> CreateIconRecord(IconCreateInDto iconCreateInDto);
        Task<object> GetIconRecords(IconGetInDto iconGetInDto);
    }
}
