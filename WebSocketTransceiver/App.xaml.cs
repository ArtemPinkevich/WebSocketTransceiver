namespace WebSocketTransceiver
{
    using System.Windows;

    using BusinessLogic.Packages;
    using BusinessLogic.Routing;
    using BusinessLogic.Settings;

    using ChatModule;

    using ConnectionModule;

    using MainWindow;

    using NetworkInteraction;

    using PackagesStorageModule;

    using Prism.Ioc;
    using Prism.Modularity;

    using TextEditorModule;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            // Заставляем PackagesManager работать
            Container.Resolve<IPackagesManager>();

            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IWsServer, WsServer>();
            containerRegistry.RegisterSingleton<IWsClient, WsClient>();
            containerRegistry.RegisterSingleton<IRouter, Router>();
            containerRegistry.RegisterSingleton<ISettingsManager, SettingsManager>();
            containerRegistry.RegisterSingleton<IPackagesManager, PackagesManager>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ConnectionModule>();
            moduleCatalog.AddModule<TextEditorModule>();
            moduleCatalog.AddModule<ChatModule>();
            moduleCatalog.AddModule<PackagesExplorerModule>();
        }
    }
}
