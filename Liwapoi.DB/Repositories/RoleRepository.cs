using AutoMapper;
using Liwapoi.DB.Models;
using Liwapoi.DB.Repositories.Interfaces;
using Liwapoi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Liwapoi.DB.Repositories
{
    public class RoleRepository :BaseRepository, IRoleRepository
    {
        public RoleRepository(Liwapoi_WarnAppContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
        #region IRoleRepository
        public async Task<RoleDTO> GetUserRole(long userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException(nameof(userId));
            }

            var dbRole = await _context.Roles
                .Where(r => r.UserRoles.Any(ur => ur.UserId == userId))
                .FirstOrDefaultAsync();

            var role = _mapper.Map<RoleDTO>(dbRole);
            return role;
        }
        #endregion
    }
}
