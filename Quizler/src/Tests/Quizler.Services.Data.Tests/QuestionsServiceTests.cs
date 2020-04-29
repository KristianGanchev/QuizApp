namespace Quizler.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Quizler.Data;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Data.Repositories;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Questions;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class QuestionsServiceTests
    {
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly QuestionsServices service;

        public QuestionsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            this.answerRepository = new EfDeletableEntityRepository<Answer>(dbContext);
            this.questionRepository = new EfDeletableEntityRepository<Question>(dbContext);
            this.quizRepository = new EfDeletableEntityRepository<Quiz>(dbContext);
            this.service = new QuestionsServices(questionRepository ,answerRepository);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateQuestionSuccesfully()
        {
            var firstQuestionId = await this.service.CreateAsync("First Question", 10, 1);
            var secondQuestionId = await this.service.CreateAsync("Second Question", 20, 1);

            Assert.Equal(1, firstQuestionId);
            Assert.Equal(2, secondQuestionId);
        }

        [Fact]
        public async Task UpdateAsycShouldUpdateQuestionSuccesfully()
        {
            await this.service.CreateAsync("First Question", 10, 1);
            await this.service.UpdateAsync("Updated Question", 20, 1);

            AutoMapperConfig.RegisterMappings(typeof(QuestionResponse).GetTypeInfo().Assembly);

            var question = this.service.GetAll<QuestionResponse>(1).ToList().First();

            Assert.Equal("Updated Question", question.Text);
            Assert.Equal(20, question.Points);
            Assert.Equal(1, question.Id);
        }

        [Fact]
        public async Task UpdateAsycShouldthrowNullReferenceException()
        {
            await this.service.CreateAsync("First Question", 10, 1);

            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.UpdateAsync("Updated Question", 20, 2));
        }

        [Fact]
        public async Task GetAllByQuestionIdShouldReturnAllQuestions()
        {
            await this.service.CreateAsync("First Question", 10, 1);
            await this.service.CreateAsync("Second Question", 10, 1);
            await this.service.CreateAsync("Third Question", 10, 1);

            AutoMapperConfig.RegisterMappings(typeof(QuestionResponse).GetTypeInfo().Assembly);

            var questions = this.service.GetAll<QuestionResponse>(1).ToList();

            Assert.Equal(3, questions.Count());
        }

        [Fact]
        public async Task GetAllByQuestionIdShouldEmptyList()
        {
            await this.service.CreateAsync("First Question", 10, 1);
            await this.service.CreateAsync("Second Question", 10, 1);
            await this.service.CreateAsync("Third Question", 10, 1);

            AutoMapperConfig.RegisterMappings(typeof(QuestionResponse).GetTypeInfo().Assembly);

            var questions = this.service.GetAll<QuestionResponse>(2).ToList();

            Assert.Empty(questions);
        }

        [Fact]
        public async Task GetByQuizIdShoudReturnValidQuestion()
        {
            await this.quizRepository.AddAsync(
                new Quiz
                {
                    Id = 1,
                });

            await this.service.CreateAsync("First Question", 10, 1);

            AutoMapperConfig.RegisterMappings(typeof(QuestionResponse).GetTypeInfo().Assembly);

            var question = this.service.GetByQuizId<QuestionResponse>(1);

            Assert.Equal("First Question", question.Text);
            Assert.Equal(10, question.Points);
            Assert.Equal(1, question.Id);
        }

        [Fact]
        public async Task GetByQuizIdShoudReturnNull()
        {
            await this.quizRepository.AddAsync(
                new Quiz
                {
                    Id = 1,
                });

            await this.service.CreateAsync("First Question", 10, 1);

            AutoMapperConfig.RegisterMappings(typeof(QuestionResponse).GetTypeInfo().Assembly);

            var question = this.service.GetByQuizId<QuestionResponse>(2);

            Assert.Null(question);
        }

        [Fact]
        public async Task GetIdShouldReturnValidId()
        {
            await this.quizRepository.AddAsync(
                new Quiz
                {
                    Id = 1,
                });

            await this.service.CreateAsync("First Question", 10, 1);

            var questionId = this.service.GetId("First Question", 1);

            Assert.Equal(1, questionId);
        }

        [Fact]
        public async Task GetIdShouldThrowNullReferenceExceptionWithInvalidQuizId()
        {
            await this.quizRepository.AddAsync(
                new Quiz
                {
                    Id = 1,
                });

            await this.service.CreateAsync("First Question", 10, 1);

            Assert.Throws<NullReferenceException>(() => this.service.GetId("First Question", 2));
        }

        [Fact]
        public async Task GetIdShouldThrowNullReferenceExceptionWithInvalidQuestionName()
        {
            await this.quizRepository.AddAsync(
                new Quiz
                {
                    Id = 1,
                });

            await this.service.CreateAsync("First Question", 10, 1);

            Assert.Throws<NullReferenceException>(() => this.service.GetId("No Question", 1));
        }

        [Fact]
        public async Task DeleteAsyncWithOneEntityShouldDeleteQuestionSuccesfully()
        {
            await this.answerRepository.AddAsync(new Answer { QuestionId = 1});
            await this.answerRepository.AddAsync(new Answer { QuestionId = 1});
            await this.answerRepository.AddAsync(new Answer { QuestionId = 1});
          
            await this.service.CreateAsync("First Question", 10, 1);

            await this.service.DeleteAsync(1);

            Assert.Equal(0, this.answerRepository.All().Count());
            Assert.Equal(0, this.questionRepository.All().Count());
        }

        [Fact]
        public async Task DeleteAsyncWithMoreThanOneShouldDeleteQuestionSuccesfully()
        {
            await this.answerRepository.AddAsync(new Answer { QuestionId = 1 });
            await this.answerRepository.AddAsync(new Answer { QuestionId = 1 });
            await this.answerRepository.AddAsync(new Answer { QuestionId = 1 });

            await this.service.CreateAsync("First Question", 10, 1);

            await this.answerRepository.AddAsync(new Answer { QuestionId = 2 });
            await this.answerRepository.AddAsync(new Answer { QuestionId = 2 });
            await this.answerRepository.AddAsync(new Answer { QuestionId = 2 });

            await this.service.CreateAsync("Second Question", 10, 1);

            await this.service.DeleteAsync(2);

            Assert.Equal(3, this.answerRepository.All().Count());
            Assert.Equal(1, this.questionRepository.All().Count());
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }
    }
}
