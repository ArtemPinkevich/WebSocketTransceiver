namespace WebSocketTransceiver
{
    using System.Windows;

    using BusinessLogic.Packages;
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
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<WsServer>();
            containerRegistry.RegisterSingleton<WsClient>();
            containerRegistry.RegisterSingleton<ISettingsManager, SettingsManager>();
            containerRegistry.RegisterSingleton<PackagesManager>();
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
