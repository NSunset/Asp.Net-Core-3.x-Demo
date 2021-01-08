using AutoMapper;
using Custom.Domain.Entity.Initial;
using Custom.IApplication;
using Custom.IApplication.Dtos.Initial;
using Custom.IRepository;
using Custom.IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Application
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

        public async Task<List<ProductDto>> FindAllAsync()
        {
            var products = await _productrePository.FindAllAsync();
            var result = _mapper.Map<List<ProductModel>, List<ProductDto>>(products);
            return result;
        }
    }
}
