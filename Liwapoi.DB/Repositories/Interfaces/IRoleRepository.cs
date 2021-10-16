using Liwapoi.Models.DTO;
using System.Threading.Tasks;

namespace Liwapoi.DB.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleDTO> GetUserRole(long userId);
    }
}
