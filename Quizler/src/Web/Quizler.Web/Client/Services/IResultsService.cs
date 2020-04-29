using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizler.Web.Client.Services
{
    public interface IResultsService
    {
        Task<TResponse> CreateAsync<TResponse, TRequiest>(TRequiest model);

        Task<T> GetById<T>(int resultId, string route);

        Task<T> GetMyResults<T>();

        Task DeleteAsync(int resultId);
    }
}
