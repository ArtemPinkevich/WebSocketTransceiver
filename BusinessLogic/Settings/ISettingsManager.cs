namespace BusinessLogic.Settings
{
    public interface ISettingsManager
    {
        T GetSettings<T>(string fileName, bool fromMemoryIfExist = true) where T : new();
        void UpdateSettings<T>(string fileNameAsKey, T settings);
        void Save<T>(string fileName, T settings);
    }
}
