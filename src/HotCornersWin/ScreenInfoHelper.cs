using System.Buffers;
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
        internal enum QUERY_USER_NOTIFICATION_STATE
        {
            QUNS_NOT_PRESENT = 1,
            QUNS_BUSY = 2,
            QUNS_RUNNING_D3D_FULL_SCREEN = 3,
            QUNS_PRESENTATION_MODE = 4,
            QUNS_ACCEPTS_NOTIFICATIONS = 5,
            QUNS_QUIET_TIME = 6,
            QUNS_APP = 7
        };

        internal enum GW_PARAMS
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        };

        internal const int HRESULT_S_OK = 0;

        [LibraryImport("Shell32.dll")]
        internal static partial int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE state);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int UpperLeftX;
            public int UpperLeftY;
            public int LowerRightX;
            public int LowerRightY;
        }

        [LibraryImport("User32.dll")]
        internal static partial IntPtr GetDesktopWindow();

        [LibraryImport("User32.dll")]
        internal static partial IntPtr GetTopWindow(IntPtr parent);

        [LibraryImport("User32.dll")]
        internal static partial IntPtr GetWindow(IntPtr hWnd, GW_PARAMS uCmd);

        [LibraryImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [LibraryImport("User32.dll", StringMarshalling = StringMarshalling.Utf16)]
        internal static partial int GetWindowTextW(IntPtr hWnd, [Out] char[] lpString, int nMaxCount);

        [LibraryImport("User32.dll", StringMarshalling = StringMarshalling.Utf16)]
        internal static partial uint GetWindowModuleFileNameW(IntPtr hWnd, [Out] char[] pszFileName, uint cchFileNameMax);

        [LibraryImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool IsWindowVisible(IntPtr hWnd);


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
        /// Overlay windows compatibility workaroud
        /// for nVidia Overlay and MS Text Input App:
        /// detect if any of theirs tranparent windows is topmost.
        /// </summary>
        /// <returns>True is the topmost fullscreen window 
        /// belongs to overlay, false in any other case.</returns>
        public static bool DetectOverlayWindows()
        {
            // obtain desktop window dimensions
            IntPtr desktopWndhandle = GetDesktopWindow();
            int width, height;
            if (GetWindowRect(desktopWndhandle, out var wndRect))
            {
                width = wndRect.LowerRightX - wndRect.UpperLeftX;
                height = wndRect.LowerRightY - wndRect.UpperLeftY;
                Debug.WriteLine($"Desktop is {width}X{height}");
            }
            else
            {
                return false;
            }

            // check all the topmost windows starting from the first in Z-order
            const int wndTextLength = 36;
            char[] wndTextBuffer = ArrayPool<char>.Shared.Rent(wndTextLength + 1);
            const int wndFileNameLength = 128;
            char[] wndFileNameBuffer = ArrayPool<char>.Shared.Rent(wndFileNameLength + 1);
            string wndText, wndFileName;
            IntPtr wndHandle = GetTopWindow(IntPtr.Zero);
            do
            {
                if (IsWindowVisible(wndHandle) && GetWindowRect(wndHandle, out wndRect))
                {
                    int wndWidth = wndRect.LowerRightX - wndRect.UpperLeftX;
                    int wndHeight = wndRect.LowerRightY - wndRect.UpperLeftY;
                    if (width <= wndWidth && height <= wndHeight)
                    {
                        // a fullscreen window is detected, get its parameters
                        // and check for known signatures
                        int wndTextLengthActual = GetWindowTextW(wndHandle, wndTextBuffer, wndTextLength);
                        if (wndTextLengthActual > 0)
                        {
                            wndText = new string(wndTextBuffer.Take(wndTextLengthActual).ToArray());
                        }
                        else
                        {
                            wndText = string.Empty;
                        }

                        if (wndText.Contains("GeForce Overlay", StringComparison.OrdinalIgnoreCase))
                        {
                            // nVidia GeForce Overlay window detected
                            return true;
                        }
                        if (wndText.Contains("Text Input Application", StringComparison.OrdinalIgnoreCase))
                        {
                            // Microsoft Text Input Application window detected
                            return true;
                        }

                        uint wndFileNameLengthActual = GetWindowModuleFileNameW(wndHandle, wndFileNameBuffer, wndFileNameLength);
                        if (wndFileNameLengthActual > 0)
                        {
                            wndFileName = new string(wndFileNameBuffer.Take((int)wndFileNameLengthActual).ToArray());
                        }
                        else
                        {
                            wndFileName = string.Empty;
                        }

                        if (wndTextLengthActual == 0 && wndFileName.EndsWith("shcore.dll", StringComparison.OrdinalIgnoreCase))
                        {
                            // this overlay window may be on top when Show desktop button at the end of the taskbar is clicked
                            return true;
                        }
                        if (wndText.Contains("Program Manager", StringComparison.OrdinalIgnoreCase) && wndFileName.EndsWith("SHELL32.dll", StringComparison.OrdinalIgnoreCase))
                        {
                            // Program Manager window detected
                            return true;
                        }

                        //Debug.WriteLine($"{wndHandle:X}; {wndText}; {wndFileName}; {wndWidth}X{wndHeight}");
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
