using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Infrastructure.Data;
using Application.Infrastructure.Identity;
using Application.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers.Api.Authentication
{
    [Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("UserRoles")]
        public async Task<IActionResult> UserRoles()
        {
            RoleViewModel model = new RoleViewModel();

            var user = await _userManager.GetUserAsync(User); //var user = await GetCurrentUserAsync();
            if (user == null)
                return BadRequest("Not logged in.");

            var roles = await _userManager.GetRolesAsync(user);
            model.Username = user.UserName;
            model.Role = roles.ToList()[0];

            return Ok(model);
        }

        [HttpGet("UserRolesByUsername/{username}")]
        public async Task<IActionResult> UserRolesByUsername([FromRoute] string username)
        {
            RoleViewModel model = new RoleViewModel();

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
                return NotFound("User not Found.");

            model.Username = username;

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 0)
                return Ok(model);

            model.Role = roles.ToList()[0];

            return Ok(model);
        }

        [HttpGet("OtherRolesByUsername/{username}")]
        public async Task<IActionResult> GetOtheRolesByUsername([FromRoute] string username)
        {
            var rolesList = _roleManager.Roles.ToList();
            if (!_roleManager.Roles.Any())
                return Ok("Roles where not created.");

            List<string> availableRoles = new List<string>();
            foreach (var roleItem in rolesList)
                availableRoles.Add(roleItem.Name);

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
                return NotFound("User not Found.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = Sets<string>.Complement(availableRoles, userRoles.ToList());

            return Ok(roles);
        }

        [HttpGet("OtherRoles")]
        public async Task<IActionResult> GetOtherRoles()
        {

            var rolesList = _roleManager.Roles.ToList();
            if (!_roleManager.Roles.Any())
                return Ok("Roles where not created.");

            List<string> availableRoles = new List<string>();
            foreach (var roleItem in rolesList)
                availableRoles.Add(roleItem.Name);

            var user = await GetCurrentUserAsync();
            if (user == null)
                return BadRequest("Not logged in.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = Sets<string>.Complement(availableRoles, userRoles.ToList());

            return Ok(roles);
        }

        [HttpPut("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RoleViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
                return NotFound("User not Found.");

            if (model.Role == null || model.Role == "")
                return BadRequest("No role to assign.");

            var roles = _roleManager.Roles.ToList();
            if (RolesListOfStrings(roles).Contains(model.Role))
                await _userManager.AddToRoleAsync(user, model.Role);
            else
                return BadRequest("Select a role to assign.");

            return Ok("Role assigned.");
        }

        private List<string> RolesListOfStrings(List<IdentityRole> roles)
        {
            List<string> rolesList = new List<string>();
            foreach (var role in roles)
                rolesList.Add(role.Name);
            return rolesList;
        }

        [HttpPut("AssignRoles")]
        public async Task<IActionResult> AssignRoles([FromBody] RoleViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
                return NotFound("User not Found.");

            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok("Roles assigned.");
        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> RemoveRole([FromBody] RoleViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
                return NotFound("User not Found.");

            await _userManager.RemoveFromRoleAsync(user, model.Role);
            return Ok("Role has been removed.");
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}