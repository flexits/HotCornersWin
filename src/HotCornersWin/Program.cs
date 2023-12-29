using System.Reflection;
using System.Windows.Forms;

namespace HotCornersWin
{
    // TODO autorun
    // https://gist.github.com/HelBorn/2266242

    // TODO custom actions
    // TODO installer
    // TODO about
    // TODO add attribution to https://uxwing.com/
    // TODO add attribution to Icons8 <a target="_blank" href="https://icons8.com/icon/3pKFQN9sPxow/layout">Screen</a> icon by <a target="_blank" href="https://icons8.com">Icons8</a>

    internal static class Program
    {
        private static readonly NotifyIcon _notifyIcon;
        private static readonly ToolStripMenuItem _menuItemSwitch;
        private static readonly MouseHook _mouseHook;
        private static MoveProcessor? _moveProcessor;
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
            _menuItemSwitch.Click += OnEnableChanged;

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
            _notifyIcon.DoubleClick += OnEnableChanged;

            _mouseHook = new();
        }

        [STAThread]
        static void Main()
        {
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
            _moveProcessor = new() { Screens = screens };
            _moveProcessor.CornerReached += ActionCaller.ExecuteAction;
            _mouseHook.Move += (coords) => _moveProcessor?.CornerHitTest(coords);
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
            // get screen information according to the specified multi-monitor behavior
            switch (moncfg)
            {
                case MultiMonCfg.Virtual:
                    return new Rectangle[] { SystemInformation.VirtualScreen };
                case MultiMonCfg.Primary:
                    Rectangle? bounds = Screen.PrimaryScreen?.Bounds;
                    if (bounds is not null)
                    {
                        return new Rectangle[] { (Rectangle)bounds };
                    }
                    break;
                case MultiMonCfg.Separate:
                    return Screen.AllScreens.Select(s => s.Bounds).ToArray();
            }
            return Array.Empty<Rectangle>();
        }

        private static void OnEnableChanged(object? sender, EventArgs e)
        {
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
                if (_moveProcessor is not null)
                {
                    _moveProcessor.Screens = GetScreens();
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