using System.Windows.Media;
using Caliburn.Micro;
using WackenOpenAir.Utilities;

namespace WackenOpenAir.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            ApplicationBar =
                new LocalizedApplicationBar()
                .AsMinimized()
                .WithBackground(Color.FromArgb(255, 219, 150, 24))
                .WithForeground(Color.FromArgb(255, 245, 245, 245))
                .WithButton(
                    () => Action.Invoke(DataContext, "RefreshNewsFeed", this),
                    AppResources.Global_Refresh,
                    PathToIcon.Refresh)
                .WithRateAndReviewMenu()
                .WithAboutMenu(() => Action.Invoke(DataContext, "NavigateToAboutPage", this))
                .Build();
        }
    }
}