using TShopSolution.ViewModels.Cart;
using System.Collections.Generic;

namespace TShopSolution.WebApp.Models
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public CheckoutRequest CheckoutRequest { get; set; }
    }
}