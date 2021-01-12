using eShopSolution.ViewModels.Cart;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration.Interface
{
    public interface IOrderApiClient
    {
        Task<bool> CreateOrder(CheckoutRequest request);
    }
}