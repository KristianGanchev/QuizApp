namespace Quizler.Services.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Quizler.Data;
    using Quizler.Data.Common.Repositories;
    using Quizler.Data.Models;
    using Quizler.Data.Repositories;
    using Quizler.Services.Mapping;
    using Quizler.Web.Shared.Models.Answers;
    using Quizler.Web.Shared.Models.Results;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class ResultsServiceTests
    {
        private readonly IDeletableEntityRepository<Quiz> quizRepository;
        private readonly IRepository<AnswerResult> answerResultRepository;
        private readonly IDeletableEntityRepository<Result> resultRepository;
        private readonly ResultService service;

        public ResultsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            this.answerResultRepository = new EfRepository<AnswerResult>(dbContext);
            this.resultRepository = new EfDeletableEntityRepository<Result>(dbContext);
            this.quizRepository = new EfDeletableEntityRepository<Quiz>(dbContext);
            this.service = new ResultService(resultRepository, quizRepository, answerResultRepository);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateResultSuccesfully()
        {
            await this.quizRepository.AddAsync(new Quiz());

            var answers = new List<AnswerResponse>
            {
                new AnswerResponse(),
                new AnswerResponse(),
                new AnswerResponse()
            };

            var firstResultId = await this.service.CreateAync(10, 10, "student", 1, answers);
            var secondResultId = await this.service.CreateAync(10, 10, "student", 1, answers);
            var thirdResultId = await this.service.CreateAync(10, 10, "student", 1, answers);

            Assert.Equal(1, firstResultId);
            Assert.Equal(2, secondResultId);
            Assert.Equal(3, thirdResultId);
        }

        [Fact]
        public async Task CreateAsyncShouldIncreaseQuizPlaysCorrectly()
        {
            await this.quizRepository.AddAsync(new Quiz());

            var answers = new List<AnswerResponse>
            {
                new AnswerResponse(),
                new AnswerResponse(),
                new AnswerResponse()
            };

            await this.service.CreateAync(10, 10, "student", 1, answers);
            await this.service.CreateAync(10, 10, "student", 1, answers);
            await this.service.CreateAync(10, 10, "student", 1, answers);

            Assert.Equal(3, this.quizRepository.All().First().Plays);
        }

        [Fact]
        public async Task GetByIdShouldReturnValidResult()
        {
            await this.quizRepository.AddAsync(new Quiz());

            await this.service.CreateAync(10, 10, "student", 1, new List<AnswerResponse>());
            await this.service.CreateAync(20, 20, "new student", 1, new List<AnswerResponse>());

            AutoMapperConfig.RegisterMappings(typeof(ResultResponse).GetTypeInfo().Assembly);

            var firstResult = this.service.GetById<ResultResponse>(1);
            var secondResult = this.service.GetById<ResultResponse>(2);

            Assert.Equal(1, firstResult.Id);
            Assert.Equal(2, secondResult.Id);
        }

        [Fact]
        public void GetByIdShouldReturnNull()
        {
            AutoMapperConfig.RegisterMappings(typeof(ResultResponse).GetTypeInfo().Assembly);

            var result = this.service.GetById<ResultResponse>(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByUserAndQuizIdShouldReturnCorrect()
        {
            await this.quizRepository.AddAsync(new Quiz());
            await this.quizRepository.AddAsync(new Quiz());

            await this.service.CreateAync(10, 10, "student", 1, new List<AnswerResponse>());
            await this.service.CreateAync(10, 10, "new student", 2, new List<AnswerResponse>());

            AutoMapperConfig.RegisterMappings(typeof(ResultResponse).GetTypeInfo().Assembly);

            var firstResult = this.service.GetByUserAndQuizId<ResultResponse>("student", 1);
            var secondResult = this.service.GetByUserAndQuizId<ResultResponse>("new student", 2);

            Assert.Equal(1, firstResult.Id);
            Assert.Equal(2, secondResult.Id);
        }


        [Fact]
        public void GetByUserAndQuizIdShouldReturnNull()
        {
            AutoMapperConfig.RegisterMappings(typeof(ResultResponse).GetTypeInfo().Assembly);

            var result = this.service.GetByUserAndQuizId<ResultResponse>("batman", 100);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteResultShouldDeleteResultSuccesfully()
        {
            await this.quizRepository.AddAsync(new Quiz());

            var firstResultId = await this.service.CreateAync(10, 10, "student", 1, new List<AnswerResponse>());
            var secondResultId = await this.service.CreateAync(20, 20, "new student", 1, new List<AnswerResponse>());

            await this.service.DeleteAsync(firstResultId);

            Assert.Equal(1, this.resultRepository.All().Count());
            Assert.Equal(2, this.resultRepository.All().First().Id);
        }

        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => this.service.DeleteAsync(1));
        }
    }
}
