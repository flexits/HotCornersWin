using System.ComponentModel;

namespace HotCornersWin
{
    public partial class FormSettings : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Pass an instance of HotCornersProcessor here to apply settings to it.
        /// </summary>
        public HotCornersProcessor? CornersProcessor { get; set; }

        private readonly ICornersSettingsHelper _cornersSettingsHelper;
        private readonly IAppSettingsHelper _appSettingsHelper;
        private readonly AppSettingsData _settings;

        public FormSettings(IAppSettingsHelper appSettingsHelper, ICornersSettingsHelper cornersSettingsHelper)
        {
            InitializeComponent();
            _cornersSettingsHelper = cornersSettingsHelper;
            _appSettingsHelper = appSettingsHelper;
            _settings = appSettingsHelper.Settings;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
            groupBoxCorners.Text = Properties.Resources.strChooseAction;
            groupBoxMulti.Text = Properties.Resources.strMonSet;
            groupBoxAdvanced.Text = Properties.Resources.strAdvSet;
            groupBoxColorScheme.Text = Properties.Resources.strColorScheme;
            radioButtonDark.Text = Properties.Resources.strDark;
            radioButtonLight.Text = Properties.Resources.strLight;
            radioButtonSystem.Text = Properties.Resources.strAuto;
            radioButtonVirt.Text = Properties.Resources.strMonVirt;
            radioButtonPrim.Text = Properties.Resources.strMonPrim;
            radioButtonSept.Text = Properties.Resources.strMonSepr;
            labelRadius.Text = Properties.Resources.strCornerRadius;
            labelPollInterval.Text = Properties.Resources.strPollInterval;
            buttonApply.Text = Properties.Resources.strApply;
            buttonCancel.Text = Properties.Resources.strCancel;
            buttonCustomActions.Text = Properties.Resources.strCustAct;
            buttonDebugInfo.Text = Properties.Resources.strDebugExport;
            checkBoxDisableOnFullscreen.Text = Properties.Resources.strDisableOnFullscreen;
            labelDelay1.Text = Properties.Resources.strActionDelay;
            labelDelay2.Text = Properties.Resources.strActionDelay;
            labelDelay3.Text = Properties.Resources.strActionDelay;
            labelDelay4.Text = Properties.Resources.strActionDelay;
        }

        /// <summary>
        /// Load actions' names from settings into comboboxes.
        /// </summary>
        private void LoadActions()
        {
            string[] actionNames = _cornersSettingsHelper.GetActionNames();
            int index = -1;
            comboBoxLT.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, _settings.LeftTop);
            comboBoxLT.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxRT.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, _settings.RightTop);
            comboBoxRT.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxLB.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, _settings.LeftBottom);
            comboBoxLB.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxRB.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, _settings.RightBottom);
            comboBoxRB.SelectedIndex = (index >= 0) ? index : 0;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            numericUpDownDelayLT.Value = _settings.DelayLT;
            numericUpDownDelayLB.Value = _settings.DelayLB;
            numericUpDownDelayRB.Value = _settings.DelayRB;
            numericUpDownDelayRT.Value = _settings.DelayRT;

            numericUpDownRadius.Value = _settings.AreaSize;
            numericUpDownPoll.Value = _settings.PollInterval;
            checkBoxDisableOnFullscreen.Checked = _settings.DisableOnFullscreen;

            switch (_settings.MultiMonCfg)
            {
                case MultiMonCfg.Virtual:
                    radioButtonVirt.Checked = true;
                    break;
                case MultiMonCfg.Primary:
                    radioButtonPrim.Checked = true;
                    break;
                case MultiMonCfg.Separate:
                    radioButtonSept.Checked = true;
                    break;
                default:
                    radioButtonPrim.Checked = true;
                    break;
            }

            switch (_settings.ColorScheme)
            {
                case SystemColorMode.System:
                    radioButtonSystem.Checked = true;
                    break;
                case SystemColorMode.Dark:
                    radioButtonDark.Checked = true;
                    break;
                case SystemColorMode.Classic:
                    radioButtonLight.Checked = true;
                    break;
            }

            LoadActions();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            // assign delays
            _settings.DelayLT = (int)numericUpDownDelayLT.Value;
            _settings.DelayLB = (int)numericUpDownDelayLB.Value;
            _settings.DelayRB = (int)numericUpDownDelayRB.Value;
            _settings.DelayRT = (int)numericUpDownDelayRT.Value;
            // assign actions
            _settings.LeftTop = comboBoxLT.SelectedItem as string ?? string.Empty;
            _settings.RightTop = comboBoxRT.SelectedItem as string ?? string.Empty;
            _settings.LeftBottom = comboBoxLB.SelectedItem as string ?? string.Empty;
            _settings.RightBottom = comboBoxRB.SelectedItem as string ?? string.Empty;
            // other settings
            _settings.AreaSize = (int)numericUpDownRadius.Value;
            _settings.PollInterval = (int)numericUpDownPoll.Value;
            _settings.DisableOnFullscreen = checkBoxDisableOnFullscreen.Checked;
            // monitor config
            MultiMonCfg monCfg = MultiMonCfg.Primary;
            if (radioButtonVirt.Checked)
            {
                monCfg = MultiMonCfg.Virtual;
            }
            else if (radioButtonSept.Checked)
            {
                monCfg = MultiMonCfg.Separate;
            }
            var screens = ScreenInfoHelper.GetScreensInfo(monCfg);
            if (screens.Length == 0)
            {
                _ = MessageBox.Show(Properties.Resources.strBoundsErr,
                    Properties.Resources.strFatalErr,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _settings.MultiMonCfg = monCfg;
            // color scheme
            SystemColorMode colorMode = SystemColorMode.System;
            if (radioButtonDark.Checked)
            {
                colorMode = SystemColorMode.Dark;
            }
            else if (radioButtonLight.Checked)
            {
                colorMode = SystemColorMode.Classic;
            }
            bool wasColorSchemeChanged = _settings.ColorScheme != colorMode;
            _settings.ColorScheme = colorMode;
            // save the settings
            _appSettingsHelper.Save();
            // apply the settings immediately
            if (wasColorSchemeChanged)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            if (CornersProcessor is not null)
            {
                CornersProcessor.PollInterval = _settings.PollInterval;
                CornersProcessor.DisableOnFullscreen = _settings.DisableOnFullscreen;
            }
            CornersHitTester.CornerRadius = _settings.AreaSize;
            CornersHitTester.Screens = screens;
            _cornersSettingsHelper.ReloadSettings();
        }

        private void buttonCustomActions_Click(object sender, EventArgs e)
        {
            _ = new FormActionsEditor(_cornersSettingsHelper)
            {
                Icon = Properties.Resources.icon_new_on,
                Text = Properties.Resources.FormActionsEditorText,
                TopMost = true,
                StartPosition = FormStartPosition.CenterParent,
            }.ShowDialog(this);
            LoadActions();
        }

        private void FormSettings_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    buttonCancel.PerformClick();
                    break;
            }
        }
    }
}