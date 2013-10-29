using Caliburn.Micro;
using Coding4Fun.Phone.Controls.Data;

namespace WackenOpenAir.ViewModels
{
    public class AboutPageViewModel : Screen
    {
        public const string SupportEmailAddress = "pompolutz@gmail.com";

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