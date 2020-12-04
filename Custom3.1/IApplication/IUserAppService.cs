using IApplication.Dtos.Default;
using IRepository.Model.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IApplication
{
    public interface IUserAppService : IApplicationBase
    {
        Task<List<UserInfo>> GetAll();

        Task<UserProductDto> GetUserProduct();
    }
}
