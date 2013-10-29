using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using Microsoft.Phone.Tasks;
using WackenDataAndUtilities.Data;
using WackenDataAndUtilities.Lastfm;
using WackenOpenAir.Core;
using WackenOpenAir.Services;

namespace WackenOpenAir.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly LineupService _lineupService;

        private ObservableCollection<Artist> _lineup;
        
        public MainPageViewModel(INavigationService navigationService, NewsFeedUserControlViewModel newsFeedViewModel, LineupService lineupService)
        {
            NewsFeedViewModel = newsFeedViewModel;
            _navigationService = navigationService;
            _lineupService = lineupService;
            //Lineup = new ObservableCollection<Artist>();
            //LoadLineup();
        }

        private void LoadLineup()
        {
            try
            {
                ArtistsInfo.Artists.ForEach(LoadArtist);
            }
            catch (Exception)
            {
            }
        }

        public NewsFeedUserControlViewModel NewsFeedViewModel { get; private set; }

        public ObservableCollection<Artist> Lineup
        {
            get
            {
                return _lineup;
            }
            set
            {
                _lineup = value;
                NotifyOfPropertyChange(() => Lineup);
            }
        }

        public async Task RefreshNewsFeed()
        {
            await NewsFeedViewModel.LoadNews();
        }

        public void NavigateToNewsDetails(NewsFeedItem newsFeedItem)
        {
            NewsFeedViewModel.NavigateToNewsDetails(newsFeedItem);
        }

        protected override async void OnActivate()
        {
            await NewsFeedViewModel.LoadNews();
            await _lineupService.LoadLineup();
        }

        private void LoadArtist(Artist artist)
        {
            try
            {
                Session.LoadArtist(artist.LastFmName, a =>
                {
                    try
                    {
                        artist.ImageUrl = a.ImageUrl;
                        artist.Url = a.Url;
                        artist.Id = a.Id;
                        Lineup.Add(artist);
                    }
                    catch (NullReferenceException)
                    {
                        //InternetConnectionProblem = true;
                    }
                });
            }
            catch (Exception ex)
            {
                //InternetConnectionProblem = true;
            }
        }

        public void NavigateToAboutPage()
        {
            _navigationService.UriFor<AboutPageViewModel>().Navigate();
        }
    }
}