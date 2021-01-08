using Custom.lib.Domain;
using Custom.IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.IRepository
{
    public interface IProductRepository<T> : IRepositoryBase<T> where T : AggregateRoot
    {

        Task<List<ProductModel>> FindAllAsync();
    }
}
