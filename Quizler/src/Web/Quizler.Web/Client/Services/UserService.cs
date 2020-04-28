using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsInRole(string role)
        {
            var authState = await this.authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return user.HasClaim("role", role);

        }
    }
}
