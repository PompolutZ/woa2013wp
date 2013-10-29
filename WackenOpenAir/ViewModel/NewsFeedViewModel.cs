using System;

using GalaSoft.MvvmLight;

namespace WackenOpenAir.ViewModel
{
    public class NewsFeedViewModel : ViewModelBase
    {
        private string _newsItemTitle;
        private DateTime _newsItemPublishDate;
        private string _newsItemDetails;
        private string _newsItemLink;

        public string NewsItemTitle
        {
            get
            {
                return _newsItemTitle;
            }
            set
            {
                _newsItemTitle = value;
                RaisePropertyChanged(() => NewsItemTitle);
            }
        }

        public string NewsItemDetails
        {
            get
            {
                return _newsItemDetails;
            }
            set
            {
                _newsItemDetails = value;
                RaisePropertyChanged(() => NewsItemDetails);
            }
        }

        public DateTime NewsItemPublishDate
        {
            get
            {
                return _newsItemPublishDate;
            }
            set
            {
                _newsItemPublishDate = value;
                RaisePropertyChanged(() => NewsItemPublishDate);
            }
        }

        public string NewsItemLink
        {
            get
            {
                return _newsItemLink;
            }
            set
            {
                _newsItemLink = value;
                RaisePropertyChanged(() => NewsItemLink);
            }
        }
    }
}