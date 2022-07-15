using Homework.Domain.DomainModels;
using Homework.Domain.Identity;
using Homework.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Homework_IS.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<CinemaApplicationUser> userManager;
        private readonly ITicketService _ticketService;

        public AdminController(IOrderService orderService, UserManager<CinemaApplicationUser> userManager, ITicketService ticketService)
        {

            this._orderService = orderService;
            this._ticketService = ticketService;
            this.userManager = userManager;
        }
        //TICKET API ACTIONS
        [HttpGet("[action]")]
        public List<Ticket> GetAllTickets()
        {
            return this._ticketService.GetAllTickets();
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this._orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<CinemaApplicationUser> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new CinemaApplicationUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = item.Role,
                        UserCart = new ShoppingCart()
                    };
                    var result = userManager.CreateAsync((CinemaApplicationUser)user, item.Password).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, user.Role);
                    }

                    status = status && result.Succeeded;


                }
                else
                {
                    continue;
                }
            }

            return status;
        }
    }
}
