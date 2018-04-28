using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.IUser;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class ExtensionGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "rsc_password";

        private readonly ITokenValidator _tokenValidator;

        public ExtensionGrantValidator(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var user = new AuthUser // TODO delete as soon as possible and implement norm process
            {
                Id = 123456,
                UserName = "user",
                Scopes = new[] {IdentityScopeEnum.TemeExt},
                CompanyName = "companyName",
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now,
                Email = "email",
                FirstName = "firstName",
                HasIin = false,
                LastName = "lastName",
                UserType = "userType",
                MiddleName = "middleName",
                Pwdhash = "123456"
            };

            if (context.Request.UserName != user.UserName)
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            var now = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.IdentityProvider, "local"),
                new Claim(JwtClaimTypes.AuthenticationMethod, "custom"),
                new Claim(JwtClaimTypes.AuthenticationTime, now.ToString()),
                new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                new Claim(JwtClaimTypes.Name, user.UserName),
                new Claim(JwtClaimTypes.Scope, user.Scopes.First())
            };

            var identityClaim = new ClaimsIdentity(claims, "RscAuthType");

            var principal = new ClaimsPrincipal(new List<ClaimsIdentity>(new[] {identityClaim}));

            var rawResult = new Dictionary<string, object>
            {
                {"field1", "value1"},
                {"field2", "value2"}
            };

            context.Result = new GrantValidationResult(user.Id.ToString(), "custom", claims);
        }
    }
}