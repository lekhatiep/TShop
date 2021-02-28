using TShopSolution.ApiIntegration.Interface;
using TShopSolution.Utilities.Constant;
using TShopSolution.ViewModels.Cart;
using TShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TShopSolution.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IOrderApiClient _orderApiClient;

        public CartController(IProductApiClient productApiClient, IOrderApiClient orderApiClient)
        {
            _productApiClient = productApiClient;
            _orderApiClient = orderApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View(GetCheckoutViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel checkOutViewModel)
        {
            var checkoutViewModel = GetCheckoutViewModel();
            var orderDetails = new List<OrderDetailViewModel>();

            foreach (var item in checkoutViewModel.CartItems)
            {
                orderDetails.Add(new OrderDetailViewModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            var checkoutRequest = new CheckoutRequest
            {
                Name = checkOutViewModel.CheckoutRequest.Name,
                Address = checkOutViewModel.CheckoutRequest.Address,
                Email = checkOutViewModel.CheckoutRequest.Email,
                Phone = checkOutViewModel.CheckoutRequest.Phone,
                OrderDetails = orderDetails
            };
            var order = await _orderApiClient.CreateOrder(checkoutRequest);
            if (order)
            {
                TempData["Success"] = "Order purchase successfully";
                HttpContext.Session.Remove(SystemConstant.OrderSettings.CART_SESSION);
            }

            return View(checkoutViewModel);
        }

        [HttpGet]
        public IActionResult GetListCart()
        {
            List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            var sessionCart = HttpContext.Session.GetString(SystemConstant.OrderSettings.CART_SESSION);
            if (sessionCart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(sessionCart);
            }
            return Ok(listCart);
        }

        public async Task<IActionResult> AddToCart(int id, string languageId, int quan)
        {
            var product = await _productApiClient.GetById(id, languageId);
            List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            var sessionCart = HttpContext.Session.GetString("CartSession");

            //var quan = 1;
            if (sessionCart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(sessionCart);
                var itemExists = listCart.Where(x => x.ProductId == id).FirstOrDefault();
                if (itemExists != null)
                {
                    itemExists.Quantity += quan;
                    if (itemExists.Quantity > product.Stock)
                    {
                        return Ok("101");
                    }
                }
                else
                {
                    listCart.Add(await AddItem(id, languageId, quan));
                }
            }
            else
            {
                listCart.Add(await AddItem(id, languageId, quan));
            }

            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(listCart));

            return Ok(listCart);
        }

        private async Task<CartItemViewModel> AddItem(int id, string languageId, int quan)
        {
            var product = await _productApiClient.GetById(id, languageId);
            var cartItem = new CartItemViewModel
            {
                ProductId = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Quantity = quan,
                Image = product.Image,
                Stock = product.Stock
            };
            return cartItem;
        }

        public IActionResult UpdateCart(int id, int quantity)
        {
            List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            var sessionCart = HttpContext.Session.GetString("CartSession");
            if (sessionCart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(sessionCart);
                foreach (var item in listCart)
                {
                    if (item.ProductId == id)
                    {
                        if (quantity == 0)
                        {
                            item.Quantity = 1;
                            break;
                        }
                        item.Quantity = quantity;
                    }
                }
            }
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(listCart));

            return Ok(listCart);
        }

        public IActionResult DeleteItemCart(int id)
        {
            List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            var sessionCart = HttpContext.Session.GetString("CartSession");
            if (sessionCart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(sessionCart);
                foreach (var item in listCart)
                {
                    if (item.ProductId == id)
                    {
                        listCart.Remove(item);
                        break;
                    }
                }
            }
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(listCart));

            return Ok(listCart);
        }

        private CheckoutViewModel GetCheckoutViewModel()
        {
            List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            var sessionCart = HttpContext.Session.GetString(SystemConstant.OrderSettings.CART_SESSION);
            if (sessionCart != null)
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(sessionCart);
            }
            var checkoutViewModel = new CheckoutViewModel
            {
                CartItems = listCart,
                CheckoutRequest = new CheckoutRequest()
            };
            return checkoutViewModel;
        }
    }
}