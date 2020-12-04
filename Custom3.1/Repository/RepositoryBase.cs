using Common.Domain;
using Common.IOC;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T, Db> : IRepositoryBase<T>, IScope where T : AggregateRoot where Db : DbContext
    {
        protected Db _context;

        protected RepositoryBase(Db context)
        {
            _context = context;
        }

        public Task<T> FindAsync(params object[] keyValues)
        {
            return _context.FindAsync<T>(keyValues).AsTask();
        }
    }
}
