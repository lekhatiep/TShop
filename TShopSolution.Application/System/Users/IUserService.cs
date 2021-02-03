using System;
using System.Threading.Tasks;
using TShopSolution.Data.Entities;
using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Auth;
using TShopSolution.ViewModels.System.Users;

namespace TShopSolution.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<AuthenticateResponse>> Authenticate(LoginRequest request);

        Task<ApiResult<AuthenticateResponse>> Authenticate(AppUser user);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(Guid Id, UserUpdateRequest request);

        Task<ApiResult<PagedResult<UserVM>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVM>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid Id, RoleAssignRequest request);
    }
}