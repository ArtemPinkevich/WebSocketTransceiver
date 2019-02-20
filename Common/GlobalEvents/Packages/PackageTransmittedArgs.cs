namespace Common.GlobalEvents.Packages
{
    using Enums;

    public class PackageTransmittedArgs
    {
        public AbonentType Source { get; }
        public AbonentType Target { get; }
        public string Message { get; }

        public PackageTransmittedArgs(AbonentType source, AbonentType target, string messsage)
        {
            Source = source;
            Target = target;
            Message = messsage;
        }
    }
}
