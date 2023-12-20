using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PudgeManga_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDBContext _context;
        public AccountController(UserManager<User> userManager,
                   SignInManager<User> signInManager,
                   ApplicationDBContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return PartialView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(LoginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(LoginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, LoginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, LoginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Невірні дані, спробуйте ще раз";
                return PartialView(LoginViewModel);
            }
            TempData["Error"] = "Невірні дані, спробуйте ще раз";
            return PartialView(LoginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {

            var response = new RegisterViewModel();
            return PartialView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {

                return PartialView(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Ця електрона адреса уже використовується!";
                return PartialView(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.UserName
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                await _signInManager.SignInAsync(newUser, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return PartialView(registerViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("Account/Welcome")]
        public async Task<IActionResult> Welcome(int page = 0)
        {
            if (page == 0)
            {
                return View();
            }
            return View();

        }

    }
}