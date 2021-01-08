using AutoMapper;
using Custom.Domain.Entity.Default;
using Custom.Domain.Entity.Initial;
using Custom.IApplication;
using Custom.IApplication.Dtos.Default;
using Custom.IApplication.Dtos.Initial;
using Custom.IRepository;
using Custom.IRepository.Model.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Custom.lib.UnitofWork;
using Custom.ORM.EntityFrameworkCore.UnitofWork;
using Custom.lib.DynamicProxy;
using Castle.DynamicProxy;

namespace Custom.Application
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

        public Task<List<UserInfo>> GetAllAsync()
        {
            var users = _userRepository.FindAllAsync();
            return users;
        }

        public async Task<UserProductDto> GetUserProductAsync()
        {
            var user = await _userRepository.FindAsync(1);

            var products = await _productRepository.FindAllAsync();



            var result = _mapper.Map<UserProductDto>(user);
            result.Product = _mapper.Map<List<ProductDto>>(products);


            return result;
        }


        [UnitofWork(Transaction = true)]
        public async Task<bool> UpdateAsync(UserInfo user)
        {
            var u = await _userRepository.FindAsync(1);
            u.UserName = user.UserName;
            _userRepository.Update(u);


            u.CreateTime = user.CreateTime;
            _userRepository.Update(u);

            var product = _productRepository.Find(1);
            product.CreateTime = DateTime.Now;
            _productRepository.Update(product);

            product.Name = "李四";
            _productRepository.Update(product);


            return true;
        }
    }
}
