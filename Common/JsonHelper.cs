namespace Common
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    public static class JsonHelper
    {
        public static T SafeReadFromFile<T>(string fullName) where T : new()
        {
            if (File.Exists(fullName))
            {
                try
                {
                    var deserializeObject = JsonConvert.DeserializeObject<T>(File.ReadAllText(fullName));
                    return deserializeObject == null ? new T() : deserializeObject;
                }
                catch (Exception e)
                {
                    return new T();
#if DEBUG
                    throw;
#endif
                }
            }

            return new T();
        }

        public static void SafeSaveToFile<T>(string folderName, string shortFileName, T settingsObject)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            string fullFileName = $"{folderName}\\{shortFileName}";

            if (settingsObject != null)
            {
                try
                {
                    File.WriteAllText(fullFileName, JsonConvert.SerializeObject(settingsObject, Formatting.Indented));
                }
                catch (Exception e)
                {
#if DEBUG
                    throw;
#endif
                }
            }
        }
    }
}
