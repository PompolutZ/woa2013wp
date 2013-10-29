using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace WackenOpenAir.Utilities
{
    public class LocalizedAppBar
    {
        private ApplicationBar _applicationBar;

        public LocalizedAppBar()
        {
            _applicationBar = BuildDefaultApplicationBar();
        }

        public ApplicationBar AppBar
        {
            get { return _applicationBar; }
        }

        private ApplicationBar BuildDefaultApplicationBar()
        {
            _applicationBar = new ApplicationBar();
            _applicationBar.IsVisible = true;
            _applicationBar.Mode = ApplicationBarMode.Minimized;
            _applicationBar.IsMenuEnabled = true;
            _applicationBar.BackgroundColor = Color.FromArgb(255, 219, 150, 24);
            _applicationBar.ForegroundColor = Color.FromArgb(255, 224, 224, 224);

            return _applicationBar;
        }
    }

    public static class LocalizedAppBarExtensions
    {
        public static LocalizedAppBar WithRateAndReviewItem(this LocalizedAppBar localizedAppBar)
        {
            var rateAndReviewAppBarMenuItem = new ApplicationBarMenuItem(AppResources.Global_RateAndReview);
            rateAndReviewAppBarMenuItem.Click += (sender, args) =>
                Application.Current.RootVisual.Dispatcher.BeginInvoke(new MarketplaceReviewTask().Show);
            localizedAppBar.AppBar.MenuItems.Add(rateAndReviewAppBarMenuItem);

            return localizedAppBar;
        }

        public static LocalizedAppBar WithAboutItem(this LocalizedAppBar localizedAppBar)
        {
            var aboutPageAppBarMenuItem = new ApplicationBarMenuItem(AppResources.Global_About);
            aboutPageAppBarMenuItem.Click += (sender, agrs) => 
                ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(@"/View/AboutPage.xaml", UriKind.Relative));

            localizedAppBar.AppBar.MenuItems.Add(aboutPageAppBarMenuItem);
            
            return localizedAppBar;
        }
    }
}