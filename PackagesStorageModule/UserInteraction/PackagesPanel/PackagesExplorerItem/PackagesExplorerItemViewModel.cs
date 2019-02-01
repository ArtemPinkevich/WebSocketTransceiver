namespace PackagesStorageModule.UserInteraction.PackagesPanel.PackagesExplorerItem
{
    using System.Windows.Input;

    using Common.GlobalEvents.Packages;

    using Data;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    internal class PackagesExplorerItemViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name;
        private Package _package;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICommand RemoveCommand => new DelegateCommand(ExecuteRemoveCommand);
        public ICommand SendCommand => new DelegateCommand(ExecuteSendCommand);

        public PackagesExplorerItemViewModel(Package package, IEventAggregator eventAggregator)
        {
            _package = package;
            _name = _package.Name;
            _eventAggregator = eventAggregator;
        }

        public Package GetPackage()
        {
            return _package;
        }

        public void Refresh(Package package)
        {
            _package = package;
            Name = _package.Name;
        }

        private void ExecuteRemoveCommand()
        {
            _eventAggregator.GetEvent<RemovePackageRequest>().Publish(_package.Name);
        }

        private void ExecuteSendCommand()
        {
            _eventAggregator.GetEvent<SendPackageRequest>().Publish(new SendPackageRequestArgs(null, _package.JsonContent));
        }
    }
}
