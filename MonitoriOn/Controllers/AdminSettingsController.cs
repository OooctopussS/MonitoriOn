using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonitoriOn.Data;
using MonitoriOn.Models;

namespace MonitoriOn.Controllers
{
    public class AdminSettingsController : Controller
    {
        private readonly UserManager<MonitoriOnUser> _userManager;

        public AdminSettingsController(UserManager<MonitoriOnUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string? warning)
        {
            if (TempData["OpenBlock"] == null)
            {
                TempData["OpenBlock"] = 0;
            }

            return View((object?) warning);
        }

        // GET - SETADMIN
        public IActionResult SetAdmin()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("SetAdmin")]
        public async Task<IActionResult> SetAdminPost(string name)
        {
            TempData["OpenBlock"] = 1;

            if (name == null)
            {
                string notFound = "Пользователь с такой почтой не найден";

                @TempData["SetAdmin"] = notFound;

                return RedirectToAction(nameof(SetAdmin));
            }

            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                if (!await _userManager.IsInRoleAsync(user, WC.AdminRole))
                {
                    await _userManager.AddToRoleAsync(user, WC.AdminRole);
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    string userInRole = "Этот пользователь уже является админом";

                    @TempData["SetAdmin"] = userInRole;

                    return RedirectToAction(nameof(SetAdmin));

                }
            }
            else
            {
                string notFound = "Пользователь с такой почтой не найден";

                @TempData["SetAdmin"] = notFound;

                return RedirectToAction(nameof(SetAdmin));
            }

            string successfullAdded = "Роль успешна присвоена пользователю";

            @TempData["SetAdmin"] = successfullAdded;

            return RedirectToAction(nameof(SetAdmin));
        }

        // GET - DELETEADMIN
        public IActionResult DeleteAdmin()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteAdmin")]
        public async Task<IActionResult> DeleteAdminPost(string name)
        {
            TempData["OpenBlock"] = 2;

            if (name == null)
            {
                string notFound = "Пользователь с такой почтой не найден";

                @TempData["DeleteAdmin"] = notFound;

                return RedirectToAction(nameof(DeleteAdmin));
            }

            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, WC.AdminRole))
                {
                    await _userManager.RemoveFromRoleAsync(user, WC.AdminRole);
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    string userInRole = "Этот пользователь не является админом";

                    @TempData["DeleteAdmin"] = userInRole;

                    return RedirectToAction(nameof(DeleteAdmin));

                }
            }
            else
            {
                string notFound = "Пользователь с такой почтой не найден";

                @TempData["DeleteAdmin"] = notFound;

                return RedirectToAction(nameof(DeleteAdmin));
            }

            string successfullAdded = "Роль успешна удалена у пользователя";

            @TempData["DeleteAdmin"] = successfullAdded;

            return RedirectToAction(nameof(DeleteAdmin));
        }

    }
}
