namespace BusinessLogic.Connection
{
    using System;

    public interface IConnectionMaker
    {
        event EventHandler OnConnectionBroken;
        event EventHandler OnConnected;

        void Connect(string ip, string port);
        void Disconnect();
    }
}
