using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using Quizler.Web.Client.Services;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazor.FileReader;

namespace Quizler.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore(options => options.AddPolicy("AdminOnly", polici => polici.RequireClaim("role", "Administrator")));
            builder.Services.AddBlazoredModal();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IQuizzesService, QuizzesService>();
            builder.Services.AddScoped<IQuestionsService, QuestionsService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IResultsService, ResultsServices>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);

            await builder.Build().RunAsync();
        }
    }
}
