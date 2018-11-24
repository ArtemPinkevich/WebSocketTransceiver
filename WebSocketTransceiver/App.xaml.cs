namespace WebSocketTransceiver
{
    using System.Windows;

    using Prism.Ioc;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //throw new System.NotImplementedException();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell.MainView>();
        }
    }
}
