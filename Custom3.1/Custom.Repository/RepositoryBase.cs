using Custom.lib.Domain;
using Custom.lib.IOC;
using Custom.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Repository
{
    public abstract class RepositoryBase<T, Context> : IRepositoryBase<T>, IScope where T : AggregateRoot where Context : DbContext
    {
        protected readonly Context _context;

        protected RepositoryBase(Context context)
        {
            _context = context;
        }

        public T Find(params object[] keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        public Task<T> FindAsync(params object[] keyValues)
        {
            return _context.Set<T>().FindAsync(keyValues).AsTask();
        }

        public bool Update(T data)
        {
            _context.Set<T>().Update(data);
            return _context.SaveChanges() > 0;
        }
    }
}
