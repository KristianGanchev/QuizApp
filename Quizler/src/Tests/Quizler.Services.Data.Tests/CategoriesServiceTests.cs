namespace Quizler.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Quizler.Data;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Data.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;
    using System;
    using Quizler.Web.Shared.Models.Categories;
    using Quizler.Services.Mapping;
    using System.Reflection;

    public class CategoriesServiceTests
    {
        private readonly IDeletableEntityRepository<Category> repository;
        private readonly CategoriesService service;

        public CategoriesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            this.repository = new EfDeletableEntityRepository<Category>(dbContext);
            this.service = new CategoriesService(repository);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateSuccesfully()
        {
            var firstId = await this.service.CreateAsync("Movies");
            var secondId = await this.service.CreateAsync("Games");

            Assert.Equal(1, firstId);
            Assert.Equal(2, secondId);
        }

        [Fact]
        public async Task GetAllShouldReturnAllCategories() 
        {
            await this.service.CreateAsync("Movies");
            await this.service.CreateAsync("Games");
            await this.service.CreateAsync("Sports");

            AutoMapperConfig.RegisterMappings(typeof(CategorieResponse).GetTypeInfo().Assembly);

            var categories = this.service.GetAll<CategorieResponse>();

            Assert.Equal(3, categories.Count());
        }

        [Fact]
        public async Task GetByNameShouldReturnValidCategorie()
        {
            await this.service.CreateAsync("Movies");

            AutoMapperConfig.RegisterMappings(typeof(CategorieResponse).GetTypeInfo().Assembly);

            var categorie = this.service.GetByName<CategorieResponse>("Movies");

            Assert.Equal("Movies", categorie.Name);
        }

        [Fact]
        public void GetByNameShouldReturnNull()
        {

            AutoMapperConfig.RegisterMappings(typeof(CategorieResponse).GetTypeInfo().Assembly);

            var categorie = this.service.GetByName<CategorieResponse>("Movies");

            Assert.Null(categorie);
        }
    }
}
