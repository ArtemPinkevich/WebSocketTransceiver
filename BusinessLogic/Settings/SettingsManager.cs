namespace BusinessLogic.Settings
{
    using System.Collections.Generic;

    using Common;
    using Common.GlobalEvents;

    using Prism.Events;

    public class SettingsManager : ISettingsManager
    {
        #region Constants

        private const string FOLDER_NAME = "settings";

        #endregion

        #region Fields

        private readonly Dictionary<string, object> _settins = new Dictionary<string, object>();

        #endregion

        #region Constructors

        public SettingsManager(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<AppClosingEvent>().Subscribe(HandleOnAppClosing);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns settings object from file if it is not found in memory
        /// </summary>
        public T GetSettings<T>(string fileName)
            where T : new()
        {
            if (_settins.TryGetValue(fileName, out var settingsObject))
            {
                if (settingsObject is T)
                {
                    return (T)settingsObject;
                }
            }

            var settings = JsonHelper.SafeReadFromFile<T>($"{FOLDER_NAME}/{fileName}");

            _settins.Add(fileName, settings);

            return settings;
        }

        /// <summary>
        ///     Update settings in memory
        /// </summary>
        public void UpdateSettings<T>(string fileNameAsKey, T settings)
        {
            if (_settins.ContainsKey(fileNameAsKey))
            {
                _settins[fileNameAsKey] = settings;
            }
            else
            {
                _settins.Add(fileNameAsKey, settings);
            }
        }

        public void Save<T>(string fileName, T settings)
        {
            // TODO improve!!
            UpdateSettings(fileName, settings);
            JsonHelper.SafeSaveToFile(FOLDER_NAME, fileName, settings);
        }

        private void HandleOnAppClosing()
        {
            Save();
        }

        private void Save()
        {
            foreach (KeyValuePair<string, object> item in _settins)
                JsonHelper.SafeSaveToFile(FOLDER_NAME, item.Key, item.Value);
        }

        #endregion
    }
}
