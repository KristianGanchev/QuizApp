namespace Quizler.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Quizler.Data;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Data.Repositories;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Answers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class AnswersServiceTests
    {
        private readonly IDeletableEntityRepository<Answer> answerRepository;
        private readonly IDeletableEntityRepository<Question> questionRepository;
        private readonly AnswersServices service;

        public AnswersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            this.answerRepository = new EfDeletableEntityRepository<Answer>(dbContext);
            this.questionRepository = new EfDeletableEntityRepository<Question>(dbContext);
            this.service = new AnswersServices(answerRepository);
        }


        [Fact]
        public async Task CreateAsyncShouldCreateAnswerSuccesfully()
        {
            var firstAnswerId = await this.service.CreateAync("First Answer", true, 1);
            var secondAnswerId = await this.service.CreateAync("Second Answer", true, 2);

            Assert.Equal(1, firstAnswerId);
            Assert.Equal(2, secondAnswerId);
        }

        [Fact]
        public async Task GetAllByQuestionIdShouldReturnAllAnswers()
        {
            await this.service.CreateAync("First Answer", true, 1); ;
            await this.service.CreateAync("Second Answer", true, 1);
            await this.service.CreateAync("Third Answer", true, 1);

            AutoMapperConfig.RegisterMappings(typeof(AnswerResponse).GetTypeInfo().Assembly);

            var answers = this.service.GetAll<AnswerResponse>(1).ToList();

            Assert.Equal(3, answers.Count());
        }


        [Fact]
        public async Task GetAllByQuestionIdShouldEmptyList()
        {
            await this.service.CreateAync("First Answer", true, 1);
            await this.service.CreateAync("Second Answer", true, 1);
            await this.service.CreateAync("Third Answer", true, 1);

            AutoMapperConfig.RegisterMappings(typeof(AnswerResponse).GetTypeInfo().Assembly);

            var answers = this.service.GetAll<AnswerResponse>(2).ToList();

            Assert.Empty(answers);
        }

        [Fact]
        public async Task UpdateAsycShouldUpdateAnswerSuccesfully()
        {
            await this.service.CreateAync("Answer", false, 1);
            await this.service.UpdateAsync("Updated Answer", true, 1);

            AutoMapperConfig.RegisterMappings(typeof(AnswerResponse).GetTypeInfo().Assembly);

            var answer = this.service.GetAll<AnswerResponse>(1).ToList().First();

            Assert.Equal("Updated Answer", answer.Text);
            Assert.True(answer.IsCorrect);
            Assert.Equal(1, answer.Id);
        }

        [Fact]
        public async Task UpdateAsycShouldthrowNullReferenceException()
        {
            await this.service.CreateAync("Answer", false, 1);
             
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.UpdateAsync("Updated Answer", true, 2));
        }

        [Fact]
        public async Task GetByQuestionIdShoudReturnValidAndwer() 
        {
            await this.questionRepository.AddAsync(
                new Question
                {
                    Id = 1,
                    QuizId = 1
                });

            await this.service.CreateAync("Answer", false, 1);

            AutoMapperConfig.RegisterMappings(typeof(AnswerResponse).GetTypeInfo().Assembly);

            var answer = this.service.GetByQuestionId<AnswerResponse>(1);

            Assert.Equal("Answer", answer.Text);
            Assert.False(answer.IsCorrect);
        }


        [Fact]
        public async Task GetByQuestionIdShoudReturnNull()
        {
            await this.questionRepository.AddAsync(
                new Question
                {
                    Id = 1,
                    QuizId = 1
                });

            await this.service.CreateAync("Answer", false, 1);

            AutoMapperConfig.RegisterMappings(typeof(AnswerResponse).GetTypeInfo().Assembly);

            var answer = this.service.GetByQuestionId<AnswerResponse>(2);

            Assert.Null(answer);
        }
    }
}
