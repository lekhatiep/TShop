using TShopSolution.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TShopSolution.Application.Catolog.Orders
{
    public interface IOrderService
    {
        Task<int> Create(CheckoutRequest request);
    }
}