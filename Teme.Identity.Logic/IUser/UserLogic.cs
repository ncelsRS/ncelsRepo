using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Teme.Identity.Data.Repos.IUser;
using Teme.Identity.Logic.Dtos;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic.IUser;

namespace Teme.Identity.Logic.IUser
{
    public class UserLogic : BaseUserLogic<IUserRepo>, IUserLogic
    {
        private readonly IIdentityLogic _identityLogic;

        public UserLogic(IUserRepo repo, IIdentityLogic identityLogic) : base(repo)
        {
            _identityLogic = identityLogic;
        }

        public async Task<OutLoginDto> Login(InLoginDto dto)
        {
            var user = await Repo.GetLogin(x => x.UserName == dto.UserName);
            if (user == null) throw new ArgumentException("Login not found");

            var alg = SHA512.Create();
            alg.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            var pwdhash = Convert.ToBase64String(alg.Hash);
            if (user.Pwdhash != pwdhash) throw new ArgumentException("Wrong password");

            return new OutLoginDto
            {
                AccessToken = _identityLogic.GenerateToken(user),
                RefreshToken = _identityLogic.GenerateUpdateToken(user.Id)
            };
        }

        public async Task<OutLoginDto> UpdateToken(string refreshToken)
        {
            var userId = _identityLogic.RealiseUpdateToken(refreshToken);
            var user = await Repo.GetLogin(x => x.Id == userId);
            if (user == null) throw new ArgumentException("Refresh not found");
            return new OutLoginDto
            {
                AccessToken = _identityLogic.GenerateToken(user),
                RefreshToken = _identityLogic.GenerateUpdateToken(user.Id)
            };
        }
    }
}