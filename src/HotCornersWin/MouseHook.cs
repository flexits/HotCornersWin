using Gma.System.MouseKeyHook;

namespace HotCornersWin
{
    /// <summary>
    /// Uses a mouse hook to monitor mouse events.
    /// </summary>
    public class MouseHook : IDisposable
    {
        /// <summary>
        /// True if a mouse button was pressed and not released, 
        /// False otherwise.
        /// </summary>
        public bool IsMouseButtonPressed { get; private set; }

        /// <summary>
        /// Current system cursor position in screen coordinates.
        /// </summary>
        public Point CursorPosition => Cursor.Position;

        private readonly IKeyboardMouseEvents _keyboardMouseEvents;

        public MouseHook()
        {
            _keyboardMouseEvents = Hook.GlobalEvents();
            _keyboardMouseEvents.MouseDown += (s, e) => IsMouseButtonPressed = true;
            _keyboardMouseEvents.MouseUp += (s, e) => IsMouseButtonPressed = false;
        }

        public void Dispose()
        {
            _keyboardMouseEvents.Dispose();
        }
    }
}
