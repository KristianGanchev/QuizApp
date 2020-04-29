namespace Quizler.Web.Server.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Common;
    using Quizler.Data.Models;
    using Quizler.Services.Mapping;
    using Quizler.Web.Server.Controllers;
    using Quizler.Web.Shared.Models.Account;
    using Quizler.Web.Shared.Models.Areas.Administration.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("admin/[controller]")]
    public class UsersController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("[action]")]
        public  ActionResult<IEnumerable<UserResponse>> All() 
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.userManager.Users.OrderByDescending(u => u.CreatedOn).To<UserResponse>().ToList();
            }

            return Unauthorized();
        }
    }
}
