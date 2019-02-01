namespace BusinessLogic.Settings
{
    using System.Collections.Generic;

    using Common;
    using Common.GlobalEvents;

    using Prism.Events;

    public class SettingsManager : ISettingsManager
    {
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
        public T GetSettings<T>(string fileName, bool fromMemoryIfExist = true)
            where T : new()
        {
            if (fromMemoryIfExist && _settins.TryGetValue(fileName, out var settingsObject))
            {
                if (settingsObject is T)
                {
                    return (T)settingsObject;
                }
            }

            var settings = JsonHelper.SafeReadFromFile<T>(fileName);

            UpdateSettings(fileName, settings);

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
            JsonHelper.SafeSaveToFile(fileName, settings);
        }

        private void HandleOnAppClosing()
        {
            Save();
        }

        private void Save()
        {
            foreach (KeyValuePair<string, object> item in _settins)
                JsonHelper.SafeSaveToFile(item.Key, item.Value);
        }

        #endregion
    }
}
