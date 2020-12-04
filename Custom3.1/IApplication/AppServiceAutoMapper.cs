using AutoMapper;
using Common.AutoMapperConfig;
using Domain.Entity.Default;
using IApplication.Dtos.Default;
using IApplication.Dtos.Initial;
using IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;

namespace IApplication
{
    public class AppServiceAutoMapper : Profile
    {
        public AppServiceAutoMapper()
        {
            this.CreateMap<ProductModel, ProductDto>()
                .ForMember(dto => dto.CreateTime, mo => mo.ConvertUsing(new ProductDtoConvert(), m => m.CreateTime))
                //.ForMember(dto=>dto.CreateTime,mo=>mo.MapFrom(m=>m.CreateTime.ToString("yyyy-MM-dd")))
                ;

            this.CreateMap<User, UserProductDto>();


        }
    }
}
