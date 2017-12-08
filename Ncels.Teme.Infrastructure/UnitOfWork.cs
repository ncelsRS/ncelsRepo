﻿using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.DataModel;

namespace Ncels.Teme.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ncelsEntities _ctx;

        public UnitOfWork()
        {
            _ctx = new ncelsEntities();
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return _ctx.Set<T>().AsQueryable();
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}