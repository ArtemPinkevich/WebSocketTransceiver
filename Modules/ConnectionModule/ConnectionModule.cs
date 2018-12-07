namespace ConnectionModule
{
    using BusinessLogic;

    using Common.constants;

    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using UserInteraction.ConnectionForm;
    using UserInteraction.ConnectionPanel;

    using Prism.Unity;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;


    public class ConnectionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.CONNECTION_REGION, typeof(ConnectionPanelView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();

            containerRegistry.Register<IConnectionMaker, ConnectionOpener>("ConnectionOpener");
            containerRegistry.Register<IConnectionMaker, ConnectionRequester>("ConnectionRequester");

            container.RegisterType<ConnectionViewModel>(
                "ConnectionOpenerVm",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(container.Resolve<IConnectionMaker>("ConnectionOpener")));

            container.RegisterType<ConnectionViewModel>(
                "ConnectionRequesterVm",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(container.Resolve<IConnectionMaker>("ConnectionRequester")));

            container.RegisterType<ConnectionPanelViewModel>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(container.Resolve<ConnectionViewModel>("ConnectionOpenerVm"), container.Resolve<ConnectionViewModel>("ConnectionRequesterVm")));
        }
    }
}
