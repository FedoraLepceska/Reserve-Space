using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Stripe;
using System;
using System.Security.Claims;

namespace Reserve_Space.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._reservationService.GetReservationInfo(userId));
        }

        public IActionResult DeleteSpaceFromReservation(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._reservationService.DeleteSpaceFromReservation(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "Reservation");
            }
            else
            {
                return RedirectToAction("Index", "Reservation");
            }
        }

        private Boolean Reserve()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._reservationService.Reserve(userId);

            return result;
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this._reservationService.GetReservationInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Reserve Space Payment",
                Currency = "eur",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.Reserve();

                if (result)
                {
                    return RedirectToAction("Index", "Reservation");
                }
                else
                {
                    return RedirectToAction("Index", "Reservation");
                }
            }

            return RedirectToAction("Index", "Reservation");
        }
    }
}
