using System.Data.Entity;
using System.Security.Claims;
using System.Web.Security;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services.InMemory;
using PW.Ncels.Database.DataModel;
using Task = System.Threading.Tasks.Task;

namespace RSC.IdentityServer3
{
    public class LocalAuthenticationService : UserServiceBase
    {
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            if (Membership.ValidateUser(context.UserName, context.Password))
                return;

            using (var ctx = new ncelsEntities())
            {
                var user = await ctx.aspnet_Users.FirstOrDefaultAsync(x => x.UserName == context.UserName);
                if (user == null) return;

                var authenticateResult = GetAuthenticateResult(new InMemoryUser
                {
                    Username = user.UserName,
                    Subject = user.UserName,
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, user.UserName)
                    }
                });

                context.AuthenticateResult = authenticateResult;
            }
        }

        private static AuthenticateResult GetAuthenticateResult(InMemoryUser user)
        {
            var authenticateResult = new AuthenticateResult(
                user.Subject,
                user.Username,
                user.Claims,
                Constants.BuiltInIdentityProvider,
                Constants.AuthenticationMethods.Password);

            return authenticateResult;
        }
    }
}