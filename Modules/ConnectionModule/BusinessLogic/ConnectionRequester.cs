namespace ConnectionModule.BusinessLogic
{
    using System;

    using NetworkInteraction;

    public class ConnectionRequester : IConnectionMaker
    {
        public event EventHandler OnConnectionBroken;
        public event EventHandler OnConnected;

        private readonly WsClient _wsClient;

        public ConnectionRequester(WsClient wsServer)
        {
            _wsClient = wsServer;
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

        public void Connect(string ip, string port)
        {
            _wsClient.Connect(ip, port);
        }

        public void Disconnect()
        {
            _wsClient.Disconnect();
        }
    }
}
