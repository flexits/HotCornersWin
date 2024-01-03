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
        /// Nothing is currently running in a full screen mode.
        /// </summary>
        NoFullscreen,

        /// <summary>
        /// Something is running in a full screen mode or a 
        /// user notification is inappropriate due to other reasons.
        /// </summary>
        IsFullscreen,
    }

    /// <summary>
    /// Monitors the state of the computer in a background thread
    /// and invokes an event when something enters or leaves 
    /// the fullscreen mode.
    /// </summary>
    public class FullscreenMonitor
    {
        public delegate void FullscreenStateChanged(FullscreenState state);

        public event FullscreenStateChanged? OnFullscreenStateChanged;

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

        private readonly int HRESULT_S_OK = 0;

        [DllImport("Shell32.dll")]
        private static extern int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE state);

        private bool _allowRun;
        private int _pollInterval;
        private FullscreenState _state;

        public FullscreenMonitor(int pollInterval = 100)
        {
            _pollInterval = pollInterval;
            _allowRun = false;
            _state = FullscreenState.Undefined;
        }

        public void Run()
        {
            _allowRun = true;
            QUERY_USER_NOTIFICATION_STATE qnsState = QUERY_USER_NOTIFICATION_STATE.QUNS_NOT_PRESENT;
            _ = Task.Run(() =>
            {
                FullscreenState newState = FullscreenState.Undefined;
                while (_allowRun)
                {
                    if (SHQueryUserNotificationState(out qnsState) == HRESULT_S_OK)
                    {
                        if (qnsState == QUERY_USER_NOTIFICATION_STATE.QUNS_ACCEPTS_NOTIFICATIONS)
                        {
                            newState = FullscreenState.NoFullscreen;
                        }
                        else
                        {
                            newState = FullscreenState.IsFullscreen;
                        }
                    }
                    else
                    {
                        newState = FullscreenState.Undefined;
                    }
                    if (newState != _state)
                    {
                        _state = newState;
                        OnFullscreenStateChanged?.Invoke(_state);
                    }
                    if (_pollInterval > 0)
                    {
                        Thread.Sleep(_pollInterval);
                    }
                }
            });
        }

        public void Stop()
        {
            _allowRun = false;
        }
    }
}
