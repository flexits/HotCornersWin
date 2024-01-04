using Gma.System.MouseKeyHook;
using System.Timers;

namespace HotCornersWin
{
    /// <summary>
    /// Polls the mouse cursor with given intervals and generates events 
    /// with current cursor coordinates. The events will not be generated 
    /// while a mouse button is being held down.
    /// </summary>
    public class MouseHook : IDisposable
    {
        public delegate void CursorCoordinatesUpdateHandler(Point coords);

        /// <summary>
        /// Invoked on every mouse cursor coordinates poll.
        /// </summary>
        public event CursorCoordinatesUpdateHandler? CoordinatesUpdated;

        /// <summary>
        /// Enable or disable cursor polling (does nothing if disabled).
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
                        _keyboardMouseEvents.MouseDown += OnMouseDown;
                        _keyboardMouseEvents.MouseUp += OnMouseUp;
                        _timer.Start();
                    }
                    else
                    {
                        _keyboardMouseEvents.MouseDown -= OnMouseDown;
                        _keyboardMouseEvents.MouseUp -= OnMouseUp;
                        _timer.Stop();
                    }
                }
            }
        }

        private bool _enabled = false;

        private readonly IKeyboardMouseEvents _keyboardMouseEvents;

        private readonly System.Timers.Timer _timer;

        private bool _isMouseBtnReleased = true;

        public MouseHook(int pollInterval = 100)
        {
            if (pollInterval <= 0)
            {
                pollInterval = 1;
            }
            _keyboardMouseEvents = Hook.GlobalEvents();
            _timer = new System.Timers.Timer()
            { 
                AutoReset = true,
                Interval = pollInterval
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
            _keyboardMouseEvents.MouseDown -= OnMouseDown;
            _keyboardMouseEvents.MouseUp -= OnMouseUp;
            _keyboardMouseEvents.Dispose();
        }

        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            _isMouseBtnReleased = false;
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            _isMouseBtnReleased = true;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (_isMouseBtnReleased)
            {
                CoordinatesUpdated?.Invoke(Cursor.Position);
            }
        }
    }
}
