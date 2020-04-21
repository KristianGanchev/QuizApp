using System;
using System.Collections.Generic;
using System.Text;

namespace Quizler.Web.Shared.Models.Questions
{
    public class QuestionRequest
    {
        public string Text { get; set; }

        public int Points { get; set; }

        public int QuizId { get; set; }
    }
}
