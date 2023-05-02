using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using System.Text.Encodings.Web;
using System.Text;

namespace MonitoriOn.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<MonitoriOnUser> _userManager;
        private readonly SignInManager<MonitoriOnUser> _signInManager;

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

        // GET - ForgotPassword
        public IActionResult ForgotPassword()
        {
            return PartialView("_ForgotPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordPost(UserVM userVM)
        {
            TempData["ForgotPasswordModal"] = "false";

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userVM.Email);

                if (user == null)
                {

                    TempData["IncorrectEmail"] = "Такой пользователь не зарегестрирован";

                    TempData["ForgotPasswordModal"] = "true";

                    return RedirectToAction(nameof(Login), "Account");
                }

                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    TempData["IncorrectEmail"] = "У данного аккаунта не подтверждена почта";

                    TempData["ForgotPasswordModal"] = "true";

                    return RedirectToAction(nameof(Login), "Account");
                }

                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ResetPassword",
                //pageHandler: null,
                //    values: new { area = "Identity", code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    Input.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            }

            return RedirectToAction(nameof(Login), "Account");

        }

        // GET - ResetPassword
        public IActionResult ResetPassword(string? code = null)
        {
            if (code == null)
            {
                return NotFound("A code must be supplied for password reset.");
            }
            else
            {
                TempData["ResetPasswordCode"] = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                TempData["ResetPasswordModal"] = "true";

                return RedirectToAction(nameof(Login), "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ResetPassword")]
        public async Task<IActionResult> ResetPasswordPost(ResetPasswordVM resetPasswordVM)
        {
            TempData["ResetPasswordModal"] = "true";
            TempData["ResetPasswordCode"] = resetPasswordVM.Code;

            if (!ModelState.IsValid)
            {
                TempData["ResetPasswordError"] = "Данные введены не корректно";

                return RedirectToAction(nameof(Login), "Account");
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);

            if (user == null)
            {
                TempData["ResetPasswordError"] = "Пользователь не найден";

                return RedirectToAction(nameof(Login), "Account");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordVM.Code, resetPasswordVM.Password);

            if (result.Succeeded)
            {
                TempData["ResetPasswordModal"] = "false";

                return RedirectToAction(nameof(Login), "Account");
            }

            TempData["ResetPasswordModal"] = "true";

            foreach (var error in result.Errors)
            {
                TempData["ResetPasswordModal"] = String.Concat(TempData["ResetPasswordModal"], error.Description);
            }

            return RedirectToAction(nameof(Login), "Account");
        }

    }
}
