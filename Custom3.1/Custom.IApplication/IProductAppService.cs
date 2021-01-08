using Custom.IApplication.Dtos.Initial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.IApplication
{
    public interface IProductAppService : IApplicationBase
    {
        Task<List<ProductDto>> FindAllAsync();
    }
}
