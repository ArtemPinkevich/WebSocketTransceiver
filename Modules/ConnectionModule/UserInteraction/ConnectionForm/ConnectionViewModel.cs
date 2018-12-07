namespace ConnectionModule.UserInteraction.ConnectionForm
{
    using System;
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Mvvm;

    public class ConnectionViewModel : BindableBase
    {
        private readonly IConnectionMaker _connectionMaker;
        private string _ip = "192.168.36.18";
        private string _port = "8192";
        private bool _isConnected;

        public string Ip
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }

        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public ICommand ConnectCommand { get; }

        public ConnectionViewModel(IConnectionMaker connectionMaker)
        {
            _connectionMaker = connectionMaker;
            _connectionMaker.OnConnected += HandleConnectionMakerOnConnected;
            _connectionMaker.OnConnectionBroken += HandleConnectionMakerOnConnectionBroken;
            ConnectCommand = new DelegateCommand(ExecuteConnectCommand);
        }

        private void HandleConnectionMakerOnConnected(object sender, EventArgs eventArgs)
        {
            IsConnected = true;
        }

        private void HandleConnectionMakerOnConnectionBroken(object sender, EventArgs eventArgs)
        {
            IsConnected = false;
        }

        private void ExecuteConnectCommand()
        {
            if (_isConnected)
            {
                _connectionMaker.Disconnect();
            }
            else
            {
                _connectionMaker.Connect(_ip, _port);
            }
        }
    }
}
