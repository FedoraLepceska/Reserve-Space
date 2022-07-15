using Domain.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Collections.Generic;

namespace Reserve_Space.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISpaceService _spaceService;

        public AdminController(IOrderService orderService, UserManager<ApplicationUser> userManager, ISpaceService spaceService)
        {
            _orderService = orderService;
            this.userManager = userManager;
            _spaceService = spaceService;
        }

        [HttpGet("[action]")]
        public List<Space> GetAllSpaces()
        {
            return this._spaceService.GetAllSpaces();
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this._orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this._orderService.GetOrderDetails(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<ApplicationUser> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Role = item.Role,
                        Reservation = item.Reservation
                    };
                    var result = userManager.CreateAsync((ApplicationUser)user, item.Password).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, user.Role.ToString());
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
