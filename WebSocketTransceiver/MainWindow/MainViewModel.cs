namespace WebSocketTransceiver.MainWindow
{
    using System.Windows.Input;

    using Common.GlobalEvents;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ICommand ExitCommand { get; }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            ExitCommand = new DelegateCommand(ExecuteExitCommand);
        }

        private void ExecuteExitCommand()
        {
            _eventAggregator.GetEvent<AppClosingEvent>().Publish();
        }
    }
}
