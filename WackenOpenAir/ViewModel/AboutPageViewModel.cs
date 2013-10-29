using System.Windows.Input;
using System.Windows.Threading;

using Coding4Fun.Phone.Controls.Data;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Phone.Tasks;

namespace WackenOpenAir.ViewModel
{
    public class AboutPageViewModel : ViewModelBase
    {
        public const string SupportEmailAddress = "pompolutz@gmail.com";
        private readonly RelayCommand _executeEmailComposeTaskCommand;
        private readonly RelayCommand _executeReviewAppTaskCommand;

        public AboutPageViewModel(Dispatcher dispatcher)
        {
            _executeReviewAppTaskCommand =
                new RelayCommand(() => dispatcher.BeginInvoke(new MarketplaceReviewTask().Show));
            _executeEmailComposeTaskCommand =
                new RelayCommand(() => dispatcher.BeginInvoke(new EmailComposeTask {To = SupportEmailAddress}.Show));
        }

        public ICommand ExecuteReviewAppTaskCommand
        {
            get { return _executeReviewAppTaskCommand; }
        }

        public ICommand ExecuteEmailComposeTaskCommand
        {
            get { return _executeEmailComposeTaskCommand; }
        }

        public string Title
        {
            get
            {
                return PhoneHelper.GetAppAttribute("Title");
            }
        }

        public string Version
        {
            get
            {
                return PhoneHelper.GetAppAttribute("Version");
            }
        }


        public string SupportEmail
        {
            get { return SupportEmailAddress; }
        }
    }
}