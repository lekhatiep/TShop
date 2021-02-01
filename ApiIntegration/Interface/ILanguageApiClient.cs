using TShopSolution.ViewModels.Common;
using TShopSolution.ViewModels.System.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration.Interface
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageVM>>> GetAll();
    }
}