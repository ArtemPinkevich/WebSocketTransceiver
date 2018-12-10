namespace ChatModule
{
    using Common.Constants;

    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using UserInteraction.Chat;

    public class ChatModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.CHAT_REGION, typeof(ChatView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
