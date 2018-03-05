using IdentityServer4.Test;
using System.Collections.Generic;

namespace RSC.IdentityServer4.Config
{
    public class UserCfg
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "user",
                    Password = "123456"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "user1",
                    Password = "123456"
                }
            };
        }
    }
}
