using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Xml;
using GalaSoft.MvvmLight;
using Microsoft.Phone.Tasks;
using WackenDataAndUtilities.Data;
using WackenDataAndUtilities.Lastfm;
using WackenDataAndUtilities.Utilities;

namespace WackenOpenAir.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private const string GermanCultureName = "de-DE";
        private ObservableCollection<NewsFeedViewModel> _allNews;
        private ObservableCollection<Artist> _lineup; 

        public MainPageViewModel()
        {
            FeedItems = new ObservableCollection<NewsFeedViewModel>();
            Lineup = new ObservableCollection<Artist>();
            LoadLineup();
        }

        private void LoadLineup()
        {
            ArtistsInfo.Artists.ForEach(LoadArtist);
        }

        public event EventHandler ErrorOccured;

        public ObservableCollection<NewsFeedViewModel> FeedItems
        {
            get
            {
                return _allNews;
            }
            set
            {
                _allNews = value;
                RaisePropertyChanged(() => FeedItems);
            }
        }

        public ObservableCollection<Artist> Lineup
        {
            get
            {
                return _lineup;
            }
            set
            {
                _lineup = value;
                RaisePropertyChanged(() => Lineup);
            }
        }

        public void LoadNews()
        {
            var webclient = new WebClient();
            webclient.DownloadStringCompleted += NewsFeedDownloaded;

            var cultureInfo = Thread.CurrentThread.CurrentUICulture;
            if(cultureInfo.Name == GermanCultureName)
                webclient.DownloadStringAsync(new Uri(@"http://www.wacken.com/de/wacken2012/main-news/rss/rss.xml"));
            else
                webclient.DownloadStringAsync(new Uri(@"http://www.wacken.com/en/wacken2012/main-news/rss/rss.xml"));
        }

        public void NavigateToNewsDetails(int newsIndex)
        {
            var webBrowserTask = new WebBrowserTask
                {
                    Uri = new Uri(FeedItems[newsIndex].NewsItemLink, UriKind.Absolute)
                };

            webBrowserTask.Show();
        }

        private void NewsFeedDownloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                EventRaiser.RiseWithEmptyArgs(ErrorOccured, this);
                return;
            }

            if (string.IsNullOrEmpty(e.Result))
            {
                return;
            }

            XmlReader xmlReader = XmlReader.Create(new StringReader(e.Result));
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            foreach (SyndicationItem sItem in feed.Items)
            {
                FeedItems.Add(
                    new NewsFeedViewModel
                        {
                            NewsItemTitle = sItem.IfNotNull(item => item.Title.Text),
                            NewsItemPublishDate = sItem.IfNotNull(item => item.PublishDate.Date),
                            NewsItemDetails = sItem.IfNotNull(item => item.Summary.Text),
                            NewsItemLink = sItem.IfNotNull(item => item.Links[0].Uri.ToString())
                        });
            }
        }

        private void LoadArtist(Artist artist)
        {
            Session.LoadArtist(artist.LastFmName, a =>
            {
                artist.ImageUrl = a.ImageUrl;
                artist.Url = a.Url;
                artist.Id = a.Id;
                Lineup.Add(artist);
            });
        }
    }
}