using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSC.IdentityServer4.Config
{
    public class ClientCfg
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "obk",
                    ClientSecrets = { new Secret("obkSecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = { "obk" },
                    
                    AccessTokenLifetime = 60
                }
            };
        }
    }
}
