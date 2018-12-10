namespace ConnectionModule.BusinessLogic
{
    using System;

    using Settings;

    public interface IConnectionMaker
    {
        event EventHandler OnConnectionBroken;
        event EventHandler OnConnected;

        void Connect(ConnectionSettings connectionSettings);
        void Disconnect();
        ConnectionSettings GetSettings();
    }
}
