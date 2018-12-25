namespace BusinessLogic.Settings
{
    public interface ISettingsManager
    {
        T GetSettings<T>(string fileName) where T : new();
        void UpdateSettings<T>(string fileNameAsKey, T settings);
        void Save<T>(string fileName, T settings);
    }
}
