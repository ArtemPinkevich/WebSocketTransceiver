namespace BusinessLogic.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BusinessLogic.Settings;

    using Common.GlobalEvents;
    using Common.GlobalEvents.Packages;

    using Data;

    using Prism.Events;

    using Settings;

    public class PackagesManager
    {
        private const string GENERAL_PACKAGES_SETTINGS_FILE_NAME = "settings\\packages\\General.json";
        private const string DEFAULT_PACKAGE_NAME = "Package";
        private const string DEFAULT_PACKAGE_FROM_CHAT_NAME = "Package from chat";
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsManager _settingsManager;
        private List<Package> _packages = new List<Package>();
        private PackagesSettings _packagesSettings;

        public PackagesManager(IEventAggregator eventAggregator, ISettingsManager settingsManager)
        {
            _eventAggregator = eventAggregator;
            _settingsManager = settingsManager;
            Init();

            _eventAggregator.GetEvent<AppClosingEvent>().Subscribe(HandleOnAppClosing);
            _eventAggregator.GetEvent<AddNewPackageRequest>().Subscribe(HandleAddNewPackageRequest);
            _eventAggregator.GetEvent<GetPackagesRequest>().Subscribe(HandleGetPackagesRequest);
            _eventAggregator.GetEvent<GetPackagesFileNameRequest>().Subscribe(HandleGetPackagesFileNameRequest);
            _eventAggregator.GetEvent<ImportPackagesRequest>().Subscribe(HandleImportPackagesRequest);
            _eventAggregator.GetEvent<AddPackagesFromFileRequest>().Subscribe(HandleAddPackagesFromFileRequest);
            _eventAggregator.GetEvent<ExportPackagesRequest>().Subscribe(HandleExportPackagesRequest);
            _eventAggregator.GetEvent<RemovePackageRequest>().Subscribe(HandleRemovePackageRequest);
            _eventAggregator.GetEvent<PackageMoveRequest>().Subscribe(HandlePackageMoveRequest);
            _eventAggregator.GetEvent<ShowPackageFromChat>().Subscribe(HandleShowPackageFromChat);
        }

        private void Init()
        {
            _packagesSettings = _settingsManager.GetSettings<PackagesSettings>(GENERAL_PACKAGES_SETTINGS_FILE_NAME);
            _packages = _settingsManager.GetSettings<List<Package>>(_packagesSettings.PackagesListFileName);
        }

        private void SavePackages()
        {
            _settingsManager.Save(GENERAL_PACKAGES_SETTINGS_FILE_NAME, _packagesSettings);
            _settingsManager.Save(_packagesSettings.PackagesListFileName, _packages);
        }

        private void InvokePackagesFileNameRefreshedEvent()
        {
            _eventAggregator.GetEvent<PackagesFileNameRefreshedEvent>().Publish(_packagesSettings.PackagesListFileName);
        }

        private void InvokePackagesListRefreshedEvent()
        {
            _eventAggregator.GetEvent<PackagesListRefreshed>().Publish(_packages);
        }

        private void HandleRemovePackageRequest(Guid guid)
        {
            Package package = _packages.FirstOrDefault(o => o.Guid == guid);
            if (package != null)
            {
                _packages.Remove(package);
                _eventAggregator.GetEvent<PackageRemovedEvent>().Publish(package);
            }
        }

        private void HandlePackageMoveRequest(PackageMoveRequestArgs args)
        {
            Package package = _packages[args.SourceIndex];

            _packages.Insert(args.TargetIndex, package);

            if (args.TargetIndex > args.SourceIndex)
            {
                _packages.Remove(package);
            }
            else
            {
                _packages.RemoveAt(args.SourceIndex + 1);
            }

            _eventAggregator.GetEvent<PackagesListRefreshed>().Publish(_packages);
        }

        private void HandleAddNewPackageRequest()
        {
            var newPackage = new Package(DEFAULT_PACKAGE_NAME);
            _packages.Add(newPackage);
            _eventAggregator.GetEvent<PackageAddedEvent>().Publish(newPackage);
        }

        private void HandleGetPackagesRequest()
        {
            InvokePackagesListRefreshedEvent();
        }

        private void HandleGetPackagesFileNameRequest()
        {
            InvokePackagesFileNameRefreshedEvent();
        }

        private void HandleShowPackageFromChat(string packageContent)
        {
            Package package = _packages.FirstOrDefault(o => o.Name == DEFAULT_PACKAGE_FROM_CHAT_NAME);
            if (package == null)
            {
                var newPackage = new Package(DEFAULT_PACKAGE_FROM_CHAT_NAME)
                {
                    JsonContent = packageContent
                };

                _packages.Add(newPackage);
                _eventAggregator.GetEvent<PackageAddedEvent>().Publish(newPackage);
            }
            else
            {
                package.JsonContent = packageContent;
                _eventAggregator.GetEvent<PackageEditedEvent>().Publish(package);
                _eventAggregator.GetEvent<ShowPackageInEditorRequest>().Publish(package);
            }
        }

        private void HandleImportPackagesRequest(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                _packagesSettings.PackagesListFileName = fileName;
                _packages = _settingsManager.GetSettings<List<Package>>(_packagesSettings.PackagesListFileName, false);
                InvokePackagesListRefreshedEvent();
                InvokePackagesFileNameRefreshedEvent();
            }
        }

        private void HandleAddPackagesFromFileRequest(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                _packages.AddRange(_settingsManager.GetSettings<List<Package>>(fileName, false));
                InvokePackagesListRefreshedEvent();
            }
        }

        private void HandleExportPackagesRequest(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                _packagesSettings.PackagesListFileName = fileName;
                SavePackages();
                InvokePackagesFileNameRefreshedEvent();
            }
        }

        private void HandleOnAppClosing()
        {
            SavePackages();
        }

        public List<Package> GetPackages()
        {
            return _packages;
        }
    }
}
