using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.IUser;

namespace RSC.IdentityServer4.IdentityConfig
{
    
    [DataContract(Name="MyResource")]
    public sealed class MyResourceType
    {
        [DataMember]
        private string Role { get; set; }

        // Constructors
        public MyResourceType()
        {
        }

        public MyResourceType(string Role)
        {
            this.Role = Role;
        }
    }  
    
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
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

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                new Claim(JwtClaimTypes.Name, user.UserName),
//                new Claim(JwtClaimTypes.Role, )
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}