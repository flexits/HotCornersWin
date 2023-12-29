namespace HotCornersWin
{
    /// <summary>
    /// Behavior in a multi-monitor configuration.
    /// </summary>
    public enum MultiMonCfg
    {
        /// <summary>
        /// Hot corners on a single virtual monitor.
        /// </summary>
        Virtual,
        /// <summary>
        /// Hot corners on the primary monitor only.
        /// </summary>
        Primary,
        /// <summary>
        /// Each monitor has its own hot corners.
        /// </summary>
        Separate
    }
}
