using Liwapoi.BLL.Services.Interfaces;
using Liwapoi.Models.BLL;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Liwapoi.BLL.Services
{
    public class TokenService : ITokenService
    {
        #region Private fields
        private readonly AuthOptions _authOptions;
        private readonly IRoleService _roleService;
        #endregion

        #region Ctor
        public TokenService(IOptions<AuthOptions> authOptionsAccessor, IRoleService roleService)
        {
            _authOptions = authOptionsAccessor.Value;
            _roleService = roleService;

        }
        #endregion

        #region ITokenService
        public async Task<TokenModel> CreateToken(string userName, long userId)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            if (userId < 0)
            {
                throw new ArgumentException(nameof(userId));
            }

            var role = await _roleService.GetUserRole(userId);

            var authClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("userId", userId.ToString()),
               //  new(ClaimTypes.Role, role.Name)
            };

            var expiresDateTime = DateTime.Now.AddMinutes(_authOptions.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                expires: expiresDateTime,
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = expiresDateTime,
                UserId = userId,
                RoleId = role.RoleId
            };
        }
        #endregion
    }
}
