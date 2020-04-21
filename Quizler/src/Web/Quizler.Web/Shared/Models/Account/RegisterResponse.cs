using System;
using System.Collections.Generic;
using System.Text;

namespace Quizler.Web.Shared.Models.Account
{
    public class RegisterResponseModel
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
