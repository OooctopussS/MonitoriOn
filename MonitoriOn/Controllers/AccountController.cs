using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;

namespace MonitoriOn.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<MonitoriOnUser> _signInManager;
        private readonly UserManager<MonitoriOnUser> _userManager;


        public AccountController(
            SignInManager<MonitoriOnUser> signInManager,
            UserManager<MonitoriOnUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }



        // GET - Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterUserVM registerUserVM)
        {
            if (ModelState.IsValid)
            {
                var user = new MonitoriOnUser
                {
                    FirstName = registerUserVM.FirstName,
                    Email = registerUserVM.Email,
                    PhoneNumber = registerUserVM.PhoneNumber,
                    UserName = registerUserVM.Email
                };

                var result = await _userManager.CreateAsync(user, registerUserVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, WC.UserRole);

                    // Отправлять письмо на почту для подтверждения

                    // если подтвердил почту
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return View();
        }

        // GET - Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginUserVM loginUserVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserVM.Email, loginUserVM.Password, loginUserVM.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");

                return View();

            }

            return View();
        }

    }
}
