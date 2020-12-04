using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace IApplication.Dtos.Initial
{
    public class ProductDtoConvert : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime sourceMember, ResolutionContext context)
        {
            return sourceMember.ToString("yyyy-MM-dd");
        }
    }
}
