namespace ChatModule.UserInteraction.Chat
{
    using System.Collections.ObjectModel;
    using System.Windows;

    using BusinessLogic.Routing;
    using BusinessLogic.Routing.EventArgs;

    using ChatItem;

    using Common.Enums;

    using Prism.Mvvm;

    public class ChatViewModel : BindableBase
    {
        public ObservableCollection<ChatItemViewModel> Messages { get; } = new ObservableCollection<ChatItemViewModel>();

        public ChatViewModel(Router router)
        {
            router.OnMessageReceived += HandleRouterOnMessageReceived;
        }

        private void AddMessage(AbonentType source, string message)
        {
            Messages.Add(new ChatItemViewModel(source, message));
        }

        private void HandleRouterOnMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(() => AddMessage(args.Source, args.JsonString));
        }
    }
}
