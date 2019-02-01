namespace PackagesStorageModule.UserInteraction.PackagesPanel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using Common.GlobalEvents.Packages;

    using Data;

    using GongSolutions.Wpf.DragDrop;

    using PackagesExplorerItem;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    class PackagesPanelViewModel : BindableBase, IDropTarget
    {
        private readonly IEventAggregator _eventAggregator;
        private PackagesExplorerItemViewModel _selectedPackage;

        public ObservableCollection<PackagesExplorerItemViewModel> Packages { get; } = new ObservableCollection<PackagesExplorerItemViewModel>();

        public PackagesExplorerItemViewModel SelectedPackage
        {
            get => _selectedPackage;
            set => SetProperty(ref _selectedPackage, value);
        }

        public ICommand SelectedCommand => new DelegateCommand(ExecuteSelectedCommand);

        public PackagesPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PackagesListRefreshed>().Subscribe(HandlePackagesListRefreshed);
            _eventAggregator.GetEvent<PackageAddedEvent>().Subscribe(HandlePackageAddedEvent);
            _eventAggregator.GetEvent<PackageEditedEvent>().Subscribe(HandlePackageEditedEvent);
            _eventAggregator.GetEvent<PackageRemovedEvent>().Subscribe(HandlePackageRemovedEvent);
            Init();
        }

        private void Init()
        {
            _eventAggregator.GetEvent<GetPackagesRequest>().Publish();
        }

        private void ExecuteSelectedCommand()
        {
            if (SelectedPackage != null)
            {
                _eventAggregator.GetEvent<ShowPackageInEditorRequest>().Publish(SelectedPackage.GetPackage());
            }
        }

        private void HandlePackagesListRefreshed(List<Package> packages)
        {
            Packages.Clear();
            Packages.AddRange(packages.Select(o => new PackagesExplorerItemViewModel(o, _eventAggregator)));
        }

        private void HandlePackageRemovedEvent(Package package)
        {
            PackagesExplorerItemViewModel packageVm = Packages.FirstOrDefault(o => o.GetPackage().Name == package.Name);
            Packages.Remove(packageVm);
            if (SelectedPackage == null)
            {
                SelectedPackage = Packages.FirstOrDefault();
            }
        }

        private void HandlePackageAddedEvent(Package package)
        {
            var packageVm = new PackagesExplorerItemViewModel(package, _eventAggregator);
            Packages.Add(packageVm);
            SelectedPackage = packageVm;
        }

        private void HandlePackageEditedEvent(Package package)
        {
            PackagesExplorerItemViewModel packageVm = Packages.FirstOrDefault(o => o.GetPackage().Guid == package.Guid);
            if (packageVm != null)
            {
                packageVm.Refresh(package);
                SelectedPackage = packageVm;
            }
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is PackagesExplorerItemViewModel)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is PackagesExplorerItemViewModel sourceItem)
            {
                int sourceIndex = Packages.IndexOf(sourceItem);
                _eventAggregator.GetEvent<PackageMoveRequest>().Publish(new PackageMoveRequestArgs(sourceIndex, dropInfo.InsertIndex));
            }
        }
    }
}
