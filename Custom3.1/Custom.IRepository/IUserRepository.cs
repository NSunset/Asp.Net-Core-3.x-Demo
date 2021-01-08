using Custom.lib.Domain;
using Custom.IRepository.Model.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.IRepository
{
    public interface IUserRepository<T> : IRepositoryBase<T> where T : AggregateRoot
    {
        Task<List<UserInfo>> FindAllAsync();

        Task<T> GetAsync(int id);
    }
}
