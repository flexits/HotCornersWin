namespace HotCornersWin
{
    public static class ActionCaller
    {
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
        
        public static string[] GetActionNames()
        {
            return _allActions.Keys.ToArray();
        }

        public static void ExecuteAction(Corners corner)
        {
            if (_execActions.TryGetValue(corner, out Action? action))
            {
                action?.Invoke();
            }
        }
    }
}
