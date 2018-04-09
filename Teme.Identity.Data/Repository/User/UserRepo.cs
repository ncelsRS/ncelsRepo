using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Identity.Data.Repository.User
{
    public class UserRepo : IUserRepo, IDisposable
    {
        private TemeContext _context;
        public UserRepo()
        {
            this._context = new TemeContext();
        }
        public async void AddUser(long id)
        {
            throw new NotImplementedException();
        }

        //public void Dispose()
        //{
        //    if (_context != null)
        //        _context.Dispose();
        //}

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
