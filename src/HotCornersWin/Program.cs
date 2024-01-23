using System.Diagnostics;
using System.Reflection;

namespace HotCornersWin
{
    // TODO consider autorun through registry, https://gist.github.com/HelBorn/2266242
    // TODO custom actions (hotkeys)

    internal static class Program
    {
        private static readonly NotifyIcon _notifyIcon;
        private static readonly ToolStripMenuItem _menuItemSwitch;

        private static readonly HotCornersProcessor _cornersProcessor;

        private static readonly Form _dummyForm; // for accessing the UI thread

        /// <summary>
        /// The flag is used to distinguish single and double clicks.
        /// It is set when a click occurs and must be reset 
        /// when either a single or double click is handled.
        /// </summary>
        private static bool _wasIconClicked = false;

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

            _cornersProcessor = new();
            _cornersProcessor.StateChanged += CornersProcessorStateChanged;

            _dummyForm = new Form()
            {
                TopMost = false,
                WindowState = FormWindowState.Minimized
            };
            _ = _dummyForm.Handle;

            CornersProcessorStateChanged(false);
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
            CornersHitTester.Screens = screens;
            CornersHitTester.CornerRadius = Properties.Settings.Default.AreaSize;

            // Set polling interval
            _cornersProcessor.PollInterval = Properties.Settings.Default.PollInterval;

            // Enable or disable operation according to the settings.
            _cornersProcessor.DisableOnFullscreen = Properties.Settings.Default.DisableOnFullscreen;
            _cornersProcessor.Enabled = Properties.Settings.Default.IsEnabled;

            Application.Run();
        }

        /// <summary>
        /// Adjust the UI according with the app's state - running or disabled.
        /// </summary>
        private static void CornersProcessorStateChanged(bool enabled)
        {
            _menuItemSwitch.Checked = enabled;
            _notifyIcon.Icon = enabled ? Properties.Resources.icon_new_on : Properties.Resources.icon_new_off;
            string tooltip = enabled ? Properties.Resources.strEnabled : Properties.Resources.strDisabled;
            _notifyIcon.Text = $"HotCornersWin ({tooltip})";
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
                OnIconSingleClick();
            }
        }

        private static void NotifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            // reset the clicked flag
            _wasIconClicked = false;
            OnIconDoubleClick();
        }

        private static void OnIconSingleClick()
        {
            // toggle enable on double click and save changes
            _cornersProcessor.Enabled = !_cornersProcessor.Enabled;
            Properties.Settings.Default.IsEnabled = _cornersProcessor.Enabled;
            Properties.Settings.Default.Save();
        }

        private static void OnIconDoubleClick()
        {
            // show Settings on double click
            MenuItemSettings_Click(null, new());
        }

        private static void MenuItemSettings_Click(object? sender, EventArgs e)
        {
            _ = new FormSettings()
            {
                Icon = Properties.Resources.icon_new_on,
                Text = Properties.Resources.FormSettingsText,
                TopMost = true,
                CornersProcessor = _cornersProcessor,
            }.ShowDialog();
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
            _cornersProcessor.Dispose();
            _notifyIcon?.Dispose();
            Application.Exit();
        }
    }
}