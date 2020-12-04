using Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IRepositoryBase<T> where T : AggregateRoot
    {
        Task<T> FindAsync(params object[] keyValues);
    }
}
