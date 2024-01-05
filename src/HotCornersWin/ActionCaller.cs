namespace HotCornersWin
{
    /// <summary>
    /// Invokes an action associated with a hot corner in the app's settings.
    /// </summary>
    public static class ActionCaller
    {
        // TODO Corners' delays

        /// <summary>
        /// All available actions and their human-readable names.
        /// </summary>
        private static Dictionary<string, Action> _allActions = new()
        {
            {Properties.Resources.saNone,  () => { } },
            {Properties.Resources.saStartMenu,  () => {SendKeys.Send("^{ESC}"); } },
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
                SendKeys.Send("^{RIGHT}");
                KeyboardSend.KeyUp(Keys.LWin);
                }
            },
            {Properties.Resources.saLVirtDesktop, () => {
                KeyboardSend.KeyDown(Keys.LWin);
                SendKeys.Send("^{LEFT}");
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
        /// Hot corners and their correspondent actions configured in the settings.
        /// </summary>
        private static Dictionary<Corners, Action> _execActions = new()
        {
            {Corners.LeftTop, () => {} },
            {Corners.RightTop, () => {} },
            {Corners.LeftBottom, () => {} },
            {Corners.RightBottom, () => {} }
        };

        static ActionCaller()
        {
            ReloadSettings();
        }

        /// <summary>
        /// Reload hot corner actions from the app's settings.
        /// </summary>
        public static void ReloadSettings()
        {
            _execActions[Corners.LeftTop] = _allActions
                .TryGetValue(Properties.Settings.Default.LeftTop, out Action? actionLT) ? actionLT : (() => { });
            _execActions[Corners.LeftBottom] = _allActions
                .TryGetValue(Properties.Settings.Default.LeftBottom, out Action? actionLB) ? actionLB : (() => { });
            _execActions[Corners.RightTop] = _allActions
                .TryGetValue(Properties.Settings.Default.RightTop, out Action? actionRT) ? actionRT : (() => { });
            _execActions[Corners.RightBottom] = _allActions
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
        /// Executes the action associated with the given hot corner.
        /// </summary>
        public static void ExecuteAction(Corners corner)
        {
            if (_execActions.TryGetValue(corner, out Action? action))
            {
                action?.Invoke();
            }
        }
    }
}
