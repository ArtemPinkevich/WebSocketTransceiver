namespace BusinessLogic.Routing.EventArgs
{
    using Common.Enums;

    public class MessageReceivedEventArgs
    {
        public AbonentType Source { get; }
        public string JsonString { get; }

        public MessageReceivedEventArgs(AbonentType source, string jsonString)
        {
            Source = source;
            JsonString = jsonString;
        }
    }
}
