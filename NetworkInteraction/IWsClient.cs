// ---------------------------------------------------------------------------------------------------------------------------------------------------
namespace NetworkInteraction
{
    using System;

    public interface IWsClient
    {
        event EventHandler<PackageReceivedEventArgs> OnMessageReceived;
        event EventHandler OnConnected;
        event EventHandler OnClosed;

        void Connect(string ip, string port);
        void SendMessage(string data);
        void Disconnect();
    }
}
