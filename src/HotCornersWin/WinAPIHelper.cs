using System.Runtime.InteropServices;

namespace HotCornersWin
{
    /// <summary>
    /// Helper class to call Windows API functions.
    /// </summary>
    public static partial class WinAPIHelper
    {
        /// <summary>
        /// Locks the workstation and dispay Windows logon screen.
        /// </summary>
        /// <returns>true if the workstation was successfully locked</returns>
        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool LockWorkStation();
    }

    // TODO move WinAPI definitions from ScreenInfoHelper here
}
