using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public CategoriesService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<T> GetAll<T>(string route)
        {
            var categories = await this.httpClient.GetJsonAsync<T>(route);

            return categories;
        }
    }
}
