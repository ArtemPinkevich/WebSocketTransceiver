namespace BusinessLogic.Routing.EventArgs
{
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
