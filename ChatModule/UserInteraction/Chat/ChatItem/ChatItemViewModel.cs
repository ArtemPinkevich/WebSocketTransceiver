namespace ChatModule.UserInteraction.Chat.ChatItem
{
    using Common.Enums;

    using Prism.Mvvm;

    public class ChatItemViewModel : BindableBase
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
