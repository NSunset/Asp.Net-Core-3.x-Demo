using Common.Domain;
using IRepository.Model.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUserRepository<T> : IRepositoryBase<T> where T : AggregateRoot
    {
        Task<List<UserInfo>> FindAll();
    }
}
