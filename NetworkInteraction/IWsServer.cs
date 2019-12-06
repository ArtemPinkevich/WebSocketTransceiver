// ---------------------------------------------------------------------------------------------------------------------------------------------------
namespace NetworkInteraction
{
    using System;

    public interface IWsServer
    {
        event EventHandler<PackageReceivedEventArgs> OnMessageReceived;
        event EventHandler OnStopped;
        event EventHandler OnStarted;
        event EventHandler OnClosed;

        void StartListening(string ip, string port);
        void StopListening();
        void SendMessage(string message);
    }
}
