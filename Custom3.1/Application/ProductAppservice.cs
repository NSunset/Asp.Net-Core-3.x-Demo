using AutoMapper;
using Domain.Entity.Initial;
using IApplication;
using IApplication.Dtos.Initial;
using IRepository;
using IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ProductAppservice : ApplicationBase, IProductAppService
    {
        private IProductRepository<Product> _productrePository;

        private IMapper _mapper;

        public ProductAppservice(IProductRepository<Product> productrePository,
            IMapper mapper)
        {
            _productrePository = productrePository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> FindAll()
        {
            var products = await _productrePository.FindAll();

            //List<ProductDto> result = new List<ProductDto>();
            //foreach (var item in products)
            //{
            //    result.Add(new ProductDto
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Price = item.Price
            //    });
            //}
            var result = _mapper.Map<List<ProductModel>, List<ProductDto>>(products);
            return result;
        }
    }
}
