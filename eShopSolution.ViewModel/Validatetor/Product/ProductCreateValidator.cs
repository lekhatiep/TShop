using eShopSolution.ViewModels.Catalog.Products;
using FluentValidation;

namespace eShopSolution.ViewModels.Validatetor.Product
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên sản phẩm là bắt buộc").MaximumLength(200).WithMessage("Tên sản phẩm không vượt quá 200 ký tự").MinimumLength(5).WithMessage("Tên sản phẩm ít nhất 5 ký tự");
        }
    }
}