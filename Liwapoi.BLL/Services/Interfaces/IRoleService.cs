using Liwapoi.Models.BLL;
using System.Threading.Tasks;

namespace Liwapoi.BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleModel> GetUserRole(long userId);
    }
}
