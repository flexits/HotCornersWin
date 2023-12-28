using Gma.System.MouseKeyHook;

namespace HotCornersWin
{
    public class MouseHook : IDisposable
    {
        public delegate void MouseActionHandler(Point coords);

        public event MouseActionHandler? Move;

        private readonly IKeyboardMouseEvents _keyboardMouseEvents;

        private bool _enabled = false;

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
                        _keyboardMouseEvents.MouseMove += MouseHook_MouseMove;
                    }
                    else
                    {
                        _keyboardMouseEvents.MouseMove -= MouseHook_MouseMove;
                    }
                }
            }
        }

        public MouseHook()
        {
            _keyboardMouseEvents = Hook.GlobalEvents();
        }

        private void MouseHook_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.None)
            {
                // TODO doesnt work
                return;
            }
            Move?.Invoke(e.Location);
        }

        public void Dispose()
        {
            _keyboardMouseEvents.MouseMove -= MouseHook_MouseMove;
            _keyboardMouseEvents.Dispose();
        }

        // own low-level implementation
        //https://github.com/rvknth043/Global-Low-Level-Key-Board-And-Mouse-Hook
    }
}
