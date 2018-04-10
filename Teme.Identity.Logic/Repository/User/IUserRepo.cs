using System;
using System.Collections.Generic;
using System.Text;
using Teme.Identity.Logic.Models;

namespace Teme.Identity.Logic.Repository.User
{
    public interface IUserRepo
    {
        void RegisterUser(Register register);
    }
}
