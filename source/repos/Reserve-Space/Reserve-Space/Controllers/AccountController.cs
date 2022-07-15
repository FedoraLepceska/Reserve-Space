using Domain.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reserve_Space.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Reservation = new Reservation()
                    };
                    var result = await userManager.CreateAsync((ApplicationUser)user, request.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("NewAccount");
                    }
                }
                else
                {
                    return View(request);
                }
            }
            return View(request);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(request);

                }
                if (await userManager.CheckPasswordAsync(user, request.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(request);

                }


                var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));                    
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(request);
                }
            }
            return View(request);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult NewAccount()
        {
            return View();
        }

    }
}
