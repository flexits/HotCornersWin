using System.Diagnostics;
using System.Reflection;

namespace HotCornersWin
{
    // TODO consider autorun through registry, https://gist.github.com/HelBorn/2266242
    // TODO ignore actions if mouse button is pressed - to settings
    // TODO custom actions (commands)
    // TODO custom actions (hotkeys)
    // TODO detect a full-screen app and disable itself

    internal static class Program
    {
        private static readonly NotifyIcon _notifyIcon;
        private static readonly ToolStripMenuItem _menuItemSwitch;
        private static readonly MouseHook _mouseHook;
        private static HotCornersHelper? _hotCornersHelper;
        
        /// <summary>
        /// The flag is used to distinguish single and double clicks.
        /// It is set when a click occurs and must be reset 
        /// when either a single or double click is handled.
        /// </summary>
        private static bool _wasIconClicked;

        private static bool _enabled;

        private static bool IsEnabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                _menuItemSwitch.Checked = _enabled;
                _notifyIcon.Icon = _enabled ? Properties.Resources.icons8_layout_96_cl : Properties.Resources.icons8_layout_96_bk;
                string tooltip = _enabled ? Properties.Resources.strEnabled : Properties.Resources.strDisabled;
                _notifyIcon.Text = $"HotCornersWin ({tooltip})";
                _mouseHook.IsEnabled = _enabled;
                Properties.Settings.Default.IsEnabled = _enabled;
                Properties.Settings.Default.Save();
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
            _hotCornersHelper = new(screens, 
                Properties.Settings.Default.AreaSize,
                Properties.Settings.Default.HitRepeatDelay);
            _hotCornersHelper.CornerReached += ActionCaller.ExecuteAction;
            _mouseHook.Move += (coords) => _hotCornersHelper?.CornerHitTest(coords);
            // Enable or disable operation according to the settings.
            IsEnabled = Properties.Settings.Default.IsEnabled;

            Application.Run();
        }

        private static Rectangle[] GetScreens()
        {
            // read multi-monitor configuration from settings
            MultiMonCfg moncfg = MultiMonCfg.Primary;
            if (Enum.IsDefined(typeof(MultiMonCfg), Properties.Settings.Default.MultiMonCfg))
            {
                moncfg = (MultiMonCfg)Properties.Settings.Default.MultiMonCfg;
            }
            return ScreenInfoHelper.GetScreens(moncfg);
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
                MenuItemSettings_Click(sender, e);
            }
        }

        private static void NotifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            // reset the clicked flag
            _wasIconClicked = false;
            // execute double click actions
            IsEnabled = !IsEnabled;
        }

        private static void MenuItemSettings_Click(object? sender, EventArgs e)
        {
            FormSettings fs = new()
            {
                Icon = Properties.Resources.icons8_layout_96_cl,
                Text = Properties.Resources.FormSettingsText,
            };
            if (fs.ShowDialog() == DialogResult.OK)
            {
                if (_hotCornersHelper is not null)
                {
                    _hotCornersHelper.Screens = GetScreens();
                    _hotCornersHelper.CornerAreaSize = Properties.Settings.Default.AreaSize;
                    _hotCornersHelper.RepetitiveHitDelay = Properties.Settings.Default.HitRepeatDelay;
                }
                ActionCaller.ReloadSettings();
            }
        }

        private static void MenuItemAbout_Click(object? sender, EventArgs e)
        {
            _ = new FormAbout()
            {
                Icon = Properties.Resources.icons8_layout_96_cl,
                Text = Properties.Resources.FormAboutText,
            }
            .ShowDialog();
        }

        private static void MenuItemQuit_Click(object? sender, EventArgs e)
        {
            _mouseHook?.Dispose();
            _notifyIcon?.Dispose();
            Application.Exit();
        }
    }
}