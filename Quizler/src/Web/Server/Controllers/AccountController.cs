namespace Quizler.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Quizler.Common;
    using Quizler.Data.Models;
    using Quizler.Shared.Jwt;
    using Quizler.Shared.Models.Account;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class AccountController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtSettings = jwtSettings.Value;
        }

         [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] RegisterInputModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            if (userManager.Users.Any(u => u.Email == model.Email))
            {
                return BadRequest();
            }

            if (userManager.Users.Any(u => u.UserName == model.Username))
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = model.Password
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            if (model.Role == GlobalConstants.TeacherRoleName)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.TeacherRoleName);
            }

            await signInManager.SignInAsync(user, false);

            var token = this.GenerateJwtToken(user);

            return Ok(token);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Login([FromBody] LoginInputModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await this.userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user ==null)
            {
                return BadRequest();
            }

            var result = this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Result.Succeeded)
            {
                var token = GenerateJwtToken(user);

                return Ok(token);
            }

            return BadRequest();
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role,
                            this.userManager.IsInRoleAsync(user, "Administrator")
                                .GetAwaiter()
                                .GetResult() ? "Administrator" : "User")
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
