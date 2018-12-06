namespace WebSocketTransceiver.Chat.ChatItem
{
    using BusinessLogic.Routing;

    using Prism.Mvvm;

    class ChatItemViewModel : BindableBase
    {
        public AbonentType Source { get; }
        public string Message { get; }

        public ChatItemViewModel(AbonentType source, string message)
        {
            Source = source;
            Message = message;
        }

    }
}
