using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace RSC.IdentityServer3
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OfflineAccess,
                new Scope{Name="api"}
            };
        }
    }
}