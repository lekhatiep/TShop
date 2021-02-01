using TShopSolution.ViewModels.Cart;
using System.Threading.Tasks;

namespace TShopSolution.ApiIntegration.Interface
{
    public interface IOrderApiClient
    {
        Task<bool> CreateOrder(CheckoutRequest request);
    }
}