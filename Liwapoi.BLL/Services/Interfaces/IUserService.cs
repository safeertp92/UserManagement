using Liwapoi.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liwapoi.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsValid(LoginModel user);
        Task<UserModel> Get(string name);
        Task<UserModel> AddNewUser(UserModel user);
        Task<bool> IsDuplicated(UserModel user);
        Task<PageModel<List<UserModel>>> GetPage(int pageNumber, int pageSize);
    }
}
