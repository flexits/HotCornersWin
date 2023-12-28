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
            groupBoxStart.Text = Properties.Resources.strAutostartSettings;
            radioButtonAutoOff.Text = Properties.Resources.strAutoOff;
            radioButtonAutoCurUsr.Text = Properties.Resources.strAutoCurUsr;
            radioButtonAutoAllUsr.Text = Properties.Resources.strAutoAllUsr;
            buttonApply.Text = Properties.Resources.strApply;
            buttonCancel.Text = Properties.Resources.strCancel;
            _actionNames = ActionCaller.GetActionNames();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
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
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
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