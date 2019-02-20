namespace Common.GlobalEvents.Packages
{
    using Enums;

    public class SendPackageRequestArgs
    {
        public AbonentType? Target { get; }
        public string Message { get; }

        public SendPackageRequestArgs(AbonentType? target, string message)
        {
            Target = target;
            Message = message;
        }
    }
}
