using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace RSC.IdentityServer3
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "angular",
                    ClientId = "angular",
                    ClientSecrets = new List<Secret>{ new Secret("js".Sha256()) },

                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.ResourceOwner,

                    AllowedScopes = new List<string>{"api", StandardScopes.OfflineAccess.Name},
                    AllowAccessToAllScopes = true,

                    AllowedCorsOrigins = new List<string>{ "*" }
                }
            };
        }
    }
}