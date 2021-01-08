using Custom.IApplication.Dtos.Default;
using Custom.IRepository.Model.Default;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Custom.IApplication
{
    public interface IUserAppService : IApplicationBase
    {
        Task<List<UserInfo>> GetAllAsync();

        Task<UserProductDto> GetUserProductAsync();

        Task<bool> UpdateAsync(UserInfo user);
    }
}
