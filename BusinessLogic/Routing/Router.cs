namespace BusinessLogic.Routing
{
    using System;

    using Common.Enums;
    using Common.GlobalEvents;
    using Common.GlobalEvents.Packages;

    using NetworkInteraction;

    using Prism.Events;

    public class Router : IRouter
    {
        private readonly IWsClient _wsClient;
        private readonly IWsServer _wsServer;
        private readonly IEventAggregator _eventAggregator;
        private bool _enableRepeaterMode;
        private AbonentType _target = AbonentType.Server;

        public Router(IWsServer wsServer, IWsClient wsClient, IEventAggregator eventAggregator)
        {
            _wsServer = wsServer;
            _wsClient = wsClient;

            _wsServer.OnMessageReceived += HandleWsServerOnMessageReceived;
            _wsServer.OnClosed += HandleWsServerOnClosed;
            _wsClient.OnMessageReceived += HandleWsClientOnMessageReceived;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SwitchRepeaterModeEvent>().Subscribe(HandleSwitchRepeaterModeEvent);
            _eventAggregator.GetEvent<SendPackageRequest>().Subscribe(HandleSendPackageRequest);
        }

        public void SetTarget(AbonentType target)
        {
            _target = target;
        }

        private void Send(AbonentType target, string message)
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

        private void HandleWsServerOnClosed(object sender, System.EventArgs eventArgs)
        {
            if (_enableRepeaterMode)
            {
                _wsClient.Disconnect();
            }
        }

        private void HandleSwitchRepeaterModeEvent(bool isEnabled)
        {
            _enableRepeaterMode = isEnabled;
        }

        private void HandleSendPackageRequest(SendPackageRequestArgs args)
        {
            AbonentType target = args.Target ?? _target;
            Send(target, args.Message);

            _eventAggregator.GetEvent<PackageTransmittedEvent>().Publish(new PackageTransmittedArgs(AbonentType.WebSocketTransceiver, target, args.Message));
        }

        private void HandleWsServerOnMessageReceived(object sender, PackageReceivedEventArgs args)
        {
            var target = AbonentType.WebSocketTransceiver;

            if (_enableRepeaterMode)
            {
                target = AbonentType.Server;
                Send(target, args.JsonString);
            }

            _eventAggregator.GetEvent<PackageTransmittedEvent>().Publish(new PackageTransmittedArgs(AbonentType.Client, target, args.JsonString));
        }

        private void HandleWsClientOnMessageReceived(object sender, PackageReceivedEventArgs args)
        {
            var target = AbonentType.WebSocketTransceiver;

            if (_enableRepeaterMode)
            {
                target = AbonentType.Client;
                Send(target, args.JsonString);
            }

            _eventAggregator.GetEvent<PackageTransmittedEvent>().Publish(new PackageTransmittedArgs(AbonentType.Server, target, args.JsonString));
        }
    }
}
