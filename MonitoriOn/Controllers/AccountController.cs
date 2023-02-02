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
                //var user = Activator.CreateInstance<MonitoriOnUser>();
                var user = new MonitoriOnUser
                {
                    FirstName = registerUserVM.FirstName,
                    Email = registerUserVM.Email,
                    PhoneNumber = registerUserVM.PhoneNumber,
                    UserName = registerUserVM.Email
                };

                //user.FirstName = registerUserVM.FirstName;

                //await _userStore.SetUserNameAsync(user, registerUserVM.Email, CancellationToken.None);
                //await _emailStore.SetEmailAsync(user, registerUserVM.Email, CancellationToken.None);
                //await _phoneNumberStore.SetPhoneNumberAsync(user, registerUserVM.PhoneNumber, CancellationToken.None);

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

            return View(registerUserVM);
        }

    }
}
