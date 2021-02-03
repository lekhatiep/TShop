using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Auth;
using System.Threading.Tasks;

namespace TShopSolution.Application.System.Auth
{
    public interface ITokenRefresh
    {
        Task<ApiResult<AuthenticateResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    }
}