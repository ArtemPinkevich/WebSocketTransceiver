namespace TextEditorModule.UserInteraction.TextEditor
{
    using System.Windows.Input;

    using BusinessLogic.Packages;
    using BusinessLogic.Routing;

    using Common;
    using Common.Enums;
    using Common.GlobalEvents.Packages;

    using Data;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    class TextEditorViewModel : BindableBase
    {
        #region Fields

        private bool _isTextValid;
        private readonly Router _router;
        private AbonentType _target = AbonentType.Server;
        private string _text;
        private Package _package = new Package("");
        private string _packageName = string.Empty;
        private readonly PackagesManager _packagesManager;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public AbonentType Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }

        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value);
                CheckTextForJsonValid();
            }
        }

        public string PackageName
        {
            get => _packageName;
            set => SetProperty(ref _packageName, value);
        }

        public bool IsTextValid
        {
            get => _isTextValid;
            set => SetProperty(ref _isTextValid, value);
        }

        public ICommand TargetChangedCommand => new DelegateCommand(ExecuteTargetChangedCommand);
        public ICommand SendCommand => new DelegateCommand(ExecuteSendCommand);
        public ICommand FormatCommand => new DelegateCommand(ExecuteFormatCommand);
        public ICommand SaveCommand => new DelegateCommand(ExecuteSaveCommand);

        #endregion

        #region Constructors

        public TextEditorViewModel(Router router, PackagesManager packagesManager, IEventAggregator eventAggregator)
        {
            _router = router;
            _packagesManager = packagesManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ShowPackageInEditorRequest>().Subscribe(HandleShowPackageInEditorRequest);
        }

        #endregion

        #region Methods

        private void CheckTextForJsonValid()
        {
            IsTextValid = JsonHelper.IsValidJson(Text);
        }

        private void FormatText()
        {
            if (IsTextValid)
            {
                Text = JsonHelper.RestructJson(Text);
            }
        }

        private void ExecuteFormatCommand()
        {
            CheckTextForJsonValid();
            FormatText();
        }

        private void ExecuteTargetChangedCommand()
        {
            _router.SetTarget(_target);
        }

        private void ExecuteSendCommand()
        {
            _router.Send(_text);
        }

        private void Refresh(Package package)
        {
            _package = package;
            PackageName = _package.Name;
            Text = _package.JsonContent;
        }

        private void ExecuteSaveCommand()
        {
            _package.Name = PackageName;
            _package.JsonContent = Text;
            _eventAggregator.GetEvent<PackageEditedEvent>().Publish(_package);
            //_packagesManager.SavePackage(_package);
        }

        private void HandleShowPackageInEditorRequest(Package package)
        {
            Refresh(package);
        }

        #endregion
    }
}
