using Microsoft.AspNetCore.Components;
using Quizler.Web.Shared.Models.Answers;
using Quizler.Web.Shared.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public QuestionsService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task UpdateAsync(QuestionEditResponse question) 
        {
           var questionResponse = await httpClient.PostJsonAsync<QuestionResponse>("questions/update", question);

            foreach (var answer in question.Answers)
            {
                var answerResponse = await httpClient.PostJsonAsync<AnswerResponse>("answers/update", answer);

                if (answerResponse == null)
                {
                    navigationManager.NavigateTo("error");
                }
            }

            if (questionResponse == null)
            {
                navigationManager.NavigateTo("error");
            }
        }

        public async Task<T> GetAllByQuizId<T>(int quizId)
        {
            var questions = await this.httpClient.GetJsonAsync<T>($"questions/All/{quizId}");

            return questions;
        }

        public async Task DeleteAsync(string questionTitle, int quizId) 
        {
            int questionId = await httpClient.GetJsonAsync<int>($"questions/?questionName={questionTitle}&quizId={quizId}");
            await httpClient.DeleteAsync($"questions/delete/{questionId}");
        }
    }
}
