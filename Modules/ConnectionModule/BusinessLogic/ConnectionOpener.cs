namespace ConnectionModule.BusinessLogic
{
    using System;

    using global::BusinessLogic.Settings;

    using NetworkInteraction;

    using Settings;

    public class ConnectionOpener: IConnectionMaker
    {
        private readonly IWsServer _wsServer;
        private readonly ISettingsManager _settingsManager;

        public event EventHandler OnConnectionBroken;
        public event EventHandler OnConnected;

        public ConnectionOpener(IWsServer wsServer, ISettingsManager settingsManager)
        {
            _wsServer = wsServer;
            _settingsManager = settingsManager;
            _wsServer.OnStopped += HandleWsServerOnStopped;
            _wsServer.OnStarted += HandleWsServerOnStarted;
        }

        private void HandleWsServerOnStarted(object sender, EventArgs eventArgs)
        {
            OnConnected?.Invoke(this, EventArgs.Empty);
        }

        private void HandleWsServerOnStopped(object sender, EventArgs eventArgs)
        {
            OnConnectionBroken?.Invoke(this, EventArgs.Empty);
        }

        public void Connect(ConnectionSettings connectionSettings)
        {
            _settingsManager.UpdateSettings(ConnectionSettingsFileNames.LISTENING_CONNECTION, connectionSettings);
            _wsServer.StartListening(connectionSettings.Ip, connectionSettings.Port);
        }

        public void Disconnect()
        {
            _wsServer.StopListening();
        }

        public ConnectionSettings GetSettings()
        {
            return _settingsManager.GetSettings<ConnectionSettings>(ConnectionSettingsFileNames.LISTENING_CONNECTION);
        }
    }
}
