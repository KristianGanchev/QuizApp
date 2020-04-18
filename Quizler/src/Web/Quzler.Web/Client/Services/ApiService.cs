namespace Quzler.Web.Client.Services
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Quzler.Web.Shared.Categories;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiService : IApiService
    {
        private readonly IAccessTokenProvider authenticationService;
        private readonly NavigationManager navigation;

        public ApiService(IAccessTokenProvider authenticationService, NavigationManager navigation)
        {
            this.authenticationService = authenticationService;
            this.navigation = navigation;
        }

        public Task<CategorieViewModel[]> GetCategoriesNames() =>
            this.GetJson<CategorieViewModel[]>("categories/getall");

        private async Task<T> GetJson<T>(string url)
        {
            var httpClient = new HttpClient();

            var tokenResult = await this.authenticationService.RequestAccessToken();

            httpClient.BaseAddress = new Uri(this.navigation.BaseUri);

            if (tokenResult.TryGetToken(out var token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                this.navigation.NavigateTo(tokenResult.RedirectUrl);
            }

            //TODO: error handling
            return await httpClient.GetJsonAsync<T>(url);

        }
    }
}
