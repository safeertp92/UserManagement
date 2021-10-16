using Liwapoi.Models.BLL;
using System.Threading.Tasks;

namespace Liwapoi.BLL.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenModel> CreateToken(string userName, long userId);
    }
}
