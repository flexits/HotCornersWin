namespace HotCornersWin
{
    /// <summary>
    /// A helper class to process the system screens information.
    /// </summary>
    public static class ScreenInfoHelper
    {
        /// <summary>
        /// Get system screen information according to the specified multi-monitor behavior scenario.
        /// </summary>
        /// <returns>An array of rectangles representing screens' locations and dimensions.
        /// The array will be empty on any error.</returns>
        public static Rectangle[] GetScreens(MultiMonCfg moncfg)
        {
            switch (moncfg)
            {
                case MultiMonCfg.Virtual:
                    return new Rectangle[] { SystemInformation.VirtualScreen };
                case MultiMonCfg.Primary:
                    Rectangle? bounds = Screen.PrimaryScreen?.Bounds;
                    if (bounds is not null)
                    {
                        return new Rectangle[] { (Rectangle)bounds };
                    }
                    break;
                case MultiMonCfg.Separate:
                    return Screen.AllScreens.Select(s => s.Bounds).ToArray();
            }
            return Array.Empty<Rectangle>();
        }
    }
}
