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

        private enum GW_PARAMS
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        };

        private const int HRESULT_S_OK = 0;

        [LibraryImport("Shell32.dll")]
        private static partial int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE state);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int UpperLeftX;
            public int UpperLeftY;
            public int LowerRightX;
            public int LowerRightY;
        }

        [LibraryImport("User32.dll")]
        private static partial IntPtr GetTopWindow(IntPtr parent);

        [LibraryImport("User32.dll")]
        private static partial IntPtr GetWindow(IntPtr hWnd, GW_PARAMS uCmd);

        [LibraryImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [LibraryImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowVisible(IntPtr hWnd);


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
                    return [SystemInformation.VirtualScreen];
                case MultiMonCfg.Primary:
                    Rectangle? bounds = Screen.PrimaryScreen?.Bounds;
                    if (bounds is not null)
                    {
                        return [(Rectangle)bounds];
                    }
                    break;
                case MultiMonCfg.Separate:
                    return Screen.AllScreens.Select(s => s.Bounds).ToArray();
            }
            return [];
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
            if (SHQueryUserNotificationState(out var qnsState) == HRESULT_S_OK)
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

        /// <summary>
        /// nVidia Overlay compatibility workaroud: 
        /// detect if its tranparent window is topmost.
        /// </summary>
        /// <returns>True is the topmost fullscreen window 
        /// belongs to nVidia Overlay, false in any other case.</returns>
        public static bool DetectNvOverlay()
        {
            IntPtr nvWindowHandle = 0;

            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains("NVIDIA Overlay", StringComparison.OrdinalIgnoreCase))
                {
                    nvWindowHandle = process.MainWindowHandle;
                    break;
                }
            }
            if (nvWindowHandle == 0)
            {
                // no overlay window found
                Debug.WriteLine("No overlay detected");
                return false;
            }
            // the overlay window is present, get its dimensions
            // (they're equal to the screen size)
            int width, height;
            if (GetWindowRect(nvWindowHandle, out var wndRect))
            {
                width = wndRect.LowerRightX - wndRect.UpperLeftX;
                height = wndRect.LowerRightY - wndRect.UpperLeftY;
                Debug.WriteLine($"NV Overlay {width}X{height}");
            }
            else
            {
                // failed to get the overlay window size
                return false;
            }
            // check all the topmost windows starting from the first in Z-order
            // to find a visible fullscreen window
            IntPtr wndHandle = GetTopWindow(IntPtr.Zero);
            do
            {
                if (wndHandle == nvWindowHandle)
                {
                    // the overlay is topmost
                    Debug.WriteLine("NV Overlay detected!");
                    return true;
                }
                if (IsWindowVisible(wndHandle) && GetWindowRect(wndHandle, out wndRect))
                {
                    int wndWidth = wndRect.LowerRightX - wndRect.UpperLeftX;
                    int wndHeight = wndRect.LowerRightY - wndRect.UpperLeftY;
                    if (width <= wndWidth && height <= wndHeight)
                    {
                        // there's a fullsceen window on top of the overlay
                        Debug.WriteLine($"TOP: {wndHandle:X}; {wndWidth}X{wndHeight}");
                        return false;
                    }
                }
                wndHandle = GetWindow(wndHandle, GW_PARAMS.GW_HWNDNEXT);
            }
            while (wndHandle != IntPtr.Zero);
            
            return false;
        }
    }
}
