using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Services.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


/*
 
 @inject SignInManager<Data.AppUser> SignInManager
@inject UserManager<Data.AppUser> UserManager
 
 */
namespace Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<Data.AppUser> _userManager;
        private readonly SignInManager<Data.AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUsersService _usersService;

        public LoginModel(SignInManager<Data.AppUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<Data.AppUser> userManager, IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _usersService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }



        public async Task OnGetAsync(string returnUrl = null)
        
        {
            var feature = HttpContext.Features.Get<IRequestCultureFeature>();
            var _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                AppUser user = _usersService.FindByUserName(Input.userName);

                if (user == null)
                {
                    ModelState.AddModelError("", "LOGIN_INVALID_ATTEMPT");
                    return Page();
                }

                if (!user.active)
                {
                    ModelState.AddModelError("", "This user is blocked");
                    return Page();
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.userName, Input.Password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = true });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
    public class InputModel
    {

        public string userName { get; set; }
        public string Password { get; set; }
        public bool active { get; set; }
    }
}
