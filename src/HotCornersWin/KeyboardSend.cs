using System.Runtime.InteropServices;

namespace HotCornersWin
{
    /// <summary>
    /// Helper class to sent keystrokes to OS using WinAPI.
    /// </summary>
    public static class KeyboardSend
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_EXTENDEDKEY = 1;
        private const int KEYEVENTF_KEYUP = 2;

        /// <summary>
        /// Virtually press a key.
        /// Don't forget to release by calling KeyUp() with the same keycode.
        /// </summary>
        /// <param name="vKey">Keycode of the key to be pressed.</param>
        public static void KeyDown(Keys vKey)
        {
            keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }

        /// <summary>
        /// Virtually release a key.
        /// </summary>
        /// <param name="vKey">Keycode of the key to be released.</param>
        public static void KeyUp(Keys vKey)
        {
            keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

        // https://stackoverflow.com/a/6407690
    }
}
