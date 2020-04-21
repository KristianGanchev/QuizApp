using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Quizler.Common;
    using Quizler.Data.Models;
    using Quizler.Web.Server.Authentication;
    using Quizler.Web.Shared.Models.Account;
    using Quizler.Web.Shared.Models.Common;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class AccountsController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtAuthenticationService authenticationService;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtAuthenticationService authenticationService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<RegisterResponseModel>> Register([FromBody] RegisterRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            if (userManager.Users.Any(u => u.Email == model.Email))
            {
                return BadRequest(new BadRequestModel
                {
                    Message = "This e-mail is already taken!"
                });
            }

            if (userManager.Users.Any(u => u.UserName == model.Username))
            {
                return BadRequest(new BadRequestModel
                {
                    Message = "This username is already taken!"
                });
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new RegisterResponseModel { Successful = false, Errors = errors });
            }

            if (model.Role == GlobalConstants.TeacherRoleName)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.TeacherRoleName);
            }


            return Ok(new RegisterResponseModel { Successful = true });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await this.userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return BadRequest(new BadRequestModel
                {
                    Message = "Incorrect e-mail or password."
                });
            }

            var result = this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Result.Succeeded)
            {

                return BadRequest(new BadRequestModel
                {
                    Message = "Incorrect e-mail or password."
                });
            }

            var token = this.authenticationService.Authenticate(user);

            return new LoginResponseModel { Successful = true, access_token = token };
        }
    }
}
