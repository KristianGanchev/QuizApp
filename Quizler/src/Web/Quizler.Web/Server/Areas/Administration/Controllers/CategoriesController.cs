namespace Quizler.Web.Server.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Common;
    using Quizler.Services.Data;
    using Quizler.Web.Server.Controllers;
    using Quizler.Web.Shared.Models.Areas.Administration;
    using Quizler.Web.Shared.Models.Common;
    using System.Threading.Tasks;

    [Route("admin/[controller]")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryRequest inputModel) 
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                var categorie = this.categoriesService.GetByName<CategoryResponse>(inputModel.Name);

                if (categorie != null)
                {
                    return BadRequest(new BadRequest
                    {
                        Message = "This category allready exist!"
                    }); ;
                }

                if (!this.ModelState.IsValid)
                {
                    return BadRequest();
                }

                var categorieId = await this.categoriesService.CreateAsync(inputModel.Name);

                return new CategoryResponse { Id = categorieId };
            }

            return Unauthorized();
        }
    }
}
