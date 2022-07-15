using Homework.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Security.Claims;

namespace Homework_IS.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        //INDEX
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._cartService.getShoppingCartInfo(userId));
        }

        //DELETE
        public IActionResult DeleteTicketFromCart(Guid id)
        {
            //deletion code
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._cartService.deleteTicketFromShoppingCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }



        //ORDERNOW
        private Boolean OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._cartService.orderNow(userId);

            return result;
        }


        //PAYORDER 
        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this._cartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "ECinema Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.OrderNow();

                if (result)
                {
                    return RedirectToAction("Index", "Cart");
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}
