using IApplication.Dtos.Initial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplication
{
    public interface IProductAppService : IApplicationBase
    {
        Task<List<ProductDto>> FindAll();
    }
}
