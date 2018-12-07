namespace BusinessLogic.Routing
{
    using System;

    using EventArgs;

    using NetworkInteraction;

    public class Router
    {
        private readonly WsClient _wsClient;
        private readonly WsServer _wsServer;
        private bool _enableRepeaterMode = true;

        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        public Router(WsServer wsServer, WsClient wsClient)
        {
            _wsServer = wsServer;
            _wsServer.OnMessageReceived += HandleWsServerOnMessageReceived;


            _wsClient = wsClient;
            _wsClient.OnMessageReceived += HandleWsClientOnMessageReceived;
            _wsServer.OnClosed += HandleWsServerOnClosed;
        }

        private void HandleWsServerOnClosed(object sender, System.EventArgs eventArgs)
        {
            if (_enableRepeaterMode)
            {
                _wsClient.Disconnect();
            }
        }

        public void Send(AbonentType target, string message)
        {
            switch (target)
            {
                case AbonentType.Client:
                    _wsServer.SendMessage(message);
                    break;
                case AbonentType.Server:
                    _wsClient.SendMessage(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
            
        }

        private void HandleWsServerOnMessageReceived(object sender, PackageReceivedEventArgs args)
        {
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(AbonentType.Client, args.JsonString));

            if (_enableRepeaterMode)
            {
                Send(AbonentType.Server, args.JsonString);
            }
        }

        private void HandleWsClientOnMessageReceived(object sender, PackageReceivedEventArgs args)
        {
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(AbonentType.Server, args.JsonString));

            if (_enableRepeaterMode)
            {
                Send(AbonentType.Client, args.JsonString);
            }
        }

    }
}
