using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonitoriOn.Models;
using MonitoriOn.Models.ViewModels;
using System.ComponentModel;

namespace MonitoriOn.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<MonitoriOnUser> _userManager;
        private readonly SignInManager<MonitoriOnUser> _signInManager;

        public ProfileController(UserManager<MonitoriOnUser> userManager, SignInManager<MonitoriOnUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с Id '{_userManager.GetUserId(User)}'.");
            }

            var newUser = new UserVM
            {
                FirstName = user.FirstName,
                Email = await _userManager.GetEmailAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
            };

            return View(newUser);
        }

        // GET - DeletePersonalData
        public IActionResult DeletePersonalData()
        {
            return PartialView("_DeletePersonalData");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeletePersonalData")]
        public async Task<IActionResult> DeletePersonalDataPost(string password)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с Id '{_userManager.GetUserId(User)}'.");
            }


            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                string incorrectPassword = "Неверный пароль";

                TempData["DeleteUser"] = incorrectPassword;

                var newUser = new UserVM
                {
                    FirstName = user.FirstName,
                    Email = await _userManager.GetEmailAsync(user),
                    PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
                };

                TempData["IncorrectPassword"] = "true";

                return View(nameof(Index), newUser);
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Произошла непредвиденная ошибка при удалении пользователя.");
            }

            TempData["IncorrectPassword"] = "false";


            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET - ChangePassword
        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangePassword")]
        public async Task<IActionResult> ChangePasswordPost(ChangePasswordVM changePasswordVM)
        {
            var user = await _userManager.GetUserAsync(User);

            var newUser = new UserVM
            {
                FirstName = user.FirstName,
                Email = await _userManager.GetEmailAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
            };

            if (!ModelState.IsValid)
            {
                TempData["ChangePassword"] = "true";

                return View(nameof(Index), newUser);
            }


            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с Id '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordVM.OldPassword, changePasswordVM.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                string incorrectPassword = "Неверный пароль";

                TempData["ChangePassowrdIncorrect"] = incorrectPassword;

                TempData["ChangePassword"] = "true";

                return View(nameof(Index), newUser);
            }

            TempData["ChangePassword"] = "false";


            await _signInManager.RefreshSignInAsync(user);

            return View(nameof(Index), newUser);
        }

        // GET - ChangeName
        public IActionResult ChangeName()
        {
            return PartialView("_ChangeName");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangeName")]
        public async Task<IActionResult> ChangeNamePost(UserVM userVM)
        {
            var user = await _userManager.GetUserAsync(User);

            var newUser = new UserVM
            {
                FirstName = user.FirstName,
                Email = await _userManager.GetEmailAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
            };

            if (!ModelState.IsValid)
            {
                TempData["ChangeName"] = "true";

                return View(nameof(Index), newUser);
            }

            TempData["ChangeName"] = "false";

            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с Id '{_userManager.GetUserId(User)}'.");
            }

            bool result = user.FirstName.Equals(userVM.FirstName);

            if (result)
            {
                return View(nameof(Index), newUser);
            }
            
            if (userVM.FirstName != null)
            {
                user.FirstName = userVM.FirstName;

                newUser.FirstName = userVM.FirstName;

                await _userManager.UpdateAsync(user);
            }

            return View(nameof(Index), newUser);
        }

        // GET - ChangePhoneNumber
        public IActionResult ChangePhoneNumber()
        {
            return PartialView("_ChangePhoneNumber");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangePhoneNumber")]
        public async Task<IActionResult> ChangePhoneNumberPost(UserVM userVM)
        {
            var user = await _userManager.GetUserAsync(User);

            var newUser = new UserVM
            {
                FirstName = user.FirstName,
                Email = await _userManager.GetEmailAsync(user),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
            };

            if (!ModelState.IsValid)
            {
                TempData["ChangePhoneNumber"] = "true";

                return View(nameof(Index), newUser);
            }
            TempData["ChangePhoneNumber"] = "false";

            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с Id '{_userManager.GetUserId(User)}'.");
            }

            bool result;

            if (user.PhoneNumber != null)
            {
                result = user.PhoneNumber.Equals(userVM.PhoneNumber);
            }
            else
            {
                if (userVM.PhoneNumber != null)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }

            if (result)
            {
                return View(nameof(Index), newUser);
            }

            user.PhoneNumber = userVM.PhoneNumber;

            newUser.PhoneNumber = userVM.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return View(nameof(Index), newUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("LogOut")]
        public async Task<IActionResult> LogOutPost()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
