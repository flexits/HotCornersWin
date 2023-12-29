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
            groupBoxMulti = new GroupBox();
            radioButtonSept = new RadioButton();
            radioButtonPrim = new RadioButton();
            radioButtonVirt = new RadioButton();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxCorners.SuspendLayout();
            groupBoxMulti.SuspendLayout();
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
            groupBoxCorners.Size = new Size(680, 210);
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
            buttonApply.Location = new Point(617, 336);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 4;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(12, 336);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // groupBoxMulti
            // 
            groupBoxMulti.Controls.Add(radioButtonSept);
            groupBoxMulti.Controls.Add(radioButtonPrim);
            groupBoxMulti.Controls.Add(radioButtonVirt);
            groupBoxMulti.Location = new Point(12, 228);
            groupBoxMulti.Name = "groupBoxMulti";
            groupBoxMulti.Size = new Size(680, 102);
            groupBoxMulti.TabIndex = 6;
            groupBoxMulti.TabStop = false;
            groupBoxMulti.Text = "Multi-monitor settings";
            // 
            // radioButtonSept
            // 
            radioButtonSept.AutoSize = true;
            radioButtonSept.Location = new Point(6, 72);
            radioButtonSept.Name = "radioButtonSept";
            radioButtonSept.Size = new Size(277, 19);
            radioButtonSept.TabIndex = 2;
            radioButtonSept.TabStop = true;
            radioButtonSept.Text = "Each physical monitor has the same hot corners";
            radioButtonSept.UseVisualStyleBackColor = true;
            // 
            // radioButtonPrim
            // 
            radioButtonPrim.AutoSize = true;
            radioButtonPrim.Location = new Point(6, 47);
            radioButtonPrim.Name = "radioButtonPrim";
            radioButtonPrim.Size = new Size(234, 19);
            radioButtonPrim.TabIndex = 1;
            radioButtonPrim.TabStop = true;
            radioButtonPrim.Text = "Hot corners on the primary display only";
            radioButtonPrim.UseVisualStyleBackColor = true;
            // 
            // radioButtonVirt
            // 
            radioButtonVirt.AutoSize = true;
            radioButtonVirt.Location = new Point(6, 22);
            radioButtonVirt.Name = "radioButtonVirt";
            radioButtonVirt.Size = new Size(339, 19);
            radioButtonVirt.TabIndex = 0;
            radioButtonVirt.TabStop = true;
            radioButtonVirt.Text = "Hot corners on the virtual display (the four farmost corners)";
            radioButtonVirt.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            AcceptButton = buttonApply;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(706, 369);
            Controls.Add(groupBoxMulti);
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
            groupBoxMulti.ResumeLayout(false);
            groupBoxMulti.PerformLayout();
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
        private GroupBox groupBoxMulti;
        private RadioButton radioButtonSept;
        private RadioButton radioButtonPrim;
        private RadioButton radioButtonVirt;
    }
}