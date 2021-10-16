using AutoMapper;
using Liwapoi.DB.Models;
using Liwapoi.DB.Repositories.Interfaces;
using Liwapoi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liwapoi.DB.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        #region Ctor

        public UserRepository(Liwapoi_WarnAppContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
        #endregion

        #region IUserRepository
        public async Task<UserDTO> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            var dbUser = await _context
                .Users
                .Where(u => u.UserName.ToLower() == name.ToLower() && !u.IsDeleted)
                .FirstOrDefaultAsync();

            return dbUser == null ? null : _mapper.Map<UserDTO>(dbUser);
        }
        public async Task<UserDTO> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(nameof(email));
            }

            var dbUser = await _context
                .Users
                .Where(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted)
                .FirstOrDefaultAsync();

            return dbUser == null ? null : _mapper.Map<UserDTO>(dbUser);
        }
        public async Task<UserDTO> AddNewUser(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }
            var dbUser = _mapper.Map<User>(user);
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    dbUser.IsDeleted = false;
                    _context.Users.Add(dbUser);
                    await _context.SaveChangesAsync();


                    var newAttach = new UserRole()
                    {
                        RoleId = user.RoleId,
                        UserId = dbUser.UserId
                    };
                    _context.UserRoles.Add(newAttach);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    await _context.Roles.LoadAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }
            return _mapper.Map<UserDTO>(dbUser);
        }


        public async Task<int> GetItemsCount()
        {


            // TODO: move role checking on the top (in service)
            var userRole = await _context
                .UserRoles
                .Include(ur => ur.Role)

                .FirstOrDefaultAsync();

            if (userRole == null)
            {
                throw new Exception("Role for current user is incorrect");
            }

            // if (userRole.Role.RoleName == "Admin")
            // {
            var itemsCount = await _context
            .Users
            .Where(u => !u.IsDeleted)
            .CountAsync();

            return itemsCount;
            //  }

            // var usersForAdm = await GetUsersForAdmin(userId);

            // return usersForAdm.Count;
        }

        public async Task<List<UserDTO>> GetPage(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException(nameof(pageNumber));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException(nameof(pageSize));
            }
            // TODO: move role checking on the top (in service)
            var userRole = await _context
                .UserRoles
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync();

            if (userRole == null)
            {
                throw new Exception("Role for current user is incorrect");
            }
            var dbUsers = _context.Users
            .Where(u => !u.IsDeleted)
            .OrderBy(u => u.UserId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(u => u.UserRoles);

            await _context.Roles.LoadAsync();

            var users = _mapper.Map<List<UserDTO>>(dbUsers);

            return users;
        }
        #endregion
    }
}
