namespace NetworkInteraction
{
    using System;
    using System.Threading;

    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class WsServerService : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private string _prefix;


        public event EventHandler<PackageReceivedEventArgs> OnMessageReceived;
        public event EventHandler OnClosed;

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        private string getName()
        {
            string name = Context.QueryString["name"];
            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }


        public void SendMessage(string data)
        {
            Send(data);
        }

        protected override void OnOpen()
        {
            _name = getName();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            OnMessageReceived?.Invoke(this, new PackageReceivedEventArgs(e.Data));
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            OnClosed?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
            OnClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
