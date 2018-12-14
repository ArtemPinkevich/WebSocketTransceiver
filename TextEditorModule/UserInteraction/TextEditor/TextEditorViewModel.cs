
namespace TextEditorModule.UserInteraction.TextEditor
{
    using System.Windows.Input;

    using BusinessLogic.Routing;

    using Common.Enums;

    using Prism.Commands;
    using Prism.Mvvm;

    class TextEditorViewModel : BindableBase
    {
        private readonly Router _router;
        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ICommand SendCommand => new DelegateCommand(ExecuteSendCommand);

        public TextEditorViewModel(Router router)
        {
            _router = router;
        }

        private void ExecuteSendCommand()
        {
            _router.Send(AbonentType.Server, _text);
        }
    }
}
