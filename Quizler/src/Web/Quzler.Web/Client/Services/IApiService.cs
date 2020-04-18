namespace Quzler.Web.Client.Services
{
    using Quzler.Web.Shared.Categories;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IApiService
    {
        public Task<CategorieViewModel[]> GetCategoriesNames();
    }
}
