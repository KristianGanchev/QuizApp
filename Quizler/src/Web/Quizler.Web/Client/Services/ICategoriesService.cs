using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public interface ICategoriesService
    {
        Task<T> GetAll<T>(string route);
    }
}
