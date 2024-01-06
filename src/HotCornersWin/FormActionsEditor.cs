namespace HotCornersWin
{
    public partial class FormActionsEditor : Form
    {
        private Dictionary<string, string> _actions;
        private string? _currentItemKey = null;

        public FormActionsEditor()
        {
            InitializeComponent();
            buttonSave.Text = Properties.Resources.strSaveChanges;
            buttonCancel.Text = Properties.Resources.strCancel;
            buttonEditCancel.Text = Properties.Resources.strCancel;
            buttonEditOK.Text = Properties.Resources.strFinish;
            buttonAdd.Text = Properties.Resources.strAdd;
            buttonEdit.Text = Properties.Resources.strEdit;
            buttonRemove.Text = Properties.Resources.strRemove;
            labelCommand.Text = Properties.Resources.strCommand;
            labelName.Text = Properties.Resources.strName;
            _actions = CornersSettingsHelper.CustomActionCommands;
            listBoxActions.DataSource = _actions.Keys.ToList();
        }

        /// <summary>
        /// Set Name and Command textboxes contents according to
        /// the item with the given key. If the key is null or
        /// doesn't exist, clear the textboxes.
        /// </summary>
        /// <param name="itemKey"></param>
        private void FillEditTextBoxes(string? itemKey = null)
        {
            if (!string.IsNullOrEmpty(itemKey)
                && _actions.TryGetValue(itemKey, out string? itemValue))
            {
                textBoxName.Text = itemKey;
                textBoxCommand.Text = itemValue;
            }
            else
            {
                textBoxName.Text = string.Empty;
                textBoxCommand.Text = string.Empty;
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
            }
            else
            {
                groupBoxEdit.Enabled = false;
                listBoxActions.Enabled = true;
                buttonSave.Enabled = true;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            CornersSettingsHelper.CustomActionCommands = _actions;
            // TODO don't close the window but apply settings
            DialogResult = DialogResult.OK;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _currentItemKey = null;
            FillEditTextBoxes();
            SwitchUIMode(true);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentItemKey))
            {
                return;
            }
            FillEditTextBoxes(_currentItemKey);
            SwitchUIMode(true);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentItemKey))
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
            _ = _actions.Remove(_currentItemKey);

            listBoxActions.DataSource = _actions.Keys.ToList();

            FillEditTextBoxes();
        }

        private void buttonEditCancel_Click(object sender, EventArgs e)
        {
            FillEditTextBoxes(_currentItemKey);
            SwitchUIMode(false);
        }

        private void buttonEditOK_Click(object sender, EventArgs e)
        {
            string newKey = textBoxName.Text.Trim();
            if (string.IsNullOrEmpty(newKey))
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
            // if _currentItemKey is null, add a new item
            // else edit already existing one
            if (string.IsNullOrEmpty(_currentItemKey))
            {
                if (_actions.ContainsKey(newKey))
                {
                    _ = MessageBox.Show(
                    Properties.Resources.strNameAlreadyExists,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (newKey != _currentItemKey && _actions.ContainsKey(newKey))
                {
                    _ = MessageBox.Show(
                    Properties.Resources.strNameAlreadyExists,
                    Properties.Resources.strError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    return;
                }
                _actions.Remove(_currentItemKey);
            }
            _actions.Add(newKey, newCommand);
            _currentItemKey = newKey;

            var keys = _actions.Keys.ToList();
            listBoxActions.DataSource = keys;
            int index = keys.IndexOf(newKey);
            if (index >= 0)
            {
                listBoxActions.SelectedIndex = index;
            }

            SwitchUIMode(false);
        }

        private void listBoxActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // remember the selected item key
            if (listBoxActions.SelectedItem is string selectedKey
                && !string.IsNullOrEmpty(selectedKey))
            {

                _currentItemKey = selectedKey;
                textBoxName.Text = _currentItemKey;
                textBoxCommand.Text = _actions[_currentItemKey];

            }
            else
            {
                _currentItemKey = null;
                textBoxName.Text = string.Empty;
                textBoxCommand.Text = string.Empty;
            }
        }

        private void listBoxActions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonEdit.PerformClick();
        }
    }
}
