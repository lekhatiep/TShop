using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Auth;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Auth
{
    public interface ITokenRefresh
    {
        Task<ApiResult<AuthenticateResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    }
}