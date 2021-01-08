using AutoMapper;
using Custom.Domain.Entity.Default;
using Custom.IApplication.Dtos.Default;
using Custom.IApplication.Dtos.Initial;
using Custom.IRepository.Model.Initial;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.IApplication
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
