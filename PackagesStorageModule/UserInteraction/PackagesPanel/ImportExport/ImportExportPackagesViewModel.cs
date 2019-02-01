namespace PackagesStorageModule.UserInteraction.PackagesPanel.ImportExport
{
    using System.Windows.Input;

    using Common.GlobalEvents.Packages;

    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    class ImportExportPackagesViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ICommand ImportCommand => new DelegateCommand(ExecuteImportCommand);
        public ICommand ExportCommand => new DelegateCommand(ExecuteExportCommand);
        public ICommand AddCommand => new DelegateCommand(ExecuteAddCommand);
        public ICommand AddFromFileCommand => new DelegateCommand(ExecuteAddFromFileCommand);

        public ImportExportPackagesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private string ChooseFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Json files (*.json) | *.json",
                Multiselect = false,
                Title = "Selection of packages set"
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : string.Empty;
        }

        private void ExecuteImportCommand()
        {
            string fileName = ChooseFile();
            _eventAggregator.GetEvent<ImportPackagesRequest>().Publish(fileName);
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

        private void ExecuteAddFromFileCommand()
        {
            string fileName = ChooseFile();
            _eventAggregator.GetEvent<AddPackagesFromFileRequest>().Publish(fileName);
        }

        private void ExecuteAddCommand()
        {
            _eventAggregator.GetEvent<AddNewPackageRequest>().Publish();
        }
    }
}
