using AutoMapper;
using Custom.Domain.Entity.Default;
using Custom.Domain.Entity.Initial;
using Custom.IRepository.Model.Default;
using Custom.IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Repository.AutoMappConfig
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
