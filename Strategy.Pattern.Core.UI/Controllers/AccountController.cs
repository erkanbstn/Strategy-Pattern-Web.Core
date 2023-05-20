using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Strategy.Pattern.Core.UI.Models;
using System.Threading.Tasks;

namespace Strategy.Pattern.Core.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var hasUser = await _userManager.FindByEmailAsync(Email);
            if (hasUser == null) return View();
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, Password, true, false);
            if (!signInResult.Succeeded) return View();
            return RedirectToAction("Index", "Settings");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUser appUser, string Password)
        {
            var hasUser = await _userManager.FindByEmailAsync(appUser.Email);
            if (hasUser != null)
            {
                ViewBag.userexist = "This User Email Already Exist.!";
                return View();
            }
            var create = await _userManager.CreateAsync(appUser, Password);
            if (!create.Succeeded)
            {
                ViewBag.createerror = "An Error Occurred While Registering.!";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
