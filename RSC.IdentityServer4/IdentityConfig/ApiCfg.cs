using System.Collections.Generic;
using IdentityServer4.Models;

namespace RSC.IdentityServer4.IdentityConfig
{
    public class ApiCfg
    {
        public static List<ApiResource> Get()
        {
            return new List<ApiResource>
            {
                new ApiResource("obk", "ОБК")
            };
        }
    }
}
