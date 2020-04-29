using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Quizler.Web.Shared.Models.Account;
using Quizler.Web.Shared.Models.Areas.Administration.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsInRole(string role)
        {
            var authState = await this.authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return user.HasClaim("role", role);

        }

        public async Task<T> GetAll<T>() 
        {
            var users = await this.httpClient.GetJsonAsync<T>("admin/users/all");

            return users;
        }
    }
}
