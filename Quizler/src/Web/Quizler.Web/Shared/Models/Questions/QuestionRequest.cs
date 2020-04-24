using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quizler.Web.Shared.Models.Questions
{
    public class QuestionRequest
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int Points { get; set; }

        public int QuizId { get; set; }
    }
}
