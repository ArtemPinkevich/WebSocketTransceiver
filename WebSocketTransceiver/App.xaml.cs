namespace WebSocketTransceiver
{
    using System.Windows;

    using ConnectionModule;

    using MainWindow;

    using NetworkInteraction;

    using Prism.Ioc;
    using Prism.Modularity;

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
            containerRegistry.RegisterSingleton<WsServer>();
            containerRegistry.RegisterSingleton<WsClient>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ConnectionModule>();
        }
    }
}
