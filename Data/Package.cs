namespace Data
{
    using System;

    public class Package
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string JsonContent { get; set; } = string.Empty;

        public Package(string name)
        {
            Name = name;
        }
    }
}
