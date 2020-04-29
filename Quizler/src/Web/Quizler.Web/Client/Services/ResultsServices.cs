using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public class ResultsServices : IResultsService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public ResultsServices(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<TResponse> CreateAsync<TResponse, TRequiest>(TRequiest model) 
        {
            var response = await this.httpClient.PostJsonAsync<TResponse>("/results/create", model);

            if (response == null)
            {
                this.navigationManager.NavigateTo("/error");
            }

            return response;
        }

        public async Task<T> GetById<T>(int resultId, string route) 
        {
            var result = await this.httpClient.GetJsonAsync<T>($"{route}{resultId}");

            return result;
        }

        public async Task<T> GetMyResults<T>()
        {
            var results = await this.httpClient.GetJsonAsync<T>("/results/userresults");

            return results;
        }

        public async Task DeleteAsync(int resultId)
        {
            await httpClient.DeleteAsync($"results/delete/{resultId}");
            this.navigationManager.NavigateTo("results/my-results");
        }
    }
}
