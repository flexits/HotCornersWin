namespace HotCornersWin
{
    partial class FormSettings
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            pictureBox1 = new PictureBox();
            comboBoxLT = new ComboBox();
            groupBoxCorners = new GroupBox();
            comboBoxRB = new ComboBox();
            comboBoxRT = new ComboBox();
            comboBoxLB = new ComboBox();
            buttonApply = new Button();
            buttonCancel = new Button();
            groupBoxStart = new GroupBox();
            radioButtonAutoAllUsr = new RadioButton();
            radioButtonAutoCurUsr = new RadioButton();
            radioButtonAutoOff = new RadioButton();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxCorners.SuspendLayout();
            groupBoxStart.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripSeparator1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 32);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(180, 22);
            toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.display;
            pictureBox1.Location = new Point(212, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 181);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // comboBoxLT
            // 
            comboBoxLT.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLT.FormattingEnabled = true;
            comboBoxLT.Location = new Point(6, 22);
            comboBoxLT.Name = "comboBoxLT";
            comboBoxLT.Size = new Size(200, 23);
            comboBoxLT.TabIndex = 2;
            // 
            // groupBoxCorners
            // 
            groupBoxCorners.Controls.Add(comboBoxRB);
            groupBoxCorners.Controls.Add(comboBoxRT);
            groupBoxCorners.Controls.Add(comboBoxLB);
            groupBoxCorners.Controls.Add(pictureBox1);
            groupBoxCorners.Controls.Add(comboBoxLT);
            groupBoxCorners.Location = new Point(12, 12);
            groupBoxCorners.Name = "groupBoxCorners";
            groupBoxCorners.Size = new Size(680, 248);
            groupBoxCorners.TabIndex = 3;
            groupBoxCorners.TabStop = false;
            groupBoxCorners.Text = "Choose actions for the hot corners:";
            // 
            // comboBoxRB
            // 
            comboBoxRB.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRB.FormattingEnabled = true;
            comboBoxRB.Location = new Point(474, 180);
            comboBoxRB.Name = "comboBoxRB";
            comboBoxRB.Size = new Size(200, 23);
            comboBoxRB.TabIndex = 5;
            // 
            // comboBoxRT
            // 
            comboBoxRT.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRT.FormattingEnabled = true;
            comboBoxRT.Location = new Point(474, 22);
            comboBoxRT.Name = "comboBoxRT";
            comboBoxRT.Size = new Size(200, 23);
            comboBoxRT.TabIndex = 4;
            // 
            // comboBoxLB
            // 
            comboBoxLB.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLB.FormattingEnabled = true;
            comboBoxLB.Location = new Point(6, 180);
            comboBoxLB.Name = "comboBoxLB";
            comboBoxLB.Size = new Size(200, 23);
            comboBoxLB.TabIndex = 3;
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(617, 374);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 4;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(12, 374);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // groupBoxStart
            // 
            groupBoxStart.Controls.Add(radioButtonAutoAllUsr);
            groupBoxStart.Controls.Add(radioButtonAutoCurUsr);
            groupBoxStart.Controls.Add(radioButtonAutoOff);
            groupBoxStart.Location = new Point(12, 266);
            groupBoxStart.Name = "groupBoxStart";
            groupBoxStart.Size = new Size(680, 102);
            groupBoxStart.TabIndex = 6;
            groupBoxStart.TabStop = false;
            groupBoxStart.Text = "Autostart settings";
            // 
            // radioButtonAutoAllUsr
            // 
            radioButtonAutoAllUsr.AutoSize = true;
            radioButtonAutoAllUsr.Location = new Point(6, 72);
            radioButtonAutoAllUsr.Name = "radioButtonAutoAllUsr";
            radioButtonAutoAllUsr.Size = new Size(190, 19);
            radioButtonAutoAllUsr.TabIndex = 2;
            radioButtonAutoAllUsr.TabStop = true;
            radioButtonAutoAllUsr.Text = "Start with Windows for all users";
            radioButtonAutoAllUsr.UseVisualStyleBackColor = true;
            // 
            // radioButtonAutoCurUsr
            // 
            radioButtonAutoCurUsr.AutoSize = true;
            radioButtonAutoCurUsr.Location = new Point(6, 47);
            radioButtonAutoCurUsr.Name = "radioButtonAutoCurUsr";
            radioButtonAutoCurUsr.Size = new Size(231, 19);
            radioButtonAutoCurUsr.TabIndex = 1;
            radioButtonAutoCurUsr.TabStop = true;
            radioButtonAutoCurUsr.Text = "Start with Windows for the current user";
            radioButtonAutoCurUsr.UseVisualStyleBackColor = true;
            // 
            // radioButtonAutoOff
            // 
            radioButtonAutoOff.AutoSize = true;
            radioButtonAutoOff.Location = new Point(6, 22);
            radioButtonAutoOff.Name = "radioButtonAutoOff";
            radioButtonAutoOff.Size = new Size(91, 19);
            radioButtonAutoOff.TabIndex = 0;
            radioButtonAutoOff.TabStop = true;
            radioButtonAutoOff.Text = "No autostart";
            radioButtonAutoOff.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            AcceptButton = buttonApply;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(706, 410);
            Controls.Add(groupBoxStart);
            Controls.Add(buttonCancel);
            Controls.Add(buttonApply);
            Controls.Add(groupBoxCorners);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSettings";
            StartPosition = FormStartPosition.Manual;
            Text = "Settings - HotCornersWin";
            Load += FormSettings_Load;
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBoxCorners.ResumeLayout(false);
            groupBoxStart.ResumeLayout(false);
            groupBoxStart.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private PictureBox pictureBox1;
        private ComboBox comboBoxLT;
        private GroupBox groupBoxCorners;
        private Button buttonApply;
        private Button buttonCancel;
        private ComboBox comboBoxRB;
        private ComboBox comboBoxRT;
        private ComboBox comboBoxLB;
        private GroupBox groupBoxStart;
        private RadioButton radioButtonAutoAllUsr;
        private RadioButton radioButtonAutoCurUsr;
        private RadioButton radioButtonAutoOff;
    }
}