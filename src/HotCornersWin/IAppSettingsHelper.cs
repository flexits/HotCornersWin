namespace HotCornersWin
{
    /// <summary>
    /// Provides access to the application settings
    /// and manages access to the persistent storage.
    /// </summary>
    public interface IAppSettingsHelper
    {
        /// <summary>
        /// Application settings.
        /// </summary>
        AppSettingsData Settings { get; }

        /// <summary>
        /// Reloads the settings from a persistent storage.
        /// </summary>
        void Reload();

        /// <summary>
        /// Saves current settings to a persistent storage.
        /// </summary>
        void Save();
    }
}