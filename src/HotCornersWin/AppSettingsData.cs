namespace HotCornersWin;

/// <summary>
/// Application settings data storage class.
/// </summary>
public class AppSettingsData
{
    /// <summary>
    /// True if the application is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Action triggered for the left top corner of the screen.
    /// </summary>
    public string LeftTop { get; set; } = string.Empty;

    /// <summary>
    /// Action triggered for the right top corner of the screen.
    /// </summary>
    public string RightTop { get; set; } = string.Empty;

    /// <summary>
    /// Action triggered for the right bottom corner of the screen.
    /// </summary>
    public string RightBottom { get; set; } = string.Empty;

    /// <summary>
    /// Action triggered for the left bottom corner of the screen.
    /// </summary>
    public string LeftBottom { get; set; } = string.Empty;

    /// <summary>
    /// App behavior in a multi-monitor system.
    /// </summary>
    public MultiMonCfg MultiMonCfg { get; set; } = MultiMonCfg.Virtual;

    /// <summary>
    /// Corner sensitive area radius.
    /// </summary>
    public int AreaSize { get; set; } = 5;

    /// <summary>
    /// Pause duration between consequent mouse cursor position updates; ms.
    /// </summary>
    public int PollInterval { get; set; } = 75;

    /// <summary>
    /// True if the application needs to detect a fullscreen application and disable itself.
    /// </summary>
    public bool DisableOnFullscreen { get; set; }

    /// <summary>
    /// Number of poll intervals before triggering an action for the left top corner of the screen.
    /// </summary>
    public int DelayLT { get; set; }

    /// <summary>
    /// Number of poll intervals before triggering an action for the right top corner of the screen.
    /// </summary>
    public int DelayRT { get; set; }

    /// <summary>
    /// Number of poll intervals before triggering an action for the left bottom corner of the screen.
    /// </summary>
    public int DelayLB { get; set; }

    /// <summary>
    /// Number of poll intervals before triggering an action for the right bottom corner of the screen.
    /// </summary>
    public int DelayRB { get; set; }

    /// <summary>
    /// Serialized string containing custom actions definitions (list of CustomAction objects).
    /// </summary>
    public string CustomActions { get; set; } = string.Empty;

    /// <summary>
    /// The app color mode: dark, light or system-dependent.
    /// </summary>
    public SystemColorMode ColorScheme { get; set; } = SystemColorMode.System;
}
