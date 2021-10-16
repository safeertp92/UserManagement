using AutoMapper;
using Liwapoi.BLL.Services.Interfaces;
using Liwapoi.Common;
using Liwapoi.DB.Repositories.Interfaces;
using Liwapoi.Models.BLL;
using Liwapoi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Liwapoi.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        #region Private fields
        private readonly PasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        #endregion

        #region Ctor
        private UserService(IMapper mapper)
            : base(mapper)
        {
            _passwordHasher = new PasswordHasher();

        }
        public UserService(IUserRepository userRepository,
            IMapper mapper,
            IRoleService roleService)
            : this(mapper)
        {
            _userRepository = userRepository;
            _roleService = roleService;
        }

        #endregion

        #region IUserService
        public async Task<UserModel> Get(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            var userDto = await _userRepository.GetByName(userName);
            return _mapper.Map<UserModel>(userDto);
        }

        public async Task<bool> IsValid(LoginModel user)
        {

            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException(nameof(user));
            }

            var currentUser = await _userRepository.GetByName(user.Name);

            if (currentUser != null && _passwordHasher.VerifyHashedPassword(currentUser.Password, user.Password) ==
                PasswordVerificationResult.Success)
            {
                return true;
            }

            return false;
        }

        public async Task<UserModel> AddNewUser(UserModel user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException(nameof(user));
            }
            user.Password = _passwordHasher.HashPassword(user.Password);
            var userDTO = _mapper.Map<UserDTO>(user);
            var returnData = await _userRepository.AddNewUser(userDTO);
            return _mapper.Map<UserModel>(returnData);
        }
        public async Task<bool> IsDuplicated(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            var currentUserByName = await _userRepository.GetByName(user.Name);
            var currentUserByEmail = await _userRepository.GetByEmail(user.Email);

            return currentUserByName != null || currentUserByEmail != null;
        }

        public async Task<PageModel<List<UserModel>>> GetPage(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException(nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException(nameof(pageSize));
            }

            

            var data = await _userRepository.GetPage(pageNumber, pageSize);

            var totalItemsCount = await _userRepository.GetItemsCount();
            var totalPages = (double)totalItemsCount / pageSize;

            var page = new PageModel<List<UserModel>>
            {
                Data = _mapper.Map<List<UserModel>>(data),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalItemsCount,
                TotalPages = Convert.ToInt32(Math.Ceiling(totalPages))
            };

            return page;
        }
        #endregion
    }
}
