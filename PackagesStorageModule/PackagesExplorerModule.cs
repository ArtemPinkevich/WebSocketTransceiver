namespace PackagesStorageModule
{
    using Common.Constants;

    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using UserInteraction.PackagesPanel;

    public class PackagesExplorerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.PACKAGES_EXPLORER_REGION, typeof(PackagesPanelView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
