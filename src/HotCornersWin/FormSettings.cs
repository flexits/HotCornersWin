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

        public FormSettings()
        {
            InitializeComponent();
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
            groupBoxCorners.Text = Properties.Resources.strChooseAction;
            groupBoxMulti.Text = Properties.Resources.strMonSet;
            groupBoxAdvanced.Text = Properties.Resources.strAdvSet;
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
            string[] actionNames = CornersSettingsHelper.GetActionNames();
            int index = -1;
            comboBoxLT.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, Properties.Settings.Default.LeftTop);
            comboBoxLT.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxRT.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, Properties.Settings.Default.RightTop);
            comboBoxRT.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxLB.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, Properties.Settings.Default.LeftBottom);
            comboBoxLB.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxRB.DataSource = actionNames.ToArray();
            index = Array.IndexOf(actionNames, Properties.Settings.Default.RightBottom);
            comboBoxRB.SelectedIndex = (index >= 0) ? index : 0;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            numericUpDownDelayLT.Value = Properties.Settings.Default.DelayLT;
            numericUpDownDelayLB.Value = Properties.Settings.Default.DelayLB;
            numericUpDownDelayRB.Value = Properties.Settings.Default.DelayRB;
            numericUpDownDelayRT.Value = Properties.Settings.Default.DelayRT;

            numericUpDownRadius.Value = Properties.Settings.Default.AreaSize;
            numericUpDownPoll.Value = Properties.Settings.Default.PollInterval;
            checkBoxDisableOnFullscreen.Checked = Properties.Settings.Default.DisableOnFullscreen;

            if (Enum.IsDefined(typeof(MultiMonCfg), Properties.Settings.Default.MultiMonCfg))
            {
                switch ((MultiMonCfg)Properties.Settings.Default.MultiMonCfg)
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
            }
            else
            {
                radioButtonPrim.Checked = true;
            }

            LoadActions();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            // assign delays
            Properties.Settings.Default.DelayLT = (int)numericUpDownDelayLT.Value;
            Properties.Settings.Default.DelayLB = (int)numericUpDownDelayLB.Value;
            Properties.Settings.Default.DelayRB = (int)numericUpDownDelayRB.Value;
            Properties.Settings.Default.DelayRT = (int)numericUpDownDelayRT.Value;
            // assign actions
            Properties.Settings.Default.LeftTop = comboBoxLT.SelectedItem as string;
            Properties.Settings.Default.RightTop = comboBoxRT.SelectedItem as string;
            Properties.Settings.Default.LeftBottom = comboBoxLB.SelectedItem as string;
            Properties.Settings.Default.RightBottom = comboBoxRB.SelectedItem as string;
            // other settings
            Properties.Settings.Default.AreaSize = (int)numericUpDownRadius.Value;
            Properties.Settings.Default.PollInterval = (int)numericUpDownPoll.Value;
            Properties.Settings.Default.DisableOnFullscreen = checkBoxDisableOnFullscreen.Checked;
            // validate monitor config
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
            Properties.Settings.Default.MultiMonCfg = (int)monCfg;
            // save the settings
            Properties.Settings.Default.Save();
            // apply the settings immediately
            if (CornersProcessor is not null)
            {
                CornersProcessor.PollInterval = Properties.Settings.Default.PollInterval;
                CornersProcessor.DisableOnFullscreen = Properties.Settings.Default.DisableOnFullscreen;
            }
            CornersHitTester.CornerRadius = Properties.Settings.Default.AreaSize;
            CornersHitTester.Screens = screens;
            CornersSettingsHelper.ReloadSettings();
        }

        private void buttonCustomActions_Click(object sender, EventArgs e)
        {
            _ = new FormActionsEditor()
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