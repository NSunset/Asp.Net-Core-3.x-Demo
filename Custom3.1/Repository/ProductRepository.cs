using Domain.Entity.Initial;
using IRepository;
using IRepository.Model.Initial;
using Microsoft.EntityFrameworkCore;
using ORM.EntityFrameworkCore.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product, InitialDbContext>, IProductRepository<Product>
    {
        private IMapper _mapper;
        public ProductRepository(InitialDbContext dbContext,
            IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public Task<List<ProductModel>> FindAll()
        {
            //var query = from p in _context.Product.AsNoTracking()
            //            select new ProductModel
            //            {
            //                Id = p.Id,
            //                Name = p.Name,
            //                Price = p.Price
            //            };
            var query = from p in _context.Product.AsNoTracking()
                        select p;

            var result = query.ProjectTo<ProductModel>(_mapper.ConfigurationProvider);
            return result.ToListAsync();
        }
    }
}
