using System.Diagnostics;
using System.Text.Json;

namespace HotCornersWin
{
    /// <summary>
    /// Wrapper around the app's settings providing access 
    /// to hot corners' actions and delays.
    /// </summary>
    public static class CornersSettingsHelper
    {
        /// <summary>
        /// User-adjustable actions. 
        /// Dictionary key is the action name, 
        /// dictionary value is the Windows shell command.
        /// </summary>
        public static Dictionary<string, string> CustomActionCommands
        {
            get
            {
                try
                {
                    var actions = JsonSerializer.
                        Deserialize<Dictionary<string, string>>(Properties.Settings.Default.CustomActions);
                    if (actions is null)
                    {
                        return new Dictionary<string, string>();
                    }
                    else
                    {
                        return actions;
                    }
                }
                catch
                {
                    return new Dictionary<string, string>();
                }
            }
            set
            {
                try
                {
                    Properties.Settings.Default.CustomActions = JsonSerializer.Serialize(value);
                    // TODO reload actions
                    Properties.Settings.Default.Save();
                }
                catch { }
            }
        }

        /// <summary>
        /// Predefined actions and their human-readable names.
        /// </summary>
        private static readonly Dictionary<string, Action> _predefinedActions = new()
        {
            {Properties.Resources.saNone,  () => { } },
            {Properties.Resources.saStartMenu,  () => {SendKeys.SendWait("^{ESC}"); } },
            {Properties.Resources.saTaskView, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.Tab);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.Tab);
                }
            },
            {Properties.Resources.saShowDesktop, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.D);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.D);
                }
            },
            {Properties.Resources.saRVirtDesktop, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                SendKeys.SendWait("^{RIGHT}");
                KeyboardSend.KeyUp(Keys.LWin);
                }
            },
            {Properties.Resources.saLVirtDesktop, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                SendKeys.SendWait("^{LEFT}");
                KeyboardSend.KeyUp(Keys.LWin);
                }
            },
            {Properties.Resources.saLockPC, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.L);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.L);
                }
            },
            {Properties.Resources.saExplorer, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.E);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.E);
                }
            },
            {Properties.Resources.saQuickLink, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.X);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.X);
                }
            },
            {Properties.Resources.saActionCenter, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.A);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.A);
                }
            },
            {Properties.Resources.saPrjSet, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.P);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.P);
                }
            },
            {Properties.Resources.saWinInk, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.W);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.W);
                }
            },
            {Properties.Resources.saSnipSketch, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.LShiftKey);
                KeyboardSend.KeyDown(Keys.S);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.LShiftKey);
                KeyboardSend.KeyUp(Keys.S);
                }
            },
        };

        /// <summary>
        /// All available actions and their human-readable names.
        /// </summary>
        private static Dictionary<string, Action> _allActions = new();

        /// <summary>
        /// Hot corners and their correspondent actions as configured in the settings.
        /// </summary>
        private static Dictionary<Corners, Action> _cornerActions = new()
        {
            {Corners.LeftTop, () => {} },
            {Corners.RightTop, () => {} },
            {Corners.LeftBottom, () => {} },
            {Corners.RightBottom, () => {} }
        };

        /// <summary>
        /// Hot corners and their correspondent delays as configured in the settings. 
        /// </summary>
        private static Dictionary<Corners, int> _cornerDelays = new()
        {
            {Corners.LeftTop, 0 },
            {Corners.RightTop, 0 },
            {Corners.LeftBottom, 0 },
            {Corners.RightBottom, 0 }
        };

        static CornersSettingsHelper()
        {
            ReloadSettings();
        }

        /// <summary>
        /// Reload hot corner settings from the app's settings.
        /// </summary>
        public static void ReloadSettings()
        {
            _cornerDelays[Corners.LeftTop] = Properties.Settings.Default.DelayLT;
            _cornerDelays[Corners.LeftBottom] = Properties.Settings.Default.DelayLB;
            _cornerDelays[Corners.RightTop] = Properties.Settings.Default.DelayRT;
            _cornerDelays[Corners.RightBottom] = Properties.Settings.Default.DelayRB;

            _allActions.Clear();
            foreach (var action in _predefinedActions)
            {
                _ = _allActions.TryAdd(action.Key, action.Value);
            }
            foreach (var command in CustomActionCommands)
            {
                if (string.IsNullOrEmpty(command.Value))
                {
                    continue;
                }
                _ = _allActions.TryAdd(command.Key, () =>
                {
                    _ = new Process
                    {
                        StartInfo = new ProcessStartInfo(command.Value)
                        {
                            UseShellExecute = true
                        }
                    }.Start();
                });
            }

            _cornerActions[Corners.LeftTop] = _allActions
                .TryGetValue(Properties.Settings.Default.LeftTop, out Action? actionLT) ? actionLT : (() => { });
            _cornerActions[Corners.LeftBottom] = _allActions
                .TryGetValue(Properties.Settings.Default.LeftBottom, out Action? actionLB) ? actionLB : (() => { });
            _cornerActions[Corners.RightTop] = _allActions
                .TryGetValue(Properties.Settings.Default.RightTop, out Action? actionRT) ? actionRT : (() => { });
            _cornerActions[Corners.RightBottom] = _allActions
                .TryGetValue(Properties.Settings.Default.RightBottom, out Action? actionRB) ? actionRB : (() => { });
        }
        
        /// <summary>
        /// Returns an array of human-readable names for all available actions.
        /// </summary>
        public static string[] GetActionNames()
        {
            return _allActions.Keys.ToArray();
        }

        /// <summary>
        /// Get the action associated with the given hot corner.
        /// </summary>
        public static Action GetAction(Corners corner)
        {
            if (_cornerActions.TryGetValue(corner, out Action? action))
            {
                return action;
            }
            return () => { };
        }

        /// <summary>
        /// Get the action trigger delay for the given corner.
        /// </summary>
        public static int GetDelay(Corners corner)
        {
            if (_cornerDelays.TryGetValue(corner, out int delay))
            {
                return delay;
            }
            return 0;
        }
    }
}
