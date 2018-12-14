namespace TextEditorModule
{
    using Common.Constants;

    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    using UserInteraction.TextEditor;

    public class TextEditorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.TEXT_EDITOR_REGION, typeof(TextEditorView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
