using Quizler.Data.Models;
using Quizler.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quzler.Web.Shared.Categories
{
    public class CategorieResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
