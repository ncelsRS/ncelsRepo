﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.IO;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.IUser;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
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

            if (context.UserName != user.UserName || context.Password != user.Pwdhash)
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, "declarant")
            };

            context.Result = new GrantValidationResult(user.Id.ToString(), "custom", claims);
        }
    }
}