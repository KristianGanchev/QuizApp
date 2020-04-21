using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizler.Services.Data;
using Quzler.Web.Shared.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quzler.Web.Server.Controllers
{
    //[Authorize]
    public class CategoriesController : ApiCotroller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet("[action]")]
        public IEnumerable<CategorieResponseModel> GetAll() 
        {

            return this.categoriesService.GetAll<CategorieResponseModel>().ToArray();

        }
    }
}
