namespace ChatModule.UserInteraction.Chat
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    using BusinessLogic.Routing;
    using BusinessLogic.Routing.EventArgs;

    using ChatItem;

    using Common.Enums;

    using Prism.Commands;
    using Prism.Mvvm;

    public class ChatViewModel : BindableBase
    {
        public ObservableCollection<ChatItemViewModel> Messages { get; } = new ObservableCollection<ChatItemViewModel>();

        public ICommand ClearCommand => new DelegateCommand(ExecuteClearCommand);

        public ChatViewModel(Router router)
        {
            router.OnMessageReceived += HandleRouterOnMessageReceived;
        }

        private void AddMessage(AbonentType source, string message)
        {
            Messages.Add(new ChatItemViewModel(source, message));
        }

        private void ExecuteClearCommand()
        {
            Messages.Clear();
        }

        private void HandleRouterOnMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(() => AddMessage(args.Source, args.JsonString));
        }
    }
}
