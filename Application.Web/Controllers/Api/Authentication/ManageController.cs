using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Application.Infrastructure.Identity;
using Application.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Web.Controllers.Api.Authentication
{
    [Produces("application/json")]
    [Route("api/Manage")]
    public class ManageController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ILogger<ManageController> _logger;
        private UrlEncoder _urlEncode;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<ManageController> logger,
          UrlEncoder urlEncode
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncode = urlEncode;
        }

        [TempData]
        public string StatusMessage { get; set; }


        [HttpGet]
        public async Task<IndexViewModel> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel();
            string role = null;

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count != 0)
                    role = roles.ToList().FirstOrDefault();

                model = new IndexViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsEmailConfirmed = user.EmailConfirmed,
                    StatusMessage = StatusMessage,
                    Role = role
                };
            }

            return model;
        }

        [HttpGet("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return Ok("Redirect to set password.");
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return Ok(model);
        }

        [HttpPost("ChangePassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                //AddErrors(changePasswordResult);
                return Ok(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return Ok(StatusMessage);
        }
    }
}
