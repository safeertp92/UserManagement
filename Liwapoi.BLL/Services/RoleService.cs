using AutoMapper;
using Liwapoi.BLL.Services.Interfaces;
using Liwapoi.DB.Repositories.Interfaces;
using Liwapoi.Models.BLL;
using System;
using System.Threading.Tasks;

namespace Liwapoi.BLL.Services
{
    public class RoleService : BaseService, IRoleService
    {

        #region Private fields

        private readonly IRoleRepository _roleRepository;

        #endregion

        #region Ctor

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
            : base(mapper)
        {
            _roleRepository = roleRepository;
        }
        #endregion

        #region IRoleService
        public async Task<RoleModel> GetUserRole(long userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException(nameof(userId));
            }

            var role = await _roleRepository.GetUserRole(userId);

            return role == null
                ? new RoleModel { RoleName = "User" }
                : _mapper.Map<RoleModel>(role);
        }
        #endregion

    }
}
