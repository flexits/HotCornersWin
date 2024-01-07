using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HotCornersWin
{
    /// <summary>
    /// State of the computer for the current user.
    /// </summary>
    public enum FullscreenState
    {
        Undefined,

        /// <summary>
        /// Nothing is currently running in a full screen mode, 
        /// user notification is allowed.
        /// </summary>
        NoFullscreen,

        /// <summary>
        /// Something is running in a full screen mode or a 
        /// user notification is inappropriate due to other reasons.
        /// </summary>
        IsFullscreen,
    }

    /// <summary>
    /// A helper class to process the system screens information.
    /// </summary>
    public static partial class ScreenInfoHelper
    {
        private enum QUERY_USER_NOTIFICATION_STATE
        {
            QUNS_NOT_PRESENT = 1,
            QUNS_BUSY = 2,
            QUNS_RUNNING_D3D_FULL_SCREEN = 3,
            QUNS_PRESENTATION_MODE = 4,
            QUNS_ACCEPTS_NOTIFICATIONS = 5,
            QUNS_QUIET_TIME = 6,
            QUNS_APP = 7
        };

        private const int HRESULT_S_OK = 0;

        [LibraryImport("Shell32.dll")]
        private static partial int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE state);

        /// <summary>
        /// Get system screen information according to the specified 
        /// multi-monitor behavior scenario.
        /// </summary>
        /// <returns>An array of rectangles representing screens' 
        /// locations and dimensions.
        /// The array will be empty on any error.</returns>
        public static Rectangle[] GetScreensInfo(MultiMonCfg moncfg)
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

        /// <summary>
        /// Get the current computer screen state: is something running in a fullscreen mode 
        /// thus preventing user notifications or not.
        /// </summary>
        /// <returns>FullscreenState.NoFullscreen if a user notification is allowed, 
        /// FullscreenState.IsFullscreen otherwise, or 
        /// FullscreenState.Undefined in case of a failure.</returns>
        public static FullscreenState GetFullscreenState()
        {
            QUERY_USER_NOTIFICATION_STATE qnsState = QUERY_USER_NOTIFICATION_STATE.QUNS_NOT_PRESENT;
            if (SHQueryUserNotificationState(out qnsState) == HRESULT_S_OK)
            {
                switch (qnsState)
                {
                    case QUERY_USER_NOTIFICATION_STATE.QUNS_ACCEPTS_NOTIFICATIONS:
                    case QUERY_USER_NOTIFICATION_STATE.QUNS_QUIET_TIME:
                    case QUERY_USER_NOTIFICATION_STATE.QUNS_APP:
                        return FullscreenState.NoFullscreen;
                    default:
                        return FullscreenState.IsFullscreen;
                }
            }
            else
            {
                return FullscreenState.Undefined;
            }
        }
    }
}
