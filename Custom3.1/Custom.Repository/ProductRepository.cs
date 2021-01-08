using Custom.Domain.Entity.Initial;
using Custom.IRepository;
using Custom.IRepository.Model.Initial;
using Microsoft.EntityFrameworkCore;
using Custom.ORM.EntityFrameworkCore.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Custom.Repository
{
    public class ProductRepository : RepositoryBase<Product, InitialDbContext>, IProductRepository<Product>
    {
        private readonly IMapper _mapper;
        public ProductRepository(InitialDbContext dbContext,
            IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public Task<List<ProductModel>> FindAllAsync()
        {
            var query = from p in _context.Product.AsNoTracking()
                        select p;

            var result = query.ProjectTo<ProductModel>(_mapper.ConfigurationProvider);
            return result.ToListAsync();
        }
    }
}
