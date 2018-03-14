using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Application.Models.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Application.Infrastructure.Identity;

namespace Application.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            AppIdentityDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<ApplicationUserViewModel> GetApplicationUser()
        {
            return Mapper.Map<IEnumerable<ApplicationUserViewModel>>(_context.ApplicationUsers);
        }

        // GET: api/User/5
        [HttpGet("{username}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user));
        }

        // GET: api/User/5
        [HttpGet("Journalists")]
        public async Task<IEnumerable<ApplicationUserViewModel>> GetJournalists()
        {
            var users = _userManager.GetUsersInRoleAsync("Journalist")
                .Result.ToList().AsQueryable();
            return Mapper.Map<IEnumerable<ApplicationUserViewModel>>(await Journalists(users.ToList()));
        }

        [HttpGet("JournalistByUsername/{username}")]
        public async Task<IActionResult> GetJournalistByUsername([FromRoute] string username)
        {
            var user = new ApplicationUserViewModel();

            user = Mapper.Map<ApplicationUserViewModel>(await Journalist(username));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // PUT: api/User/5
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> PutApplicationUser([FromBody] ApplicationUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.Username;
            user.Email = model.Username;
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumber = model.PhoneNumber;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /*
        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser ApplicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationUser.Add(ApplicationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUser", new { id = ApplicationUser.Id }, ApplicationUser);
        }
        */

        // DELETE: api/User/5
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                return NotFound();
            }
            var value = await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();

            return Ok("Delete was success");
        }

        #region Private Methods

        private bool ApplicationUserExists(string email)
        {
            return _context.ApplicationUsers.Any(e => e.Email == email);
        }

        private async Task<IEnumerable<ApplicationUser>> Journalists(List<ApplicationUser> users)
        {

            var userList = await _context.ApplicationUsers
                .Include(x => x.JournoRankings)
                .ToListAsync();


            var newList = new List<ApplicationUser>();
            foreach (var user in userList)
                if (users.Where(x => x.UserName == user.UserName).Any())
                    newList.Add(user);

            return newList;
        }

        private async Task<ApplicationUser> Journalist(string username)
        {
            var journalist = new ApplicationUser();
            var userList = await _context.ApplicationUsers
                .Include(x => x.JournoRankings)
                .ToListAsync();

            foreach (var user in userList)
                if (user.UserName == username)
                    journalist = user;

            return journalist;
        }

        #endregion 
    }
}