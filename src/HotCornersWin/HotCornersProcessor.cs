using System.Diagnostics;

namespace HotCornersWin
{
    /// <summary>
    /// Performs hot corners monitoring and 
    /// triggers the corresponding events.
    /// </summary>
    public class HotCornersProcessor : IDisposable
    {
        public delegate void ProcessingEnabledChangedHandler(bool enabled);
        
        /// <summary>
        /// Invoked on hot corners monitoring enable/disable.
        /// </summary>
        public event ProcessingEnabledChangedHandler? StateChanged;

        private readonly ICornersSettingsHelper _cornersSettingsHelper;

        private readonly System.Timers.Timer _timer;
        private readonly MouseHook _mouseHook;
        private int _pollCyclesCounter = 0;
        private Corners _lastTestCorner = Corners.None;
        private bool _blockStateChanged = false;
        private bool _blocked = false;

        /// <summary>
        /// Enable or disable hot corners monitoring.
        /// </summary>
        public bool Enabled
        {
            get { return _timer.Enabled; }
            set
            {
                if (_timer.Enabled != value)
                {
                    _timer.Enabled = value;
                }
                StateChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// The cursor location update frequency, ms.
        /// Must be greater than zero.
        /// </summary>
        public int PollInterval
        {
            get { return (int)_timer.Interval; }
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
                _timer.Interval = value;
            }
        }

        /// <summary>
        /// Auto-disable hot corners monitoring when a full-screen up is running.
        /// </summary>
        public bool DisableOnFullscreen {  get; set; } = false;

        public HotCornersProcessor(ICornersSettingsHelper cornersSettingsHelper)
        {
            _cornersSettingsHelper = cornersSettingsHelper;
            _mouseHook = new();
            _timer = new()
            {
                Interval = 75,
                Enabled = false
            };
            _timer.Elapsed += OnTimerElapsed;
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            // Disable operation if something's running in a full screen mode.
            if (DisableOnFullscreen)
            {
                if (ScreenInfoHelper.GetFullscreenState() != FullscreenState.NoFullscreen)
                {
                    if (!_blockStateChanged)
                    {
                        _blockStateChanged = true;
                        if (ScreenInfoHelper.DetectOverlayWindows())
                        {
                            Debug.WriteLine("An overlay is on top, ignoring"); // TODO remove debug
                        }
                        else
                        {
                            Debug.WriteLine("Blocked by a fullscreen app"); // TODO remove debug
                            StateChanged?.Invoke(enabled: false);
                            _blocked = true;
                        }
                    }
                }
                else
                {
                    if (_blockStateChanged)
                    {
                        Debug.WriteLine("Unblocked"); // TODO remove debug
                        _blockStateChanged = false;
                        StateChanged?.Invoke(enabled: true);
                        _blocked = false;
                    }
                }
                if (_blocked)
                {
                    return;
                }
            }

            // Ignore cursor movements when a button is pressed (dragging).
            // TODO introduce an option in Settings
            if (_mouseHook.IsMouseButtonPressed)
            {
                return;
            }

            // Hit test cursor
            Corners currentCorner = CornersHitTester.HitTest(_mouseHook.CursorPosition);
            if (currentCorner == Corners.None)
            {
                // a miss; reset counter and exit
                _lastTestCorner = Corners.None;
                _pollCyclesCounter = 0;
                return;
            }
            // a hit:
            // if the corner was already hit in previous cycles,
            // wait for a delay to expire and fire the correspondent action;
            // if the delay has already expired, do nothing to avoid repetitive 
            // action invocation while the cursor stays still
            Debug.WriteLine($"Hit at {currentCorner}"); // TODO remove debug
            if (_lastTestCorner == currentCorner)
            {
                _pollCyclesCounter++;
            }
            else
            {
                _pollCyclesCounter = 0;
            }
            if (_pollCyclesCounter == _cornersSettingsHelper.GetDelay(currentCorner))
            {
                Debug.WriteLine($"Action at {currentCorner} after {_pollCyclesCounter} polls"); // TODO remove debug
                try
                {
                    _cornersSettingsHelper.GetAction(currentCorner).Invoke();
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show(
                    ex.Message,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            _lastTestCorner = currentCorner;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Stop();
                _timer.Elapsed -= OnTimerElapsed;
                _timer.Dispose();
                _mouseHook?.Dispose();
            }
        }
    }
}
