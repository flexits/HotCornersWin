namespace HotCornersWin;

/// <summary>
/// Wrapper around the app's settings providing access 
/// to hot corners' actions and delays.
/// </summary>
public interface ICornersSettingsHelper
{
    /// <summary>
    /// Custom actions created by user - get from settings, save to settings.
    /// </summary>
    List<CustomAction> CustomActions { get; set; }

    /// <summary>
    /// Get the action associated with the given hot corner.
    /// </summary>
    Action GetAction(Corners corner);

    /// <summary>
    /// Returns an array of human-readable names for all available actions.
    /// </summary>
    string[] GetActionNames();

    /// <summary>
    /// Get the action trigger delay for the given corner.
    /// </summary>
    int GetDelay(Corners corner);

    /// <summary>
    /// Reload hot corner settings (delays and actions) from the app's settings.
    /// </summary>
    void ReloadSettings();
}