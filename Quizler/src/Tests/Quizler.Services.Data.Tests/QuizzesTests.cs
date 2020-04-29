namespace Quizler.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Quizler.Data;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Data.Repositories;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Questions;
    using Quizler.Web.Shared.Models.Quizzes;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class QuizzesTests
    {
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly QuizzesService service;

        public QuizzesTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            this.answerRepository = new EfDeletableEntityRepository<Answer>(dbContext);
            this.questionRepository = new EfDeletableEntityRepository<Question>(dbContext);
            this.categoryRepository = new EfDeletableEntityRepository<Category>(dbContext);
            this.quizRepository = new EfDeletableEntityRepository<Quiz>(dbContext);
            this.service = new QuizzesService(quizRepository, questionRepository, answerRepository);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateQuizSuccesfully()
        {
            var firstQuizId = await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            var secondQuizId = await this.service.CreateAsync("Second Quiz", 3, new ApplicationUser(), null);

            Assert.Equal(1, firstQuizId);
            Assert.Equal(2, secondQuizId);
        }

        [Fact]
        public async Task UpdateAsycShouldUpdateQuizSuccesfully()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);

            await this.service.UpdateAsync("Updated Quiz", 3, "picture", 1);

            var quiz = this.quizRepository.All().First();

            Assert.Equal("Updated Quiz", quiz.Name);
            Assert.Equal(3, quiz.CategoryId);
            Assert.Equal("picture", quiz.ImageUrl);
            Assert.Equal(1, quiz.Id);
        }

        [Fact]
        public async Task UpdateAsycShouldthrowNullReferenceException()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.UpdateAsync("Updated Quiz", 3, "picture", 2));
        }

        [Fact]
        public async Task GetAllShouldReturnAllQuizzes()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Thrid Quiz", 1, new ApplicationUser(), null);


            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.GetAll<QuizResponse>().ToList();

            Assert.Equal(3, quizzes.Count());
        }

        [Fact]
        public void GetAllShouldReturnEmptyList()
        {
            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.GetAll<QuizResponse>().ToList();

            Assert.Empty(quizzes);
        }

        [Fact]
        public async Task GetByIdShoudReturnValidQuiz()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);


            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var firstQuiz = this.service.GetById<QuizResponse>(1);
            var secondQuiz = this.service.GetById<QuizResponse>(2);

            Assert.Equal("First Quiz", firstQuiz.Name);
            Assert.Equal(1, firstQuiz.Id);
            Assert.Equal("Second Quiz", secondQuiz.Name);
            Assert.Equal(2, secondQuiz.Id);
        }

        [Fact]
        public void GetByIdShoudReturnNull()
        {
            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quiz = this.service.GetById<QuizResponse>(1);

            Assert.Null(quiz);
        }

        [Fact]
        public async Task GetByCategoryShouldReturnValidQuizzes()
        {
            await this.categoryRepository.AddAsync(new Category { Name = "Movies" });

            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Third Quiz", 1, new ApplicationUser(), null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.GetByCategory<QuizResponse>("Movies");

            Assert.Equal(3, quizzes.Count());
        }

        [Fact]
        public async Task GetByCategoryShouldReturnEmptyList()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Third Quiz", 1, new ApplicationUser(), null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.GetByCategory<QuizResponse>("Movies");

            Assert.Empty(quizzes);
        }

        [Fact]
        public async Task GetByUserShouldReturnValidQuizzes()
        {
            var user = new ApplicationUser { Id = "user" };

            await this.service.CreateAsync("First Quiz", 1, user, null);
            await this.service.CreateAsync("Second Quiz", 1, user, null);
            await this.service.CreateAsync("Third Quiz", 1, user, null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.GetAllByUser<QuizResponse>("user");

            Assert.Equal(3, quizzes.Count());
        }

        [Fact]
        public async Task GetByUserShouldReturnEmptyList()
        {
            var user = new ApplicationUser { Id = "user" };

            await this.service.CreateAsync("First Quiz", 1, user, null);
            await this.service.CreateAsync("Second Quiz", 1, user, null);
            await this.service.CreateAsync("Third Quiz", 1, user, null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.GetAllByUser<QuizResponse>("fake user");

            Assert.Empty(quizzes);
        }

        [Fact]
        public async Task SearchShouldReturnOneMatch()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Third Quiz", 1, new ApplicationUser(), null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.Search<QuizResponse>("First");

            Assert.Single(quizzes);
            Assert.Equal("First Quiz", quizzes.First().Name);
        }

        [Fact]
        public async Task SearchShouldReturnMoreThanOneMatch()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Third Quiz", 1, new ApplicationUser(), null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.Search<QuizResponse>("Quiz");

            Assert.Equal(3, quizzes.Count());
        }

        [Fact]
        public async Task SearchShouldReturnNoeMatches()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Third Quiz", 1, new ApplicationUser(), null);

            AutoMapperConfig.RegisterMappings(typeof(QuizResponse).GetTypeInfo().Assembly);

            var quizzes = this.service.Search<QuizResponse>("a");

            Assert.Empty(quizzes);
        }

        [Fact]
        public async Task DeleteQuizShouldDeleteQuizSuccesfully()
        {
            await this.service.CreateAsync("First Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Second Quiz", 1, new ApplicationUser(), null);
            await this.service.CreateAsync("Third Quiz", 1, new ApplicationUser(), null);

            await this.service.DeleteAsync(2);

            Assert.Equal(2, this.quizRepository.All().Count());
            Assert.Equal(1, this.quizRepository.All().First().Id);
            Assert.Equal(3, this.quizRepository.All().Last().Id);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }
    }
}
