using IdentityServer4.Models;
using System.Collections.Generic;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class MyIdentityResources
    {
        public static List<IdentityResource> Get()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile() // <-- usefull
        };
        }
    }
}