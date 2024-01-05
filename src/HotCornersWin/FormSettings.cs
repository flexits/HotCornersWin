namespace HotCornersWin
{
    public partial class FormSettings : Form
    {
        private readonly string[] _actionNames;

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
            _actionNames = ActionCaller.GetActionNames();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            numericUpDownRadius.Value = Properties.Settings.Default.AreaSize;
            numericUpDownPoll.Value = Properties.Settings.Default.PollInterval;
            checkBoxAutoFullscreen.Checked = Properties.Settings.Default.AutoFullscreen;

            int index = -1;
            comboBoxLT.DataSource = _actionNames.ToArray();
            index = Array.IndexOf(_actionNames, Properties.Settings.Default.LeftTop);
            comboBoxLT.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxRT.DataSource = _actionNames.ToArray();
            index = Array.IndexOf(_actionNames, Properties.Settings.Default.RightTop);
            comboBoxRT.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxLB.DataSource = _actionNames.ToArray();
            index = Array.IndexOf(_actionNames, Properties.Settings.Default.LeftBottom);
            comboBoxLB.SelectedIndex = (index >= 0) ? index : 0;

            comboBoxRB.DataSource = _actionNames.ToArray();
            index = Array.IndexOf(_actionNames, Properties.Settings.Default.RightBottom);
            comboBoxRB.SelectedIndex = (index >= 0) ? index : 0;

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
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
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
            if (ScreenInfoHelper.GetScreensInfo(monCfg).Length == 0)
            {
                _ = MessageBox.Show(Properties.Resources.strBoundsErr,
                    Properties.Resources.strFatalErr,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Properties.Settings.Default.MultiMonCfg = (int)monCfg;
            // assign actions
            Properties.Settings.Default.LeftTop = (string)comboBoxLT.SelectedItem;
            Properties.Settings.Default.RightTop = (string)comboBoxRT.SelectedItem;
            Properties.Settings.Default.LeftBottom = (string)comboBoxLB.SelectedItem;
            Properties.Settings.Default.RightBottom = (string)comboBoxRB.SelectedItem;
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}