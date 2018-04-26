using System;
using Teme.Shared.Data.Context;

namespace Teme.Identity.Data.Repository.IUser
{
    public class UserRepo : IUserRepo, IDisposable
    {
        private TemeContext _context;
        public UserRepo()
        {
            this._context = new TemeContext();
        }
        public async void AddUser(AuthUser authUser)
        {
            _context.AuthUsers.Add(authUser);
            await _context.SaveChangesAsync();
        }





        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
