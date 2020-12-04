using AutoMapper;
using Domain.Entity.Default;
using Domain.Entity.Initial;
using IRepository.Model.Default;
using IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.AutoMappConfig
{
    public class DomainAutoMapper : Profile
    {
        public DomainAutoMapper()
        {
            this.CreateMap<Product, ProductModel>();
            this.CreateMap<User,UserInfo>();
        }

    }
}
