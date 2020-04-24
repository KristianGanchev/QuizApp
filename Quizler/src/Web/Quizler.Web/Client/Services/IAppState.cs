namespace Quizler.Web.Client.Services
{
    using System;

    public interface IAppState
    {
        event Action RefreshRequested;

        void CallRequestRefresh();
    }
}