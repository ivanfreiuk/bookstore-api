using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStore.DataAccess.Identity;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace BookStore.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }

                var user= await _userManager.FindByNameAsync(model.Email);
                


                var response = new
                {
                    access_token = GetToken(user),
                    user = new UserModel
                    {
                        Id = user.Id,
                        RoleName = _userManager.GetRolesAsync(user).Result.Single(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    }
                };

                return Ok(response);
            }
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
            );
            return Ok(GetToken(user));

        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    //TODO: Use Automapper instaed of manual binding

                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };

                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(GetToken(user));
                }
                else
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            return BadRequest(ModelState);


        }

        private string GetToken(User user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSettings:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("TokenSettings:Lifetime")),
                audience: _configuration.GetValue<string>("TokenSettings:Audience"),
                issuer: _configuration.GetValue<string>("TokenSettings:Issuer")
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        //    if (!result.Succeeded)
        //    {
        //        return BadRequest("Invalid password or email.");
        //    }

        //    var user = _userManager.Users.First(u => u.Email == model.Email);

        //    var response = new
        //    {
        //        access_token = _tokenHelper.GetAccessToken(user)
        //    };

        //    return Ok(response);
        //}

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody]RegisterModel model)
        //{
        //    var resultExist = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        //    if (resultExist != null)
        //    {
        //        return BadRequest("User already exist.");
        //    }

        //    var user = new User
        //    {
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Email = model.Email
        //    };

        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return StatusCode(201, "Successfully registered");
        //    }
        //    else
        //    {
        //        return GetErrorResult(result);
        //    }
        //}

        //private string GetToken(IdentityUser user)
        //{
        //    var utcNow = DateTime.UtcNow;

        //    var claims = new Claim[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
        //    };

        //    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetValue<String>("Tokens:Key")));
        //    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        //    var jwt = new JwtSecurityToken(
        //        signingCredentials: signingCredentials,
        //        claims: claims,
        //        notBefore: utcNow,
        //        expires: utcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime")),
        //        audience: this.configuration.GetValue<String>("Tokens:Audience"),
        //        issuer: this.configuration.GetValue<String>("Tokens:Issuer")
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(jwt);

        //}

        //private IActionResult GetErrorResult(IdentityResult result)
        //{
        //    if (result.Errors != null)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    return BadRequest(ModelState);
        //}
    }
}
