namespace NetworkInteraction
{
    using System;

    using WebSocketSharp;

    public class WsClient
    {
        #region Fields

        private WebSocket _webSocket;

        #endregion

        #region Events

        public event EventHandler<PackageReceivedEventArgs> OnMessageReceived;
        public event EventHandler OnClosed;
        public event EventHandler OnConnected;

        #endregion

        #region Methods

        public void Connect(string ip, string port)
        {
            if (_webSocket != null)
                return;

            _webSocket = new WebSocket($"ws://{ip}:{port}");

            _webSocket.OnOpen += HandleWebSocketOnOpen;
            _webSocket.OnMessage += HandleWebSocketOnMessage;
            _webSocket.OnError += HandleWebSocketOnError;
            _webSocket.OnClose += HandleWebSocketOnClose;

            _webSocket.Connect();
        }

        public void SendMessage(string data)
        {
            _webSocket?.Send(data);
        }

        private void FreeWebSocket()
        {
            _webSocket.OnOpen -= HandleWebSocketOnOpen;
            _webSocket.OnMessage -= HandleWebSocketOnMessage;
            _webSocket.OnError -= HandleWebSocketOnError;
            _webSocket.OnClose -= HandleWebSocketOnClose;
            _webSocket = null;
        }

        private void HandleWebSocketOnMessage(object o, MessageEventArgs args)
        {
            OnMessageReceived?.Invoke(this, new PackageReceivedEventArgs(args.Data));
        }

        private void HandleWebSocketOnOpen(object o, EventArgs args)
        {
            OnConnected?.Invoke(this, EventArgs.Empty);
        }

        private void HandleWebSocketOnClose(object sender, CloseEventArgs closeEventArgs)
        {
            FreeWebSocket();
            OnClosed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleWebSocketOnError(object o, ErrorEventArgs errorEventArgs)
        {
            FreeWebSocket();
            OnClosed?.Invoke(this, EventArgs.Empty);
        }

        public void Disconnect()
        {
            _webSocket?.CloseAsync();
        }

        #endregion
    }
}
