namespace PackagesStorageModule.UserInteraction.PackagesPanel.ImportExport
{
    using System.IO;
    using System.Windows.Input;

    using Common.GlobalEvents.Packages;

    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    class ImportExportPackagesViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _fileName;
        private string _fullFileName;

        public ICommand ImportCommand => new DelegateCommand(ExecuteImportCommand);
        public ICommand ExportCommand => new DelegateCommand(ExecuteExportCommand);
        public ICommand AddCommand => new DelegateCommand(ExecuteAddCommand);
        public ICommand AddFromFileCommand => new DelegateCommand(ExecuteAddFromFileCommand);

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public string FullFileName
        {
            get => _fullFileName;
            set => SetProperty(ref _fullFileName, value);
        }

        public ImportExportPackagesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PackagesFileNameRefreshedEvent>().Subscribe(HandlePackagesFileNameRefreshedEvent);
            _eventAggregator.GetEvent<GetPackagesFileNameRequest>().Publish();
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

        private void HandlePackagesFileNameRefreshedEvent(string fileName)
        {
            FullFileName = Path.GetFullPath(fileName);
            FileName = Path.GetFileName(FullFileName);
        }
    }
}
