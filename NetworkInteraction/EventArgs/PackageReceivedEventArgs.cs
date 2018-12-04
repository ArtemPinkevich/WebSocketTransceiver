namespace NetworkInteraction
{
    using System;

    public class PackageReceivedEventArgs : EventArgs
    {
        public string JsonString { get; }

        public PackageReceivedEventArgs(string jsonString)
        {
            JsonString = jsonString;
        }
    }
}
