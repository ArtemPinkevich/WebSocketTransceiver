namespace ChatModule.UserInteraction.TransmissionSettingsPanel
{
    using System.Windows.Input;

    using Common.GlobalEvents;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    class TransmissionSettingsPanelViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isRepeaterModeEnabled;

        public bool IsRepeaterModeEnabled
        {
            get => _isRepeaterModeEnabled;
            set => SetProperty(ref _isRepeaterModeEnabled, value);
        }

        public ICommand SwitchRepeaterModeCommand => new DelegateCommand(ExecuteSwitchRepeaterModeCommand);

        public TransmissionSettingsPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private void ExecuteSwitchRepeaterModeCommand()
        {
            _eventAggregator.GetEvent<SwitchRepeaterModeEvent>().Publish(_isRepeaterModeEnabled);
        }
    }
}
