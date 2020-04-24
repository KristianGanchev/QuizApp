using System;

namespace Quizler.Web.Client.Services
{
    public class AppState : IAppState
    {
        public event Action RefreshRequested;

        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
