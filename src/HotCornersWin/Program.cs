using System.Diagnostics;
using System.Reflection;

namespace HotCornersWin
{
    // TODO consider autorun through registry, https://gist.github.com/HelBorn/2266242
    // TODO custom actions (hotkeys)

    internal static class Program
    {
        private static readonly AppSettingsHelper _appSettingsHelper;

        private static readonly CornersSettingsHelper _cornersSettingsHelper;

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
        private static DateTime _iconClickTimestamp = DateTime.Now;

        static Program()
        {
            ApplicationConfiguration.Initialize();

#if PORTABLE
            SettingsStorageOption settingsStorageOption = SettingsStorageOption.Filesystem;
#else
            SettingsStorageOption settingsStorageOption = SettingsStorageOption.Default;
#endif
            _appSettingsHelper = new(settingsStorageOption);
            _cornersSettingsHelper = new(_appSettingsHelper);

            Application.SetColorMode(_appSettingsHelper.Settings.ColorScheme);

            _menuItemSwitch = new(Properties.Resources.strMenuEnabled)
            {
                CheckOnClick = true
            };
            _menuItemSwitch.Click += (o, e) => ToggleOperationOnOff();

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
            _notifyIcon.MouseDown += NotifyIcon_MouseDown;

            _cornersProcessor = new(_cornersSettingsHelper);
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
            CornersHitTester.CornerRadius = _appSettingsHelper.Settings.AreaSize;

            // Set polling interval
            _cornersProcessor.PollInterval = _appSettingsHelper.Settings.PollInterval;

            // Enable or disable operation according to the settings.
            _cornersProcessor.DisableOnFullscreen = _appSettingsHelper.Settings.DisableOnFullscreen;
            _cornersProcessor.Enabled = _appSettingsHelper.Settings.IsEnabled;

            Application.Run();
        }

        /// <summary>
        /// Adjust the UI according with the app's state - running or disabled.
        /// </summary>
        private static void CornersProcessorStateChanged(bool enabled)
        {
            _dummyForm.BeginInvoke(new Action(() =>
            {
                _menuItemSwitch.Checked = enabled;
                _notifyIcon.Icon = enabled ? Properties.Resources.icon_new_on : Properties.Resources.icon_new_off;
                string tooltip = enabled ? Properties.Resources.strEnabled : Properties.Resources.strDisabled;
                _notifyIcon.Text = $"HotCornersWin ({tooltip})";
            }));
        }

        /// <summary>
        /// Get system screen information according to the app's settings.
        /// </summary>
        /// <returns>An array of rectangles representing screens' 
        /// locations and dimensions.</returns>
        private static Rectangle[] GetScreens()
        {
            /*MultiMonCfg moncfg = MultiMonCfg.Primary;
            if (Enum.IsDefined(typeof(MultiMonCfg), Properties.Settings.Default.MultiMonCfg))
            {
                moncfg = (MultiMonCfg)Properties.Settings.Default.MultiMonCfg;
            }*/
            return ScreenInfoHelper.GetScreensInfo(_appSettingsHelper.Settings.MultiMonCfg);
        }

        private static void NotifyIcon_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            // If the clicked flag is set, this is a second click:
            // calculate the time elapsed since the first click and
            // invoke the double click action if needed.
            // If the clicked flag isn't set, this is a first click:
            // set the clicked flag and rememeber the timestamp of the event.
            // Wait for a possible double click and check the flag,
            // if it was reset than the double click was processed,
            // else perform the single click action
            if (_wasIconClicked)
            {
                if (DateTime.Now.Subtract(_iconClickTimestamp).TotalMilliseconds <= SystemInformation.DoubleClickTime)
                {
                    _wasIconClicked = false;
                    // double click action
                    MenuItemSettings_Click(null, new());
                    return;
                }  
            }
            _wasIconClicked = true;
            _iconClickTimestamp = DateTime.Now;
            _ = Task.Run(async () =>
            {
                await Task.Delay(SystemInformation.DoubleClickTime + 5);
                if (_wasIconClicked)
                {
                    _wasIconClicked = false;
                    // single click action
                    ToggleOperationOnOff();
                }
            });
        }

        private static void ToggleOperationOnOff()
        {
            // toggle enable on double click and save changes
            _cornersProcessor.Enabled = !_cornersProcessor.Enabled;
            _appSettingsHelper.Settings.IsEnabled = _cornersProcessor.Enabled;
            _appSettingsHelper.Save();
        }

        private static void MenuItemSettings_Click(object? sender, EventArgs e)
        {
            if (Application.OpenForms["FormSettings"] is Form formSettings)
            {
                formSettings.BringToFront();
                return;
            }

            _ = new FormSettings(_appSettingsHelper, _cornersSettingsHelper)
            {
                Icon = Properties.Resources.icon_new_on,
                Text = Properties.Resources.FormSettingsText,
                TopMost = true,
                CornersProcessor = _cornersProcessor,
            }.ShowDialog();
        }

        private static void MenuItemAbout_Click(object? sender, EventArgs e)
        {
            if (Application.OpenForms["FormAbout"] is Form formAbout)
            {
                formAbout.BringToFront();
                return;
            }

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