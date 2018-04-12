using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Teme.Identity.Logic.Models;
using Teme.Shared.Data.Context;

namespace Teme.Identity.Logic.Repository.User
{
    public class UserRepo:IUserRepo
    {
        public void RegisterUser(Register register)
        {

            AuthUser authUser = new AuthUser()
            {
                Bin = register.Bin,
                CompanyName = register.CompanyName,
                Iin = register.Iin,
                Pwdhash = GetPasswordHash(register.Password),
                LastName = register.LastName,
                FirstName = register.FirstName,
                MiddleName = register.MiddleName,
                Email = register.Email,
                UserType = register.UserType,
                HasIin = register.HasIin
            };


    }

        private string GetPasswordHash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password", "Пароль не может быть равен null.");
            }
            var data = Encoding.UTF8.GetBytes(password);
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                byte[] sha1data = sha256.ComputeHash(data);
                var hashedPassword = System.Text.Encoding.Default.GetString(sha1data); ;
                return hashedPassword;
            }

        }



    }
}
