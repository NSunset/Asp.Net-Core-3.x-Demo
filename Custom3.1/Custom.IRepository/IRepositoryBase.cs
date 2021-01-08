using Custom.lib.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.IRepository
{
    public interface IRepositoryBase<T> where T : AggregateRoot
    {
        T Find(params object[] keyValues);

        Task<T> FindAsync(params object[] keyValues);

        bool Update(T t);
    }
}
