using AutoMapper;
using Domain.Entity.Default;
using Domain.Entity.Initial;
using IApplication;
using IApplication.Dtos.Default;
using IApplication.Dtos.Initial;
using IRepository;
using IRepository.Model.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UserAppService : ApplicationBase, IUserAppService
    {
        private IUserRepository<User> _userRepository;
        private IProductRepository<Product> _productRepository;
        private IMapper _mapper;

        public UserAppService(IUserRepository<User> userRepository,
            IProductRepository<Product> productRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<List<UserInfo>> GetAll()
        {
            var users = _userRepository.FindAll();
            return users;
        }

        public async Task<UserProductDto> GetUserProduct()
        {
            var user = await _userRepository.FindAsync(1);

            var products = await _productRepository.FindAll();

            

            var result = _mapper.Map<UserProductDto>(user);
            result.Product = _mapper.Map<List<ProductDto>>(products);


            return result;
        }
    }
}
