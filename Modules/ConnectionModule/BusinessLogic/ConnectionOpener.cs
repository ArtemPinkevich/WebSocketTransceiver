namespace ConnectionModule.BusinessLogic
{
    using System;

    using NetworkInteraction;

    public class ConnectionOpener: IConnectionMaker
    {
        private readonly WsServer _wsServer;

        public event EventHandler OnConnectionBroken;
        public event EventHandler OnConnected;

        public ConnectionOpener(WsServer wsServer)
        {
            _wsServer = wsServer;
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

        public void Connect(string ip, string port)
        {
            _wsServer.StartListening(ip, port);
        }

        public void Disconnect()
        {
            _wsServer.StopListening();
        }
    }
}
