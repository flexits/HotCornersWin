namespace HotCornersWin
{
    public partial class FormSettings : Form
    {
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
            checkBoxAutoFullscreen.Text = Properties.Resources.strAutoFullscreen;
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
            checkBoxAutoFullscreen.Checked = Properties.Settings.Default.AutoFullscreen;

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
            Properties.Settings.Default.DelayLT = (int)numericUpDownDelayLT.Value;
            Properties.Settings.Default.DelayLB = (int)numericUpDownDelayLB.Value;
            Properties.Settings.Default.DelayRB = (int)numericUpDownDelayRB.Value;
            Properties.Settings.Default.DelayRT = (int)numericUpDownDelayRT.Value;

            // assign actions
            Properties.Settings.Default.LeftTop = (string)comboBoxLT.SelectedItem;
            Properties.Settings.Default.RightTop = (string)comboBoxRT.SelectedItem;
            Properties.Settings.Default.LeftBottom = (string)comboBoxLB.SelectedItem;
            Properties.Settings.Default.RightBottom = (string)comboBoxRB.SelectedItem;

            Properties.Settings.Default.AreaSize = (int)numericUpDownRadius.Value;
            Properties.Settings.Default.PollInterval = (int)numericUpDownPoll.Value;
            Properties.Settings.Default.AutoFullscreen = checkBoxAutoFullscreen.Checked;

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

            Properties.Settings.Default.Save();

            // TODO PollInterval and AutoFullscreen won't apply here 
            CornersHitTester.CornerRadius = Properties.Settings.Default.AreaSize;
            CornersHitTester.Screens = screens;
            CornersSettingsHelper.ReloadSettings();
        }

        private void buttonCustomActions_Click(object sender, EventArgs e)
        {
            var fae = new FormActionsEditor()
            {
                Icon = Properties.Resources.icon_new_on,
                Text = Properties.Resources.FormActionsEditorText,
                TopMost = true,
                StartPosition = FormStartPosition.CenterParent,
            };
            if (fae.ShowDialog(this) == DialogResult.OK)
            {
                // TODO see CornersSettingsHelper line 44
                CornersSettingsHelper.ReloadSettings();
                LoadActions();
            }
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