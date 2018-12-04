namespace NetworkInteraction
{
    using System;

    using WebSocketSharp.Server;

    public class WsServer
    {
        private WsServerService _wsServerService;
        private WebSocketServer _webSocketServer;

        public event EventHandler<PackageReceivedEventArgs> OnMessageReceived;
        public event EventHandler OnStopped;
        public event EventHandler OnStarted;
        public event EventHandler OnClosed;

        public void StartListening(string ip, string port)
        {
            _webSocketServer = new WebSocketServer($"ws://{ip}:{port}");
            AddWsServerService();
            _webSocketServer.Start();
            OnStarted?.Invoke(this, EventArgs.Empty);
        }

        public void StopListening()
        {
            _webSocketServer.Stop();
            _webSocketServer = null;
            OnStopped?.Invoke(this, EventArgs.Empty);
        }

        public void SendMessage(string message)
        {
            _wsServerService.SendMessage(message);
        }

        private void AddWsServerService()
        {
            _wsServerService = new WsServerService();
            _wsServerService.OnMessageReceived += HandleWsServerServiceOnMessageReceived;
            _wsServerService.OnClosed += HandleWsServerServiceOnClosed;
            _webSocketServer.AddWebSocketService("/", () => _wsServerService);
        }

        private void RemoveWsServerService()
        {
            _wsServerService.OnClosed -= HandleWsServerServiceOnClosed;
            _wsServerService.OnMessageReceived -= HandleWsServerServiceOnMessageReceived;
        }

        private void HandleWsServerServiceOnMessageReceived(object sender, PackageReceivedEventArgs args)
        {
            OnMessageReceived?.Invoke(this, args);
        }

        private void HandleWsServerServiceOnClosed(object sender, EventArgs eventArgs)
        {
            RemoveWsServerService();
            AddWsServerService();
            OnClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
