namespace Common.GlobalEvents.Packages
{
    using System.Collections.Generic;

    using Data;

    using Prism.Events;

    public class PackagesListRefreshed : PubSubEvent<List<Package>>
    {
    }
}
