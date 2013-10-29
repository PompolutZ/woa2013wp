using System;
using System.Windows.Controls;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using WackenOpenAir.Utilities;
using WackenOpenAir.ViewModel;

namespace WackenOpenAir.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly MainPageViewModel _viewModel;
        
        public MainPage()
        {
            InitializeComponent();
            
            _viewModel = new MainPageViewModel();
            DataContext = _viewModel;
            Loaded += (sender, args) => Dispatcher.BeginInvoke(_viewModel.LoadNews);
        
            ApplicationBar = 
                new LocalizedAppBar()
                .WithRateAndReviewItem()
                .WithAboutItem()
                .AppBar;
        }

        private void NewsFeedSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.NavigateToNewsDetails(NewsFeed.SelectedIndex);
        }
    }
}