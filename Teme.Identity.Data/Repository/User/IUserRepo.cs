using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;

namespace Teme.Identity.Data.Repository.User
{
    public interface IUserRepo
    {
        void AddUser(AuthUser authUser);
    }
}
