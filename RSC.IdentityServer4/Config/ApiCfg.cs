using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSC.IdentityServer4.Config
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
