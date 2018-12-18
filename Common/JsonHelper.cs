namespace Common
{
    using System;
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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

        public static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return false;
            }

            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") ||   // For object
                strInput.StartsWith("[") && strInput.EndsWith("]"))     // For array
            {
                try
                {
                    JToken obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        public static string RestructJson(string strInput)
        {
            try
            {
                object parsedJson = JsonConvert.DeserializeObject(strInput);
                string fixedJson = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                return fixedJson;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return strInput;
            }
        }
    }
}
