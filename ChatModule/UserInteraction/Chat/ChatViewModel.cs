namespace ChatModule.UserInteraction.Chat
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using ChatItem;

    using Common.Enums;
    using Common.GlobalEvents.Packages;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class ChatViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<ChatItemViewModel> Messages { get; } = new ObservableCollection<ChatItemViewModel>();

        public ICommand ClearCommand => new DelegateCommand(ExecuteClearCommand);

        public ChatViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PackageTransmittedEvent>().Subscribe(HandlePackageTransmittedEvent, ThreadOption.UIThread);
        }

        private void AddMessage(AbonentType source, AbonentType target, string message)
        {
            Messages.Add(new ChatItemViewModel(source, target, message, _eventAggregator));
        }

        private void ExecuteClearCommand()
        {
            Messages.Clear();
        }

        private void HandlePackageTransmittedEvent(PackageTransmittedArgs args)
        {
            AddMessage(args.Source, args.Target, args.Message);
        }
    }
}
