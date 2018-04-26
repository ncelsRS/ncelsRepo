using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using IdentityModel;
using Teme.Shared.Data.Primitives.IUser;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class IdSrvConfig
    {
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
                    UserClaims = {JwtClaimTypes.Scope},
                    Scopes = {new Scope("all")}
                };
                list.Add(res);
            });
            return list;
        }

        public static IEnumerable<Client> GetClients()
        {
            var temeExtSecret = "770b892d2fff179291cbb2d879d5dd81fcd96768f64c2a96b609a5d3607f75c6".Sha512();
            return new List<Client>
            {
                new Client
                {
                    ClientId = "temeAngular",
                    ClientName = "Веб приложение ТЭМИ",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret> {new Secret(temeExtSecret)},

                    AccessTokenType = AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    AccessTokenLifetime = (int) TimeSpan.FromHours(8).TotalSeconds,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int) TimeSpan.FromDays(30).TotalSeconds,

                    AllowedCorsOrigins = {"*"},
                    AllowedScopes = {"all"},

                    RequireClientSecret = true
                }
            };
        }
    }
}