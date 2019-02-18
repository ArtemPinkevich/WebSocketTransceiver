namespace ConnectionModule.UserInteraction.ConnectionPanel
{
    using ConnectionForm;

    using Prism.Mvvm;

    public class ConnectionPanelViewModel : BindableBase
    {
        public ConnectionViewModel ConnectionOpenerVm { get; set; }
        public ConnectionViewModel ConnectionRequesterVm { get; set; }

        public ConnectionPanelViewModel(ConnectionViewModel connectionOpenerVm, ConnectionViewModel connectionRequesterVm)
        {
            ConnectionOpenerVm = connectionOpenerVm;
            ConnectionRequesterVm = connectionRequesterVm;
        }
    }
}
