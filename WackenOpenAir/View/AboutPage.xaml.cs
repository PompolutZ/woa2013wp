using Microsoft.Phone.Controls;

using WackenOpenAir.ViewModel;

namespace WackenOpenAir.View
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var vm = new AboutPageViewModel(Dispatcher);
            DataContext = vm;
        }
    }
}