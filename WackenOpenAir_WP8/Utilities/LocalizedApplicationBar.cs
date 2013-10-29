using System;
using System.Windows.Media;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace WackenOpenAir.Utilities
{
    public class LocalizedApplicationBar
    {
        private readonly ApplicationBar _applicationBar;

        public LocalizedApplicationBar()
        {
            _applicationBar = new ApplicationBar();
        }

        public LocalizedApplicationBar WithBackground(Color backgroundColor)
        {
            _applicationBar.BackgroundColor = backgroundColor;
            return this;
        }

        public LocalizedApplicationBar WithForeground(Color foregroundColor)
        {
            _applicationBar.ForegroundColor = foregroundColor;
            return this;
        }

        public LocalizedApplicationBar AsVisible()
        {
            _applicationBar.IsVisible = true;
            return this;
        }

        public LocalizedApplicationBar AsInvisible()
        {
            _applicationBar.IsVisible = false;
            return this;
        }

        public LocalizedApplicationBar WithOpacity(double opacity)
        {
            _applicationBar.Opacity = opacity;
            return this;
        }

        public LocalizedApplicationBar AsMinimized()
        {
            _applicationBar.Mode = ApplicationBarMode.Minimized;
            return this;
        }

        public LocalizedApplicationBar WithRateAndReviewMenu()
        {
            return WithMenuItem(
                () => new MarketplaceReviewTask().Show(),
                AppResources.Global_RateAndReview);
        }

        //public LocalizedApplicationBar WithSettingsMenu(Action doOnClick)
        //{
        //    return WithMenuItem(doOnClick, AppResources.ApplicationBar_SettingsPage);
        //}

        public LocalizedApplicationBar WithAboutMenu(Action doOnClick)
        {
            return WithMenuItem(doOnClick, AppResources.Global_About);
        }

        public LocalizedApplicationBar WithMenuItem(Action doOnClick, string text)
        {
            var appBarMenuItem = new ApplicationBarMenuItem(text);
            appBarMenuItem.Click += (sender, args) => doOnClick();
            _applicationBar.MenuItems.Add(appBarMenuItem);
            return this;
        }

        public LocalizedApplicationBar WithExternalMenuItem(ApplicationBarMenuItem item)
        {
            _applicationBar.MenuItems.Add(item);
            return this;
        }

        public LocalizedApplicationBar WithButton(Action doOnClick, string text, string iconPath)
        {
            var appBarButton = new ApplicationBarIconButton(new Uri(iconPath, UriKind.Relative));
            appBarButton.Click += (sender, args) => doOnClick();
            appBarButton.Text = text;
            _applicationBar.Buttons.Add(appBarButton);
            return this;
        }

        public LocalizedApplicationBar WithExternalButton(ApplicationBarIconButton button)
        {
            _applicationBar.Buttons.Add(button);
            return this;
        }

        public ApplicationBar Build()
        {
            return _applicationBar;
        }
    }
}