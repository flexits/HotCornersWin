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
        /// Custom actions created by user - get from settings, save to settings.
        /// </summary>
        public static List<CustomAction> CustomActions
        {
            get
            {
                try
                {
                    var customActions = JsonSerializer.
                        Deserialize<List<CustomAction>>(Properties.Settings.Default.CustomActions);
                    if (customActions is null)
                    {
                        return new();
                    }
                    else
                    {
                        return customActions;
                    }
                }
                catch
                {
                    return new();
                }
            }
            set
            {
                try
                {
                    Properties.Settings.Default.CustomActions = JsonSerializer.Serialize(value);
                    Properties.Settings.Default.Save();
                    ReloadSettings();
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
            {Properties.Resources.saWidgetsBoard, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                KeyboardSend.KeyDown(Keys.W);
                KeyboardSend.KeyUp(Keys.LWin);
                KeyboardSend.KeyUp(Keys.W);
                }
            }
        };

        /// <summary>
        /// All available actions and their human-readable names.
        /// </summary>
        private static readonly Dictionary<string, Action> _allActions = new();

        /// <summary>
        /// Hot corners and their correspondent actions as configured in the settings.
        /// </summary>
        private static readonly Dictionary<Corners, Action> _cornerActions = new()
        {
            {Corners.LeftTop, () => {} },
            {Corners.RightTop, () => {} },
            {Corners.LeftBottom, () => {} },
            {Corners.RightBottom, () => {} }
        };

        /// <summary>
        /// Hot corners and their correspondent delays as configured in the settings. 
        /// </summary>
        private static readonly Dictionary<Corners, int> _cornerDelays = new()
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
        /// Reload hot corner settings (delays and actions) from the app's settings.
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
            foreach (var customAction in CustomActions)
            {
                _ = _allActions.TryAdd(customAction.Name, customAction.GetAction());
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
