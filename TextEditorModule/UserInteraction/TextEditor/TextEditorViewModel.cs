namespace TextEditorModule.UserInteraction.TextEditor
{
    using System.Windows.Input;

    using BusinessLogic.Routing;

    using Common;
    using Common.Enums;

    using Prism.Commands;
    using Prism.Mvvm;

    class TextEditorViewModel : BindableBase
    {
        #region Fields

        private bool _isTextValid;
        private readonly Router _router;
        private AbonentType _target = AbonentType.Server;
        private string _text;

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

        public bool IsTextValid
        {
            get => _isTextValid;
            set => SetProperty(ref _isTextValid, value);
        }

        public ICommand SendCommand => new DelegateCommand(ExecuteSendCommand);
        public ICommand FormatCommand => new DelegateCommand(ExecuteFormatCommand);

        #endregion

        #region Constructors

        public TextEditorViewModel(Router router)
        {
            _router = router;
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

        private void ExecuteSendCommand()
        {
            _router.Send(Target, _text);
        }

        #endregion
    }
}
