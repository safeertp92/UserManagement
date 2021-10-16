using Liwapoi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liwapoi.DB.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTO> GetByName(string name);
        Task<UserDTO> GetByEmail(string email);
        Task<UserDTO> AddNewUser(UserDTO user);
        Task<int> GetItemsCount();
        Task<List<UserDTO>> GetPage(int pageNumber, int pageSize);
    }
}
