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
    /// Monitors the state of the computer in a background thread
    /// and invokes an event when something enters or leaves 
    /// the fullscreen mode.
    /// </summary>
    public class FullscreenMonitor
    {
        public delegate void FullscreenStateChanged(FullscreenState state);

        /// <summary>
        /// The event will be generated if something enters 
        /// a fullscreen mode and/or leaves a fullscreen mode.
        /// </summary>
        public event FullscreenStateChanged? OnFullscreenStateChanged;

        /// <summary>
        /// If enabled, starts monitoring of any fullscreen activity.
        /// Generates OnFullscreenStateChanged event immediately after 
        /// being enabled and then every time the fullscreen state changes. 
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    if (_enabled)
                    {
                        _ = Task.Run(PollingCycle);
                    }
                }
            }
        }

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

        [DllImport("Shell32.dll")]
        private static extern int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE state);

        private bool _enabled;
        private int _pollInterval;

        /// <summary>
        /// Stores the state from the last polling cycle.
        /// </summary>
        private FullscreenState _state;

        public FullscreenMonitor(int pollInterval = 100)
        {
            if (pollInterval <= 0)
            {
                pollInterval = 1;
            }
            _pollInterval = pollInterval;
            _enabled = false;
            _state = FullscreenState.Undefined;
        }

        /// <summary>
        /// While Enabled == true, calls SHQueryUserNotificationState() to find out 
        /// if a fullscreen activity is taking place and updates the _state. 
        /// If the _state has changed, invokes an OnFullscreenStateChanged event.
        /// Sleeps for _pollInterval milliseconds between subsequent polls.
        /// </summary>
        private void PollingCycle()
        {
            QUERY_USER_NOTIFICATION_STATE qnsState = QUERY_USER_NOTIFICATION_STATE.QUNS_NOT_PRESENT;
            FullscreenState newState = FullscreenState.Undefined;
            while (Enabled)
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
        }
    }
}
