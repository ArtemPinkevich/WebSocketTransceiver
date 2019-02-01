namespace PackagesStorageModule.UserInteraction.PackagesPanel.ImportExport
{
    using System;
    using System.Windows.Input;

    using Common.GlobalEvents.Packages;

    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    using Properties;

    class ImportExportPackagesViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ICommand ImportCommand => new DelegateCommand(ExecuteImportCommand);
        public ICommand ExportCommand => new DelegateCommand(ExecuteExportCommand);
        public ICommand AddCommand => new DelegateCommand(ExecuteAddCommand);

        public ImportExportPackagesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }


        private void ExecuteImportCommand()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Json files (*.json) | *.json",
                Multiselect = false,
                Title = "Selection of packages set"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _eventAggregator.GetEvent<ImportPackagesRequest>().Publish(openFileDialog.FileName);
            }
        }

        private void ExecuteExportCommand()
        {

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Json files (*.json) | *.json",
                Title = "Export set of packages"
            };

            if (saveFileDialog.ShowDialog() == true)
            {

                _eventAggregator.GetEvent<ExportPackagesRequest>().Publish(saveFileDialog.FileName);
            }
        }
        private void ExecuteAddCommand()
        {
            _eventAggregator.GetEvent<AddNewPackageRequest>().Publish();
        }
    }
}
