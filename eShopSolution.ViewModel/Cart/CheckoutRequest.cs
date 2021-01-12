using System.Collections.Generic;

namespace eShopSolution.ViewModels.Cart
{
    public class CheckoutRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OrderDate { get; set; }
        public string UserId { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
    }
}