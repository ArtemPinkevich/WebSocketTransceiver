namespace Common.GlobalEvents.Packages
{
    using System;

    using Prism.Events;

    public class RemovePackageRequest : PubSubEvent<Guid>
    {
    }
}
