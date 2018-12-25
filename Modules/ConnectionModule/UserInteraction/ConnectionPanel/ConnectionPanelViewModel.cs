namespace ConnectionModule.UserInteraction.ConnectionPanel
{
    using System.Windows.Input;

    using Common.GlobalEvents;

    using ConnectionForm;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class ConnectionPanelViewModel : BindableBase
    {
        private bool _isRepeaterModeEnabled;
        private readonly IEventAggregator _eventAggregator;

        public ConnectionViewModel ConnectionOpenerVm { get; set; }
        public ConnectionViewModel ConnectionRequesterVm { get; set; }

        public bool IsRepeaterModeEnabled
        {
            get => _isRepeaterModeEnabled;
            set => SetProperty(ref _isRepeaterModeEnabled, value);
        }

        public ICommand SwitchRepeaterModeCommand => new DelegateCommand(ExecuteSwitchRepeaterModeCommand);

        public ConnectionPanelViewModel(ConnectionViewModel connectionOpenerVm, ConnectionViewModel connectionRequesterVm, IEventAggregator eventAggregator)
        {
            ConnectionOpenerVm = connectionOpenerVm;
            ConnectionRequesterVm = connectionRequesterVm;
            _eventAggregator = eventAggregator;
        }

        private void ExecuteSwitchRepeaterModeCommand()
        {
            _eventAggregator.GetEvent<SwitchRepeaterModeEvent>().Publish(_isRepeaterModeEnabled);
        }
    }
}
