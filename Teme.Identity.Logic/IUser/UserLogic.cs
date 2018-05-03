using System;
using System.Linq;
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

        public async Task<object> Login(InLoginDto dto)
        {
            var user = await Repo.GetLogin(x => x.UserName == dto.UserName);
            if (user == null) throw new ArgumentException("Login not found");

            var alg = SHA512.Create();
            alg.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            var pwdhash = Convert.ToBase64String(alg.Hash);
            if (user.Pwdhash != pwdhash) throw new ArgumentException("Wrong password");

            return new
            {
                OneTimeToken = _identityLogic.GenerateOneTimeToken(user.Id)
            };
        }

        public async Task<object> UpdateToken(int userId)
        {
            var user = await Repo.GetLogin(x => x.Id == userId);
            if (user == null) throw new ArgumentException("User not found");

            var audiences = user.Scopes.Select(x => x.Scope);

            return new
            {
                AccessToken = _identityLogic.GenerateAccessToken(user.Id, audiences),
                RefreshToken = _identityLogic.GenerateRefreshToken(user.Id)
            };
        }

        public async Task<object> Test(int userId)
        {
            return await Task.FromResult(new { CurrentUserId = userId });
        }
    }
}