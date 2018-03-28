using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class ClientCfg
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Обк внешний портал",
                    ClientId = "obkExt",
                    ClientSecrets = {
                        new Secret("770b892d2fff179291cbb2d879d5dd81fcd96768f64c2a96b609a5d3607f75c6".Sha256())
                    },

                    Enabled = true,

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 60,
                    AllowAccessTokensViaBrowser = true,

                    RefreshTokenUsage = TokenUsage.ReUse,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new [] { "obk" },

                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = new [] { "*" },

                    RedirectUris = new []
                    {
                        "*"
                    }
                },
                new Client
                {
                    ClientName = "IdentityServer",
                    ClientId = "identity",
                    ClientSecrets = {
                        new Secret("5aff96a2179fec9ec2b30441b15a59b8995649eee4cc4b4a957efbd33545951f".Sha256())
                    },

                    Enabled = true,

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 60,
                    AllowAccessTokensViaBrowser = true,

                    RefreshTokenUsage = TokenUsage.ReUse,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new [] { "identity" },

                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = new [] { "*" },

                    RedirectUris = new []
                    {
                        "*"
                    }
                }
            };
        }
    }
}
