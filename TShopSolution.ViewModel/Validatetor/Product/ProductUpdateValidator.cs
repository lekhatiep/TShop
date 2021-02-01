using TShopSolution.ViewModels.Catalog.Products;
using FluentValidation;

namespace TShopSolution.ViewModels.Validatetor.Product
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên sản phẩm là bắt buộc").MaximumLength(200).WithMessage("Tên sản phẩm không vượt quá 200 ký tự").MinimumLength(5).WithMessage("Tên sản phẩm ít nhất 5 ký tự");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả sản phẩm là bắt buộc").MaximumLength(2000).WithMessage("Mô tả sản phẩm không vượt quá 200 ký tự").MinimumLength(5).WithMessage("Mô tả sản phẩm ít nhất 5 ký tự");
            RuleFor(x => x.Details).NotEmpty().WithMessage("Chi tiết sản phẩm là bắt buộc");
            RuleFor(x => x.SeoDescription).NotEmpty().WithMessage("Mô tả Seo là bắt buộc");
            RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("Mô tả đường dẫn Seo là bắt buộc");
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Mô tả tên Seo là bắt buộc");
        }
    }
}