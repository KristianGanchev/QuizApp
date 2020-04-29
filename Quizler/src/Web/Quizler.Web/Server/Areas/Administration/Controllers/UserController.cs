namespace Quizler.Web.Server.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Common;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Server.Controllers;
    using Quizler.Web.Shared.Models.Account;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("admin/[controller]")]
    public class UserController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("[action]")]
        public  ActionResult<IEnumerable<LoginResponseModel>> All() 
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.userManager.Users.To<LoginResponseModel>().ToList();
            }

            return Unauthorized();
        }
    }
}
