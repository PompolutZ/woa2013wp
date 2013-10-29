using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;
using WackenOpenAir.Core;
using WackenOpenAir.Services;

namespace WackenOpenAir.ViewModels
{
    public class NewsFeedUserControlViewModel : PropertyChangedBase
    {
        private readonly NewsService _newsService;
        
        private bool _internetConnectionProblem;

        public NewsFeedUserControlViewModel(NewsService newsService)
        {
            _newsService = newsService;
            NewsFeed = new ObservableCollection<NewsFeedItem>();
        }

        public ObservableCollection<NewsFeedItem> NewsFeed { get; private set; }

        public bool InternetConnectionProblem
        {
            get { return _internetConnectionProblem; }
            set
            {
                _internetConnectionProblem = value;
                NotifyOfPropertyChange(() => InternetConnectionProblem);
            }
        }

        public async Task LoadNews()
        {
            var newsFeed = await _newsService.LoadNewsAsync();

            NewsFeed.Clear();
            if (newsFeed.IsSome)
                foreach (var newsFeedItem in newsFeed.Value)
                    NewsFeed.Add(newsFeedItem);
            else
                InternetConnectionProblem = true;
        }

        public void NavigateToNewsDetails(NewsFeedItem newsFeedItem)
        {
            var webBrowserTask = new WebBrowserTask
            {
                Uri = new Uri(newsFeedItem.Link, UriKind.Absolute)
            };

            webBrowserTask.Show();
        }
    }
}