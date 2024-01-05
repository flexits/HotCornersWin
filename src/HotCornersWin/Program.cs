using System.Diagnostics;
using System.Reflection;

namespace HotCornersWin
{
    // TODO consider autorun through registry, https://gist.github.com/HelBorn/2266242
    // TODO custom actions (commands)
    // TODO custom actions (hotkeys)

    internal static class Program
    {
        private static readonly NotifyIcon _notifyIcon;
        private static readonly ToolStripMenuItem _menuItemSwitch;
        private static readonly MouseHook _mouseHook;
        private static readonly System.Timers.Timer _timer;

        private const int ACTION_DELAY = 0; // TODO introduce an option in Settings

        private static readonly Form _dummyForm; // for accessing the UI thread

        /// <summary>
        /// The flag is used to distinguish single and double clicks.
        /// It is set when a click occurs and must be reset 
        /// when either a single or double click is handled.
        /// </summary>
        private static bool _wasIconClicked = false;

        private static int _pollCyclesCounter = 0;
        private static Corners _lastTestCorner = Corners.None;

        private static bool _enabled = false;

        /// <summary>
        /// The app's state flag. On set, changes the app's icon, 
        /// menu switch and tooltip according to its state.
        /// Doesn't actually impact the main timer!
        /// </summary>
        private static bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                _menuItemSwitch.Checked = _enabled;
                _notifyIcon.Icon = _enabled ? Properties.Resources.icon_new_on : Properties.Resources.icon_new_off;
                string tooltip = _enabled ? Properties.Resources.strEnabled : Properties.Resources.strDisabled;
                _notifyIcon.Text = $"HotCornersWin ({tooltip})";
            }
        }

        static Program()
        {
            ApplicationConfiguration.Initialize();

            _menuItemSwitch = new(Properties.Resources.strMenuEnabled)
            {
                CheckOnClick = true
            };
            _menuItemSwitch.Click += NotifyIcon_DoubleClick;

            ToolStripMenuItem menuItemSettings = new(Properties.Resources.strMenuSettings);
            menuItemSettings.Click += MenuItemSettings_Click;

            ToolStripMenuItem menuItemAbout = new(Properties.Resources.strMenuAbout);
            menuItemAbout.Click += MenuItemAbout_Click;

            ToolStripMenuItem menuItemQuit = new(Properties.Resources.strMenuQuit);
            menuItemQuit.Click += MenuItemQuit_Click;

            _notifyIcon = new()
            {
                ContextMenuStrip = new()
                {
                    Items =
                    {
                        _menuItemSwitch,
                        menuItemSettings,
                        menuItemAbout,
                        new ToolStripSeparator(),
                        menuItemQuit
                    }
                },
                Visible = true
            };
            _notifyIcon.MouseClick += NotifyIcon_MouseClick;
            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            _mouseHook = new();

            _timer = new(75);
            _timer.Elapsed += OnTimerElapsed;

            _dummyForm = new Form()
            {
                TopMost = false,
                WindowState = FormWindowState.Minimized
            };
            _ = _dummyForm.Handle;
        }

        [STAThread]
        static void Main()
        {
            // Limit running instances to one
            if (Assembly.GetEntryAssembly() is Assembly assembly)
            {
                var pName = Path.GetFileNameWithoutExtension(assembly.Location);
                if (Process.GetProcessesByName(pName).Length > 1)
                {
                    Environment.Exit(0);
                }
            }

            // Get monitor configuration according to the app's settings.
            var screens = GetScreens();
            if (screens.Length == 0)
            {
                _ = MessageBox.Show(Properties.Resources.strBoundsErr,
                    Properties.Resources.strFatalErr,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // Init mouse movement processing on the selected monitor configuration.
            HotCornersHelper.Screens = screens;
            HotCornersHelper.CornerRadius = Properties.Settings.Default.AreaSize;

            // Set polling interval
            _timer.Interval = Properties.Settings.Default.PollInterval;

            // Enable or disable operation according to the settings.
            Enabled = Properties.Settings.Default.IsEnabled;
            _timer.Enabled = Enabled;

            Application.Run();
        }

        private static void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            // Disable operation if something's running in a full screen mode.
            if (Properties.Settings.Default.AutoFullscreen)
            {
                FullscreenState fullscreenState = ScreenInfoHelper.GetFullscreenState();
                if (fullscreenState == FullscreenState.NoFullscreen)
                {
                    // operation allowed, check if the app is already running
                    if (!Enabled)
                    {
                        _ = _dummyForm.BeginInvoke(new Action(() => Enabled = true));
                        Debug.WriteLine($"Fullscreen state changed to {fullscreenState}, the app's enabled"); // TODO remove debug
                    }
                }
                else
                {
                    // operation not allowed, disable the app if not already and quit
                    if (Enabled)
                    {
                        _ = _dummyForm.BeginInvoke(new Action(() => Enabled = false));
                        Debug.WriteLine($"Fullscreen state changed to {fullscreenState}, the app's disabled"); // TODO remove debug
                    }
                    return;
                }
            }

            // Ignore cursor movements when a button is pressed (dragging).
            if (_mouseHook.IsMouseButtonPressed)
            {
                // TODO introduce an option in Settings
                return;
            }

            // Hit test cursor
            Corners currentCorner = HotCornersHelper.HitTest(_mouseHook.CursorPosition);
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
            if (_pollCyclesCounter == ACTION_DELAY) // TODO settings
            {
                Debug.WriteLine($"Action at {currentCorner} after {_pollCyclesCounter} polls"); // TODO remove debug
                ActionCaller.ExecuteAction(currentCorner);
            }
            _lastTestCorner = currentCorner;
        }

        /// <summary>
        /// Get system screen information according to the app's settings.
        /// </summary>
        /// <returns>An array of rectangles representing screens' 
        /// locations and dimensions.</returns>
        private static Rectangle[] GetScreens()
        {
            MultiMonCfg moncfg = MultiMonCfg.Primary;
            if (Enum.IsDefined(typeof(MultiMonCfg), Properties.Settings.Default.MultiMonCfg))
            {
                moncfg = (MultiMonCfg)Properties.Settings.Default.MultiMonCfg;
            }
            return ScreenInfoHelper.GetScreensInfo(moncfg);
        }

        private static async void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            // if the clicked flag is set, this is a second click - do nothing
            if (_wasIconClicked)
            {
                return;
            }
            // if the clicked flag isn't set, this is a first click -
            // set the flag and wait for a possible double click
            _wasIconClicked = true;
            await Task.Delay(SystemInformation.DoubleClickTime + 1);
            // check if the flag was reset, if no -
            // then there was no double click handled, reset the flag and
            // execute single click actions
            if (_wasIconClicked)
            {
                _wasIconClicked = false;
                // show Settings on single click
                MenuItemSettings_Click(sender, e);
            }
        }

        private static void NotifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            // reset the clicked flag
            _wasIconClicked = false;
            // toggle enable on double click and save changes
            Enabled = !Enabled;
            _timer.Enabled = Enabled;
            Properties.Settings.Default.IsEnabled = Enabled;
            Properties.Settings.Default.Save();
        }

        private static void MenuItemSettings_Click(object? sender, EventArgs e)
        {
            FormSettings fs = new()
            {
                Icon = Properties.Resources.icon_new_on,
                Text = Properties.Resources.FormSettingsText,
                TopMost = true,
            };
            // if settings were changed, apply
            if (fs.ShowDialog() == DialogResult.OK)
            {
                HotCornersHelper.CornerRadius = Properties.Settings.Default.AreaSize;
                HotCornersHelper.Screens = GetScreens();
                ActionCaller.ReloadSettings();
            }
        }

        private static void MenuItemAbout_Click(object? sender, EventArgs e)
        {
            _ = new FormAbout()
            {
                Icon = Properties.Resources.icon_new_on,
                Text = Properties.Resources.FormAboutText,
                TopMost = true,
            }
            .ShowDialog();
        }

        private static void MenuItemQuit_Click(object? sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Elapsed -= OnTimerElapsed;
            _timer.Dispose();
            _mouseHook?.Dispose();
            _notifyIcon?.Dispose();
            Application.Exit();
        }
    }
}