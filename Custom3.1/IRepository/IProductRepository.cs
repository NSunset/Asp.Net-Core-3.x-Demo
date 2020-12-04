using Common.Domain;
using IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IProductRepository<T> : IRepositoryBase<T> where T : AggregateRoot
    {

        Task<List<ProductModel>> FindAll();
    }
}
