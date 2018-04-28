using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using Microsoft.Extensions.Configuration;
using Teme.Shared.Data.Primitives.IUser;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class IdSrvConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var list = new List<ApiResource>();

            var props = typeof(IdentityScopeEnum).GetFields();
            props.ToList().ForEach(field =>
            {
                var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute));
                var a = attribute as DescriptionAttribute;
                var v = field.GetValue(field).ToString();
                var res = new ApiResource(v, a?.Description ?? field.Name)
                {
                    UserClaims = {JwtClaimTypes.Role}
                };
                list.Add(res);
            });
            return list;
        }

        public static IEnumerable<Client> GetClients(IConfiguration config)
        {
            var allScopes = typeof(IdentityScopeEnum).GetFields()
                .Select(x => x.GetValue(x).ToString()).ToList();
            allScopes.Add(IdentityServerConstants.StandardScopes.OpenId);
            allScopes.Add(IdentityServerConstants.StandardScopes.Profile);
            var temeExtSecret = "770b892d2fff179291cbb2d879d5dd81fcd96768f64c2a96b609a5d3607f75c6".Sha512();
            return new List<Client>
            {
                new Client
                {
                    ClientId = "RscAuthClient",
                    ClientName = "Собственный клиент аутентификации",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = new List<Secret> {new Secret(temeExtSecret)},

                    RedirectUris = {$"{config["Urls:Identity"]}/auth"},
                    PostLogoutRedirectUris = {config["Urls:Identity"]},
                    AllowedScopes = allScopes,
                    AllowOfflineAccess = true

//                    AccessTokenType = AccessTokenType.Jwt,
//                    AllowAccessTokensViaBrowser = true,
//                    UpdateAccessTokenClaimsOnRefresh = true,
//
//                    AccessTokenLifetime = (int) TimeSpan.FromHours(8).TotalSeconds,
//                    AllowOfflineAccess = true,
//                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
//                    RefreshTokenExpiration = TokenExpiration.Absolute,
//                    AbsoluteRefreshTokenLifetime = (int) TimeSpan.FromDays(30).TotalSeconds,
//
//                    AllowedCorsOrigins = {"*"},
//                    AllowedScopes = allScopes,
//
//                    RequireClientSecret = true
                }
            };
        }
    }
}