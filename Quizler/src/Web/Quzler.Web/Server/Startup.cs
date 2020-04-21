<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Startup.cs
namespace Quizler.Web.Server
=======
namespace Quzler.Web.Server
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Startup.cs
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Quizler.Data;
    using Quizler.Data.Models;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Repositories;
    using Quizler.Data.Common;
    using Quizler.Data.Seeding;
<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Startup.cs
    using Quizler.Web.Server.Authentication;
    using Quizler.Services.Data;
    using Quizler.Web.Shared.Models.Common;
    using Quizler.Services.Mapping;
    using System.Reflection;
=======
    using Quizler.Services.Mapping;
    using Quzler.Web.Shared.Quizzes;
    using System.Reflection;
    using Quizler.Services.Data;
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Startup.cs

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Startup.cs
            // Application services
            services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();
=======
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Startup.cs
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IQuizzesService, QuizzesService>();
            services.AddTransient<IQuestionsServices, QuestionsServices>();
            services.AddTransient<IAnswersServices, AnswersServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Startup.cs
                AutoMapperConfig.RegisterMappings(typeof(BadRequestModel).GetTypeInfo().Assembly);
=======
                AutoMapperConfig.RegisterMappings(typeof(QuizCreateRequestModel).GetTypeInfo().Assembly);
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Startup.cs

                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
