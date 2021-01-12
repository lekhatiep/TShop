using eShopSolution.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catolog.Orders
{
    public interface IOrderService
    {
        Task<int> Create(CheckoutRequest request);
    }
}