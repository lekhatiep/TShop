using TShopSolution.Application.Catolog.ProductImages;
using TShopSolution.ViewModels.Catalog.Category;
using TShopSolution.ViewModels.Catalog.ProductImages;
using TShopSolution.ViewModels.Catalog.Products;
using TShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TShopSolution.Application.Catolog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<ProductViewModel> GetById(int productId, string languageId);

        Task<int> Update(ProductUpdateRequest request);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewCount(int productId);

        Task<int> Delete(int productId);

        Task<ApiResult<PagedResult<ProductViewModel>>> GetAllPaging(GetManageProductPagingRequest request);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<List<ProductImageViewModel>> GetListImages(int productId);

        Task<ProductImageViewModel> GetImageById(int imageId);

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);

        Task<ApiResult<bool>> AssignCategory(int Id, CategoryAssignRequest request);

        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);

        Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take);
    }
}