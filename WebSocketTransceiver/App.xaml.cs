namespace WebSocketTransceiver
{
    using System.Windows;

    using BusinessLogic.Connection;

    using Connection;

    using NetworkInteraction;

    using Prism.Ioc;
    using Prism.Unity;

    using Shell;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<WsServerService>();
            containerRegistry.RegisterSingleton<WsServer>();
            containerRegistry.RegisterSingleton<WsClient>();
            containerRegistry.Register<IConnectionMaker, ConnectionOpener>("ConnectionOpener");
            containerRegistry.Register<IConnectionMaker, ConnectionRequester>("ConnectionRequester");

            containerRegistry.GetContainer().RegisterType<ConnectionViewModel>(
                "ConnectionOpenerVm",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(Container.Resolve<IConnectionMaker>("ConnectionOpener")));

            containerRegistry.GetContainer().RegisterType<ConnectionViewModel>(
                "ConnectionRequesterVm",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(Container.Resolve<IConnectionMaker>("ConnectionRequester")));

            containerRegistry.GetContainer().RegisterType<MainViewModel>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(Container.Resolve<ConnectionViewModel>("ConnectionOpenerVm"), Container.Resolve<ConnectionViewModel>("ConnectionRequesterVm")));
        }
    }
}
