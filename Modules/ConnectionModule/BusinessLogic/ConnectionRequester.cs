namespace ConnectionModule.BusinessLogic
{
    using System;

    using global::BusinessLogic.Settings;

    using NetworkInteraction;

    using Settings;

    public class ConnectionRequester : IConnectionMaker
    {
        public event EventHandler OnConnectionBroken;
        public event EventHandler OnConnected;

        private readonly IWsClient _wsClient;
        private readonly ISettingsManager _settingsManager;

        public ConnectionRequester(IWsClient wsServer, ISettingsManager settingsManager)
        {
            _wsClient = wsServer;
            _settingsManager = settingsManager;
            _wsClient.OnConnected += WsClientOnConnected;
            _wsClient.OnClosed += HandleWsClientOnClosed;
        }

        private void HandleWsClientOnClosed(object sender, EventArgs eventArgs)
        {
            OnConnectionBroken?.Invoke(this, EventArgs.Empty);
        }

        private void WsClientOnConnected(object sender, EventArgs eventArgs)
        {
            OnConnected?.Invoke(this, EventArgs.Empty);
        }

        public void Connect(ConnectionSettings connectionSettings)
        {
            _settingsManager.UpdateSettings(ConnectionSettingsFileNames.REQUESTING_CONNECTION, connectionSettings);
            _wsClient.Connect(connectionSettings.Ip, connectionSettings.Port);
        }

        public void Disconnect()
        {
            _wsClient.Disconnect();
        }

        public ConnectionSettings GetSettings()
        {
            return _settingsManager.GetSettings<ConnectionSettings>(ConnectionSettingsFileNames.REQUESTING_CONNECTION);
        }
    }
}
