namespace HotCornersWin
{
    partial class FormActionsEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxActions = new ListBox();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonRemove = new Button();
            groupBoxEdit = new GroupBox();
            buttonEditCancel = new Button();
            buttonEditOK = new Button();
            textBoxCommand = new TextBox();
            labelCommand = new Label();
            textBoxName = new TextBox();
            labelName = new Label();
            buttonCancel = new Button();
            buttonSave = new Button();
            groupBoxEdit.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxActions
            // 
            listBoxActions.FormattingEnabled = true;
            listBoxActions.ItemHeight = 15;
            listBoxActions.Location = new Point(12, 12);
            listBoxActions.Name = "listBoxActions";
            listBoxActions.Size = new Size(230, 139);
            listBoxActions.TabIndex = 0;
            listBoxActions.SelectedIndexChanged += listBoxActions_SelectedIndexChanged;
            listBoxActions.MouseDoubleClick += listBoxActions_MouseDoubleClick;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(248, 12);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 38);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(248, 56);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 38);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "Edit";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new Point(248, 100);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(75, 38);
            buttonRemove.TabIndex = 3;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // groupBoxEdit
            // 
            groupBoxEdit.Controls.Add(buttonEditCancel);
            groupBoxEdit.Controls.Add(buttonEditOK);
            groupBoxEdit.Controls.Add(textBoxCommand);
            groupBoxEdit.Controls.Add(labelCommand);
            groupBoxEdit.Controls.Add(textBoxName);
            groupBoxEdit.Controls.Add(labelName);
            groupBoxEdit.Enabled = false;
            groupBoxEdit.Location = new Point(329, 5);
            groupBoxEdit.Name = "groupBoxEdit";
            groupBoxEdit.Size = new Size(275, 143);
            groupBoxEdit.TabIndex = 4;
            groupBoxEdit.TabStop = false;
            // 
            // buttonEditCancel
            // 
            buttonEditCancel.Location = new Point(6, 110);
            buttonEditCancel.Name = "buttonEditCancel";
            buttonEditCancel.Size = new Size(75, 23);
            buttonEditCancel.TabIndex = 8;
            buttonEditCancel.Text = "Cancel";
            buttonEditCancel.UseVisualStyleBackColor = true;
            buttonEditCancel.Click += buttonEditCancel_Click;
            // 
            // buttonEditOK
            // 
            buttonEditOK.Location = new Point(194, 110);
            buttonEditOK.Name = "buttonEditOK";
            buttonEditOK.Size = new Size(75, 23);
            buttonEditOK.TabIndex = 7;
            buttonEditOK.Text = "Finish";
            buttonEditOK.UseVisualStyleBackColor = true;
            buttonEditOK.Click += buttonEditOK_Click;
            // 
            // textBoxCommand
            // 
            textBoxCommand.Location = new Point(6, 76);
            textBoxCommand.Name = "textBoxCommand";
            textBoxCommand.Size = new Size(263, 23);
            textBoxCommand.TabIndex = 6;
            // 
            // labelCommand
            // 
            labelCommand.AutoSize = true;
            labelCommand.Location = new Point(6, 58);
            labelCommand.Name = "labelCommand";
            labelCommand.Size = new Size(38, 15);
            labelCommand.TabIndex = 2;
            labelCommand.Text = "label2";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(6, 32);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(263, 23);
            textBoxName.TabIndex = 5;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(6, 14);
            labelName.Name = "labelName";
            labelName.Size = new Size(38, 15);
            labelName.TabIndex = 0;
            labelName.Text = "label1";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(12, 157);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(108, 23);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(496, 157);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(108, 23);
            buttonSave.TabIndex = 9;
            buttonSave.Text = "Save changes";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonApply_Click;
            // 
            // FormActionsEditor
            // 
            AcceptButton = buttonSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(616, 191);
            Controls.Add(buttonCancel);
            Controls.Add(buttonSave);
            Controls.Add(groupBoxEdit);
            Controls.Add(buttonRemove);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(listBoxActions);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormActionsEditor";
            Text = "FormActionsEditor";
            groupBoxEdit.ResumeLayout(false);
            groupBoxEdit.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxActions;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonRemove;
        private GroupBox groupBoxEdit;
        private Label labelName;
        private Button buttonEditCancel;
        private Button buttonEditOK;
        private TextBox textBoxCommand;
        private Label labelCommand;
        private TextBox textBoxName;
        private Button buttonCancel;
        private Button buttonSave;
    }
}