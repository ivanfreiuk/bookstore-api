using System.Threading.Tasks;
using BookStore.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.AllAsync(i => true);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}
