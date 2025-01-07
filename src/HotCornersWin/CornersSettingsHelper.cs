using System.Diagnostics;
using System.Text.Json;

namespace HotCornersWin
{
    public class CornersSettingsHelper : ICornersSettingsHelper
    {
        /// <summary>
        /// Custom actions created by user - get from settings, save to settings.
        /// </summary>
        public List<CustomAction> CustomActions
        {
            get
            {
                try
                {
                    var customActions = JsonSerializer.
                        Deserialize<List<CustomAction>>(_settings.CustomActions);
                    if (customActions is null)
                    {
                        return [];
                    }
                    else
                    {
                        return customActions;
                    }
                }
                catch
                {
                    return [];
                }
            }
            set
            {
                try
                {
                    _settings.CustomActions = JsonSerializer.Serialize(value);
                    _appSettingsHelper.Save();
                    ReloadSettings();
                }
                catch { }
            }
        }

        /// <summary>
        /// Predefined actions and their human-readable names.
        /// </summary>
        private readonly Dictionary<string, Action> _predefinedActions = new()
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
                _ = new Process
                    {
                        StartInfo = new ProcessStartInfo("rundll32.exe", "user32.dll,LockWorkStation")
                        {
                            UseShellExecute = true
                        }
                    }.Start();
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
        private readonly Dictionary<string, Action> _allActions = [];

        /// <summary>
        /// Hot corners and their correspondent actions as configured in the settings.
        /// </summary>
        private readonly Dictionary<Corners, Action> _cornerActions = new()
        {
            {Corners.LeftTop, () => {} },
            {Corners.RightTop, () => {} },
            {Corners.LeftBottom, () => {} },
            {Corners.RightBottom, () => {} }
        };

        /// <summary>
        /// Hot corners and their correspondent delays as configured in the settings. 
        /// </summary>
        private readonly Dictionary<Corners, int> _cornerDelays = new()
        {
            {Corners.LeftTop, 0 },
            {Corners.RightTop, 0 },
            {Corners.LeftBottom, 0 },
            {Corners.RightBottom, 0 }
        };

        private readonly IAppSettingsHelper _appSettingsHelper;
        private readonly AppSettingsData _settings;

        public CornersSettingsHelper(IAppSettingsHelper appSettingsHelper)
        {
            _appSettingsHelper = appSettingsHelper;
            _settings = appSettingsHelper.Settings;
            ReloadSettings();
        }

        /// <summary>
        /// Reload hot corner settings (delays and actions) from the app's settings.
        /// </summary>
        public void ReloadSettings()
        {
            _cornerDelays[Corners.LeftTop] = _settings.DelayLT;
            _cornerDelays[Corners.LeftBottom] = _settings.DelayLB;
            _cornerDelays[Corners.RightTop] = _settings.DelayRT;
            _cornerDelays[Corners.RightBottom] = _settings.DelayRB;

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
                .TryGetValue(_settings.LeftTop, out Action? actionLT) ? actionLT : (() => { });
            _cornerActions[Corners.LeftBottom] = _allActions
                .TryGetValue(_settings.LeftBottom, out Action? actionLB) ? actionLB : (() => { });
            _cornerActions[Corners.RightTop] = _allActions
                .TryGetValue(_settings.RightTop, out Action? actionRT) ? actionRT : (() => { });
            _cornerActions[Corners.RightBottom] = _allActions
                .TryGetValue(_settings.RightBottom, out Action? actionRB) ? actionRB : (() => { });
        }

        /// <summary>
        /// Returns an array of human-readable names for all available actions.
        /// </summary>
        public string[] GetActionNames()
        {
            return [.. _allActions.Keys];
        }

        /// <summary>
        /// Get the action associated with the given hot corner.
        /// </summary>
        public Action GetAction(Corners corner)
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
        public int GetDelay(Corners corner)
        {
            if (_cornerDelays.TryGetValue(corner, out int delay))
            {
                return delay;
            }
            return 0;
        }
    }
}
