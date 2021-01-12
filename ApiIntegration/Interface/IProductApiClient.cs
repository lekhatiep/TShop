using eShopSolution.ViewModels.Catalog.Category;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Interface
{
    public interface IProductApiClient
    {
        Task<ApiResult<PagedResult<ProductViewModel>>> GetProductPaging(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<bool> DeleteProduct(int productId);

        Task<ProductViewModel> GetById(int Id, string languageId);

        Task<ApiResult<bool>> AssginCategory(int Id, CategoryAssignRequest request);

        Task<List<ProductViewModel>> GetFeaturedProduct(string language, int take);

        Task<List<ProductViewModel>> GetLatestProduct(string language, int take);
    }
}