using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration.Interface
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}