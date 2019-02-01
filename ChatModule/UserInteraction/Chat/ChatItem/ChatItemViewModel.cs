namespace ChatModule.UserInteraction.Chat.ChatItem
{
    using System.Windows.Input;

    using Common;
    using Common.Enums;
    using Common.GlobalEvents.Packages;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class ChatItemViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _message;
        private bool _isExpanded;
        public AbonentType Source { get; }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public string SendButtonToolTip => Source == AbonentType.Client ? "Send to Server" : "Send to Client";

        public ICommand ExpandCommand => new DelegateCommand(ExecuteExpandCommand);
        public ICommand ShowInEditorCommand => new DelegateCommand(ExecuteShowInEditorCommand);
        public ICommand SendCommand => new DelegateCommand(ExecuteSendCommand);

        public ChatItemViewModel(AbonentType source, string message, IEventAggregator eventAggregator)
        {
            Source = source;
            Message = message;
            _eventAggregator = eventAggregator;
        }

        private void ExecuteExpandCommand()
        {
            IsExpanded = !IsExpanded;
            Message = JsonHelper.RestructJson(Message, IsExpanded);
        }

        private void ExecuteShowInEditorCommand()
        {
            _eventAggregator.GetEvent<ShowPackageFromChat>().Publish(Message);
        }
        private void ExecuteSendCommand()
        {
            AbonentType target = Source == AbonentType.Client ? AbonentType.Server : AbonentType.Client;
            _eventAggregator.GetEvent<SendPackageRequest>().Publish(new SendPackageRequestArgs(target, Message));
        }
    }
}
