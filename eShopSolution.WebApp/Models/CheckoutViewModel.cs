using eShopSolution.ViewModels.Cart;
using System.Collections.Generic;

namespace eShopSolution.WebApp.Models
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public CheckoutRequest CheckoutRequest { get; set; }
    }
}