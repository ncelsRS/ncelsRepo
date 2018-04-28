using System.Threading.Tasks;
using Teme.Identity.Logic.Dtos;
using Teme.Shared.Logic.IUser;

namespace Teme.Identity.Logic.IUser
{
    public interface IUserLogic : IBaseUserLogic
    {
        Task<OutLoginDto> Login(InLoginDto dto);
    }
}