using Gma.System.MouseKeyHook;
using System.Timers;

namespace HotCornersWin
{
    /// <summary>
    /// Tracks mouse cursor and generates events on its movement.
    /// </summary>
    public class MouseHook : IDisposable
    {
        public delegate void MouseActionHandler(Point coords);

        /// <summary>
        /// Invoked on mouse movement detection.
        /// </summary>
        public event MouseActionHandler? Move;

        private readonly IKeyboardMouseEvents _keyboardMouseEvents;

        private readonly System.Timers.Timer _timer;

        private bool _enabled = false;

        private bool _isMouseBtnReleased = true;

        /// <summary>
        /// Enable or disable mouse tracking (does nothing if disabled).
        /// </summary>
        public bool IsEnabled
        {
            get { return _enabled; }
            set 
            { 
                if (_enabled != value)
                {
                    _enabled = value;
                    if (_enabled)
                    {
                        //_keyboardMouseEvents.MouseMove += MouseHook_MouseMove;
                        _keyboardMouseEvents.MouseDown += MouseHook_MouseDown;
                        _keyboardMouseEvents.MouseUp += MouseHook_MouseUp;
                        _timer.Start();
                    }
                    else
                    {
                        //_keyboardMouseEvents.MouseMove -= MouseHook_MouseMove;
                        _keyboardMouseEvents.MouseDown -= MouseHook_MouseDown;
                        _keyboardMouseEvents.MouseUp -= MouseHook_MouseUp;
                        _timer.Stop();
                    }
                }
            }
        }

        public MouseHook()
        {
            _keyboardMouseEvents = Hook.GlobalEvents();
            _timer = new System.Timers.Timer()
            { 
                AutoReset = true,
                Interval = 75
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        private void MouseHook_MouseUp(object? sender, MouseEventArgs e)
        {
            _isMouseBtnReleased = true;
        }

        private void MouseHook_MouseDown(object? sender, MouseEventArgs e)
        {
            _isMouseBtnReleased = false;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            if (_isMouseBtnReleased)
            {
                
                Move?.Invoke(Cursor.Position);
            }
        }

        /*
        private void MouseHook_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isMouseBtnReleased)
            {
                Move?.Invoke(e.Location);
            }
        }
        */

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
            //_keyboardMouseEvents.MouseMove -= MouseHook_MouseMove;
            _keyboardMouseEvents.MouseDown -= MouseHook_MouseDown;
            _keyboardMouseEvents.MouseUp -= MouseHook_MouseUp;
            _keyboardMouseEvents.Dispose();
        }

        // own low-level implementation example
        // https://github.com/rvknth043/Global-Low-Level-Key-Board-And-Mouse-Hook
    }
}
