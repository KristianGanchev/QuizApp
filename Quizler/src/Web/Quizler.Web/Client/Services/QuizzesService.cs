namespace Quizler.Web.Client.Services
{
    using Microsoft.AspNetCore.Components;
    using Quizler.Data.Models;
    using Quizler.Web.Shared.Models.Answers;
    using Quizler.Web.Shared.Models.Questions;
    using Quizler.Web.Shared.Models.Quizzes;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class QuizzesService : IQuizzesService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public QuizzesService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task CreateAsync(QuizCreateRequest Quiz)
        {
            if (Quiz.Questions.Count >= 4)
            {
                var quizResponse = await this.httpClient.PostJsonAsync<QuizResponse>("quizzes/create", Quiz);


                if (quizResponse == null)
                {
                    this.navigationManager.NavigateTo("error");
                }

                foreach (var question in Quiz.Questions)
                {
                    question.QuizId = quizResponse.Id;
                    var questionResponse = await this.httpClient.PostJsonAsync<QuestionResponse>("questions/create", question);

                    if (questionResponse == null)
                    {
                        this.navigationManager.NavigateTo("error");
                    }

                    foreach (var answer in question.Answers)
                    {
                        answer.QuestionId = questionResponse.Id;
                        var answerResponse = await this.httpClient.PostJsonAsync<AnswerResponse>("answers/create", answer);

                        if (answerResponse == null)
                        {
                            this.navigationManager.NavigateTo("error");
                        }
                    }



                }

                this.navigationManager.NavigateTo($"quizzes/my-quizzes");
            }
        }

        public async Task UpdateAsync(QuizEditResponse quiz)
        {
            var quizResponse = await httpClient.PostJsonAsync<QuizResponse>("quizzes/update", quiz);

            if (quizResponse == null)
            {
                navigationManager.NavigateTo("error");
            }

            navigationManager.NavigateTo($"quizzes/details/{quizResponse.Id}");
        }

        public async Task DeleteAsync(int quizId)
        {
            await this.httpClient.DeleteAsync($"quizzes/delete/{quizId}");
            this.navigationManager.NavigateTo("quizzes/my-quizzes");
        }

        public async Task<T> GetById<T>(int quizId, string rout)
        {
            var quiz = await this.httpClient.GetJsonAsync<T>($"{rout}{quizId}");

            return quiz;
        }

        public async Task<T> Search<T>(string searchQuery)
        {
            var quizes = await this.httpClient.GetJsonAsync<T>($"quizzes/search/{searchQuery}"); 

            return quizes;
        }
        public async Task<T> GetAllByUser<T>()
        {
            var quizzes = await this.httpClient.GetJsonAsync<T>($"quizzes/myquizzes/");

            return quizzes;
        }
    }
}
