using TShopSolution.ViewModels.Common;

namespace TShopSolution.ViewModels.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public string LanguageId { get; set; }
    }
}