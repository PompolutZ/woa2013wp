using System;
using System.Collections.Generic;
using Caliburn.Micro;
using WackenOpenAir.Services;
using WackenOpenAir.ViewModels;

namespace WackenOpenAir
{
    public class WackenBootstrapper : PhoneBootstrapper
    {
        private PhoneContainer _iocContainer;

        protected override void BuildUp(object instance)
        {
            _iocContainer.BuildUp(instance);
        }

        protected override void Configure()
        {
            _iocContainer = new PhoneContainer();
            _iocContainer.RegisterPhoneServices(RootFrame);

            RegisterServices();
            
            RegisterViews();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _iocContainer.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _iocContainer.GetInstance(service, key);
        }

        private void RegisterServices()
        {
            _iocContainer.RegisterSingleton(typeof(NewsService), "NewsService", typeof(NewsService));
            _iocContainer.RegisterSingleton(typeof(LineupService), "LineupService", typeof(LineupService));
        }

        private void RegisterViews()
        {
            _iocContainer.RegisterSingleton(typeof(MainPageViewModel), "MainPageViewModel", typeof(MainPageViewModel));
            _iocContainer.PerRequest<AboutPageViewModel>();
            _iocContainer.PerRequest<NewsFeedUserControlViewModel>();
        }
    }
}
