namespace WebSocketTransceiver.Shell
{
    using Connection;

    using Prism.Mvvm;

    class MainViewModel : BindableBase
    {
        public ConnectionViewModel ConnectionOpenerVm { get; set; }
        public ConnectionViewModel ConnectionRequesterVm { get; set; }

        public MainViewModel(ConnectionViewModel connectionOpenerVm, ConnectionViewModel connectionRequesterVm)
        {
            ConnectionOpenerVm = connectionOpenerVm;
            ConnectionRequesterVm = connectionRequesterVm;
        }
    }
}
