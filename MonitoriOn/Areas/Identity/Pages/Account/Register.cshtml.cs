#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MonitoriOn.Models;

namespace MonitoriOn.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<MonitoriOnUser> _signInManager;
        private readonly UserManager<MonitoriOnUser> _userManager;
        private readonly IUserStore<MonitoriOnUser> _userStore;
        private readonly IUserEmailStore<MonitoriOnUser> _emailStore;
        private readonly IUserPhoneNumberStore<MonitoriOnUser> _phoneNumberStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<MonitoriOnUser> userManager,
            IUserStore<MonitoriOnUser> userStore,
            SignInManager<MonitoriOnUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _phoneNumberStore = GetPhoneNumberStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Это поле обязательное")]
            [EmailAddress(ErrorMessage = "Введите корректную почту")]
            [Display(Name = "Почта")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Это поле обязательное")]
            [StringLength(30, ErrorMessage = "Пароль должен быть длиной от {2} до {1} символов", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтверждение пароля")]
            [Compare("Password", ErrorMessage = "Пароль не совпадает с указанным")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Это поле обязательное")]
            [MaxLength(30, ErrorMessage = "Максимальное кол-во символов {1}")]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }

            [Phone(ErrorMessage = "Укажите корректный номер")]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!await _roleManager.RoleExistsAsync(WC.AdminRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(WC.AdminRole));
                await _roleManager.CreateAsync(new IdentityRole(WC.UserRole));
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = Input.FirstName;

                await _phoneNumberStore.SetPhoneNumberAsync(user, Input.PhoneNumber, CancellationToken.None);

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, WC.UserRole);

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Подтвердите вашу почту",
                        $"Подтвердите ваш аккаунт <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Нажмите сюда</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private MonitoriOnUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<MonitoriOnUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(MonitoriOnUser)}'. " +
                    $"Ensure that '{nameof(MonitoriOnUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<MonitoriOnUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<MonitoriOnUser>)_userStore;
        }

        private IUserPhoneNumberStore<MonitoriOnUser> GetPhoneNumberStore()
        {
            if (!_userManager.SupportsUserPhoneNumber)
            {
                throw new NotSupportedException("The default UI requires a user store with phone number support.");
            }
            return (IUserPhoneNumberStore<MonitoriOnUser>)_userStore;
        } 
    }
}
