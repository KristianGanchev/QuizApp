using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Quizler.Web.Shared.Models.Categories;
using Quizler.Web.Shared.Models.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
     public class ApiService : IApiService
    {
        private readonly ILocalStorageService storage;
        private readonly NavigationManager navigation;

        public ApiService(ILocalStorageService storage, NavigationManager navigation)
        {
            this.storage = storage;
            this.navigation = navigation;
        }

        public Task<CategorieResponse[]> GetCategoriesNames() =>
            this.GetJson<CategorieResponse[]>("categories/getall");

        public Task<QuizResponse> CreateQuiz(QuizCreateRequest request) =>
            this.PostJson<QuizCreateRequest, QuizResponse>("quizzes/create", request);

        public Task<QuizEditResponse> GetQuizById(int id) =>
           this.GetJson<QuizEditResponse>($"quizzes/{id}");

        private async Task<T> GetJson<T>(string url)
        {
            var httpClient = new HttpClient();

            var token = await this.storage.GetItemAsync<string>("authToken");

            httpClient.BaseAddress = new Uri(this.navigation.BaseUri);

            if (string.IsNullOrEmpty(token) == false)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            else
            {
                this.navigation.NavigateTo("account/login");
            }

            //TODO: error handling
            return await httpClient.GetJsonAsync<T>(url);

        }


        private async Task<TResponse> PostJson<TRequset, TResponse>(string url, TRequset request)
        {
            var httpClient = new HttpClient();


            var token = await this.storage.GetItemAsync<string>("authToken");

            httpClient.BaseAddress = new Uri(this.navigation.BaseUri);

            if (string.IsNullOrEmpty(token) == false)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
            else
            {
                this.navigation.NavigateTo("account/login");
            }

            //TODO: error handling
            return await httpClient.PostJsonAsync<TResponse>(url, request);

        }
    }
}
