using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Interface
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserVM>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);

        Task<ApiResult<bool>> UpdateUser(Guid Id, UserUpdateRequest request);

        Task<ApiResult<UserVM>> GetById(Guid Id);

        Task<ApiResult<bool>> Delete(UserDeleteRequest request);

        Task<ApiResult<bool>> RoleAssign(Guid Id, RoleAssignRequest request);
    }
}