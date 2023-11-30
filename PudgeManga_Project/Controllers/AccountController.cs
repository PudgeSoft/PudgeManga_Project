using Microsoft.AspNetCore.Mvc;
using PudgeManga_Project.Data;
using PudgeManga_Project.Models;
using PudgeManga_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Runtime.ConstrainedExecution;

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
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(LoginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(LoginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, LoginViewModel.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, LoginViewModel.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Try again";
                return View(LoginViewModel);
            }
            TempData["Error"] = "Wrong credentials. Try again";
            return View(LoginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new User()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                //Цей блок коду потрібен для перевірки справжності почти, на почту надсилається повідомлення на яке потрібно перейти для підтвердження реєстрації
                //Поки цей блок коду хай буде закоміченний, більшість коду зробленна, але поки це не працює можливо якщо закінчу з профелем раніше дороблю
                // генерация токена для пользователя
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                //var callbackUrl = Url.Action(
                //    "ConfirmEmail",
                //    "Account",
                //    new { userId = newUser.Id, code = code },
                //    protocol: HttpContext.Request.Scheme);
                //EmailService emailService = new EmailService();
                //await emailService.SendEmailAsync(registerViewModel.EmailAddress, "Confirm your account",
                //    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                //Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
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
                return View(registerViewModel);
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