namespace HotCornersWin
{
    public partial class FormActionsEditor : Form
    {
        private readonly ICornersSettingsHelper _cornersSettingsHelper;
        private readonly List<CustomAction> _actions;
        private CustomAction? _selectedAction = null;

        public FormActionsEditor(ICornersSettingsHelper cornersSettingsHelper)
        {
            InitializeComponent();
            _cornersSettingsHelper = cornersSettingsHelper;
            buttonSave.Text = Properties.Resources.strSaveApply;
            buttonCancel.Text = Properties.Resources.strCancel;
            buttonEditCancel.Text = Properties.Resources.strCancel;
            buttonEditOK.Text = Properties.Resources.strFinish;
            buttonAdd.Text = Properties.Resources.strAdd;
            buttonEdit.Text = Properties.Resources.strEdit;
            buttonRemove.Text = Properties.Resources.strRemove;
            labelCommand.Text = Properties.Resources.strCommand;
            labelName.Text = Properties.Resources.strName;

            _actions = _cornersSettingsHelper.CustomActions;
            listBoxActions.DataSource = _actions;
            listBoxActions.DisplayMember = "Name";
        }

        /// <summary>
        /// Set Name and Command textboxes contents according to 
        /// the properties of the given item. If the item is null,
        /// clear the textboxes' contents.
        /// </summary>
        /// <param name="itemKey"></param>
        private void FillEditTextBoxes(CustomAction? customAction = null)
        {
            if (customAction is null)
            {
                textBoxName.Text = string.Empty;
                textBoxCommand.Text = string.Empty;
            }
            else
            {
                textBoxName.Text = customAction.Name;
                if (customAction is CustomActionShell customActionShell)
                {
                    textBoxCommand.Text = customActionShell.Command;
                }
                else if (customAction is CustomActionHotkey customActionHotkey)
                {
                    // TODO not implemented
                }
            }
        }

        /// <summary>
        /// Enable or disable components depending on whether user
        /// is editing a value or viewing ones.
        /// </summary>
        /// <param name="isEditing">True if editing, otherwise false.</param>
        private void SwitchUIMode(bool isEditing)
        {
            if (isEditing)
            {
                groupBoxEdit.Enabled = true;
                listBoxActions.Enabled = false;
                buttonSave.Enabled = false;
                buttonAdd.Enabled = false;
                buttonEdit.Enabled = false;
                buttonRemove.Enabled = false;
            }
            else
            {
                groupBoxEdit.Enabled = false;
                listBoxActions.Enabled = true;
                buttonSave.Enabled = true;
                buttonAdd.Enabled = true;
                buttonEdit.Enabled = true;
                buttonRemove.Enabled = true;
            }
        }

        /// <summary>
        /// A dirty hack to update listBoxActions contents.
        /// </summary>
        private void ForceListBoxUpdate() // TODO get rid of this
        {
            listBoxActions.DataSource = null;
            listBoxActions.DisplayMember = "";
            listBoxActions.DataSource = _actions;
            listBoxActions.DisplayMember = "Name";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _selectedAction = null;
            FillEditTextBoxes();
            SwitchUIMode(true);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (_selectedAction is null)
            {
                return;
            }
            FillEditTextBoxes(_selectedAction);
            SwitchUIMode(true);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (_selectedAction is null)
            {
                return;
            }
            var dialogResult = MessageBox.Show(
                Properties.Resources.strRemovalWarning,
                Properties.Resources.strConfirmRemoval,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            _ = _actions.Remove(_selectedAction);

            ForceListBoxUpdate();

            FillEditTextBoxes();
        }

        private void buttonEditCancel_Click(object sender, EventArgs e)
        {
            FillEditTextBoxes(_selectedAction);
            SwitchUIMode(false);
        }

        private void buttonEditOK_Click(object sender, EventArgs e)
        {
            // TODO CustomActionHotkey not implemented
            string newName = textBoxName.Text.Trim();
            if (string.IsNullOrEmpty(newName))
            {
                _ = MessageBox.Show(
                    Properties.Resources.strNameEmpty,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            string newCommand = textBoxCommand.Text.Trim();
            if (string.IsNullOrEmpty(newCommand))
            {
                _ = MessageBox.Show(
                    Properties.Resources.strCommandEmpty,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            // if the selected item is null, add a new one; else edit
            if (_selectedAction is null)
            {
                if (_actions.Where(x => x.Name == newName).Any())
                {
                    _ = MessageBox.Show(
                    Properties.Resources.strNameAlreadyExists,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                _selectedAction = new CustomActionShell(newName, newCommand);
                _actions.Add(_selectedAction);
            }
            else
            {
                if (newName != _selectedAction.Name && _actions.Where(x => x.Name == newName).Any())
                {
                    _ = MessageBox.Show(
                    Properties.Resources.strNameAlreadyExists,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                _selectedAction.Name = newName;
                if (_selectedAction is CustomActionShell customActionShell)
                {
                    customActionShell.Command = newCommand;
                }
            }

            ForceListBoxUpdate();
            SwitchUIMode(false);
        }

        private void listBoxActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxActions.SelectedItem is CustomAction customAction)
            {
                _selectedAction = customAction;
            }
            else
            {
                _selectedAction = null;
            }
            FillEditTextBoxes(_selectedAction);
        }

        private void listBoxActions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonEdit.PerformClick();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            // save and apply the settings immediately
            _cornersSettingsHelper.CustomActions = _actions;
        }
    }
}
