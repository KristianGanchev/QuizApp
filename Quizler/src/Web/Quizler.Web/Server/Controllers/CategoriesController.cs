namespace Quizler.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Quizler.Services.Data;
    using System.Collections.Generic;
    using System.Linq;
    using Quizler.Web.Shared.Models.Categories;
    using Microsoft.AspNetCore.Authorization;

    [AllowAnonymous]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet("[action]")]
        public IEnumerable<CategorieResponse> GetAll()
        {

            return this.categoriesService.GetAll<CategorieResponse>().ToArray();

        }
    }
}
