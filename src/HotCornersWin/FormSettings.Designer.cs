﻿namespace HotCornersWin
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
            labelDelay2 = new Label();
            numericUpDownDelayLB = new NumericUpDown();
            labelDelay4 = new Label();
            numericUpDownDelayRB = new NumericUpDown();
            labelDelay3 = new Label();
            numericUpDownDelayRT = new NumericUpDown();
            labelDelay1 = new Label();
            numericUpDownDelayLT = new NumericUpDown();
            comboBoxRB = new ComboBox();
            comboBoxRT = new ComboBox();
            comboBoxLB = new ComboBox();
            buttonApply = new Button();
            buttonCancel = new Button();
            groupBoxMulti = new GroupBox();
            radioButtonSept = new RadioButton();
            radioButtonPrim = new RadioButton();
            radioButtonVirt = new RadioButton();
            groupBoxAdvanced = new GroupBox();
            checkBoxDisableOnFullscreen = new CheckBox();
            numericUpDownPoll = new NumericUpDown();
            labelPollInterval = new Label();
            labelRadius = new Label();
            numericUpDownRadius = new NumericUpDown();
            buttonDebugInfo = new Button();
            buttonCustomActions = new Button();
            groupBoxColorScheme = new GroupBox();
            radioButtonSystem = new RadioButton();
            radioButtonDark = new RadioButton();
            radioButtonLight = new RadioButton();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxCorners.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayLB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayRB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayRT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayLT).BeginInit();
            groupBoxMulti.SuspendLayout();
            groupBoxAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPoll).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRadius).BeginInit();
            groupBoxColorScheme.SuspendLayout();
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
            groupBoxCorners.Controls.Add(labelDelay2);
            groupBoxCorners.Controls.Add(numericUpDownDelayLB);
            groupBoxCorners.Controls.Add(labelDelay4);
            groupBoxCorners.Controls.Add(numericUpDownDelayRB);
            groupBoxCorners.Controls.Add(labelDelay3);
            groupBoxCorners.Controls.Add(numericUpDownDelayRT);
            groupBoxCorners.Controls.Add(labelDelay1);
            groupBoxCorners.Controls.Add(numericUpDownDelayLT);
            groupBoxCorners.Controls.Add(comboBoxRB);
            groupBoxCorners.Controls.Add(comboBoxRT);
            groupBoxCorners.Controls.Add(comboBoxLB);
            groupBoxCorners.Controls.Add(pictureBox1);
            groupBoxCorners.Controls.Add(comboBoxLT);
            groupBoxCorners.Location = new Point(12, 12);
            groupBoxCorners.Name = "groupBoxCorners";
            groupBoxCorners.Size = new Size(680, 210);
            groupBoxCorners.TabIndex = 1;
            groupBoxCorners.TabStop = false;
            groupBoxCorners.Text = "Choose actions for the hot corners:";
            // 
            // labelDelay2
            // 
            labelDelay2.AutoSize = true;
            labelDelay2.Location = new Point(6, 183);
            labelDelay2.Name = "labelDelay2";
            labelDelay2.Size = new Size(137, 15);
            labelDelay2.TabIndex = 13;
            labelDelay2.Text = "Action delay, poll cycles:";
            // 
            // numericUpDownDelayLB
            // 
            numericUpDownDelayLB.Location = new Point(155, 181);
            numericUpDownDelayLB.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownDelayLB.Name = "numericUpDownDelayLB";
            numericUpDownDelayLB.Size = new Size(51, 23);
            numericUpDownDelayLB.TabIndex = 5;
            // 
            // labelDelay4
            // 
            labelDelay4.AutoSize = true;
            labelDelay4.Location = new Point(474, 183);
            labelDelay4.Name = "labelDelay4";
            labelDelay4.Size = new Size(137, 15);
            labelDelay4.TabIndex = 11;
            labelDelay4.Text = "Action delay, poll cycles:";
            // 
            // numericUpDownDelayRB
            // 
            numericUpDownDelayRB.Location = new Point(623, 181);
            numericUpDownDelayRB.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownDelayRB.Name = "numericUpDownDelayRB";
            numericUpDownDelayRB.Size = new Size(51, 23);
            numericUpDownDelayRB.TabIndex = 9;
            // 
            // labelDelay3
            // 
            labelDelay3.AutoSize = true;
            labelDelay3.Location = new Point(474, 53);
            labelDelay3.Name = "labelDelay3";
            labelDelay3.Size = new Size(137, 15);
            labelDelay3.TabIndex = 9;
            labelDelay3.Text = "Action delay, poll cycles:";
            // 
            // numericUpDownDelayRT
            // 
            numericUpDownDelayRT.Location = new Point(623, 51);
            numericUpDownDelayRT.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownDelayRT.Name = "numericUpDownDelayRT";
            numericUpDownDelayRT.Size = new Size(51, 23);
            numericUpDownDelayRT.TabIndex = 7;
            // 
            // labelDelay1
            // 
            labelDelay1.AutoSize = true;
            labelDelay1.Location = new Point(6, 53);
            labelDelay1.Name = "labelDelay1";
            labelDelay1.Size = new Size(137, 15);
            labelDelay1.TabIndex = 7;
            labelDelay1.Text = "Action delay, poll cycles:";
            // 
            // numericUpDownDelayLT
            // 
            numericUpDownDelayLT.Location = new Point(155, 51);
            numericUpDownDelayLT.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownDelayLT.Name = "numericUpDownDelayLT";
            numericUpDownDelayLT.Size = new Size(51, 23);
            numericUpDownDelayLT.TabIndex = 3;
            // 
            // comboBoxRB
            // 
            comboBoxRB.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRB.FormattingEnabled = true;
            comboBoxRB.Location = new Point(474, 152);
            comboBoxRB.Name = "comboBoxRB";
            comboBoxRB.Size = new Size(200, 23);
            comboBoxRB.TabIndex = 8;
            // 
            // comboBoxRT
            // 
            comboBoxRT.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRT.FormattingEnabled = true;
            comboBoxRT.Location = new Point(474, 22);
            comboBoxRT.Name = "comboBoxRT";
            comboBoxRT.Size = new Size(200, 23);
            comboBoxRT.TabIndex = 6;
            // 
            // comboBoxLB
            // 
            comboBoxLB.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLB.FormattingEnabled = true;
            comboBoxLB.Location = new Point(6, 152);
            comboBoxLB.Name = "comboBoxLB";
            comboBoxLB.Size = new Size(200, 23);
            comboBoxLB.TabIndex = 4;
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(586, 399);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(108, 23);
            buttonApply.TabIndex = 20;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(14, 399);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(108, 23);
            buttonCancel.TabIndex = 21;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxMulti
            // 
            groupBoxMulti.Controls.Add(radioButtonSept);
            groupBoxMulti.Controls.Add(radioButtonPrim);
            groupBoxMulti.Controls.Add(radioButtonVirt);
            groupBoxMulti.Location = new Point(12, 228);
            groupBoxMulti.Name = "groupBoxMulti";
            groupBoxMulti.Size = new Size(362, 102);
            groupBoxMulti.TabIndex = 10;
            groupBoxMulti.TabStop = false;
            groupBoxMulti.Text = "Multi-monitor settings";
            // 
            // radioButtonSept
            // 
            radioButtonSept.AutoSize = true;
            radioButtonSept.Location = new Point(6, 72);
            radioButtonSept.Name = "radioButtonSept";
            radioButtonSept.Size = new Size(277, 19);
            radioButtonSept.TabIndex = 13;
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
            radioButtonPrim.TabIndex = 12;
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
            radioButtonVirt.TabIndex = 11;
            radioButtonVirt.TabStop = true;
            radioButtonVirt.Text = "Hot corners on the virtual display (the four farmost corners)";
            radioButtonVirt.UseVisualStyleBackColor = true;
            // 
            // groupBoxAdvanced
            // 
            groupBoxAdvanced.Controls.Add(checkBoxDisableOnFullscreen);
            groupBoxAdvanced.Controls.Add(numericUpDownPoll);
            groupBoxAdvanced.Controls.Add(labelPollInterval);
            groupBoxAdvanced.Controls.Add(labelRadius);
            groupBoxAdvanced.Controls.Add(numericUpDownRadius);
            groupBoxAdvanced.Location = new Point(380, 228);
            groupBoxAdvanced.Name = "groupBoxAdvanced";
            groupBoxAdvanced.Size = new Size(312, 102);
            groupBoxAdvanced.TabIndex = 14;
            groupBoxAdvanced.TabStop = false;
            groupBoxAdvanced.Text = "Advanced settings:";
            // 
            // checkBoxDisableOnFullscreen
            // 
            checkBoxDisableOnFullscreen.AutoSize = true;
            checkBoxDisableOnFullscreen.Location = new Point(6, 73);
            checkBoxDisableOnFullscreen.Name = "checkBoxDisableOnFullscreen";
            checkBoxDisableOnFullscreen.Size = new Size(83, 19);
            checkBoxDisableOnFullscreen.TabIndex = 17;
            checkBoxDisableOnFullscreen.Text = "checkBox1";
            checkBoxDisableOnFullscreen.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPoll
            // 
            numericUpDownPoll.Location = new Point(237, 43);
            numericUpDownPoll.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numericUpDownPoll.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownPoll.Name = "numericUpDownPoll";
            numericUpDownPoll.Size = new Size(69, 23);
            numericUpDownPoll.TabIndex = 16;
            numericUpDownPoll.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelPollInterval
            // 
            labelPollInterval.AutoSize = true;
            labelPollInterval.Location = new Point(6, 51);
            labelPollInterval.Name = "labelPollInterval";
            labelPollInterval.Size = new Size(192, 15);
            labelPollInterval.TabIndex = 2;
            labelPollInterval.Text = "Cursor position polling interval, ms";
            // 
            // labelRadius
            // 
            labelRadius.AutoSize = true;
            labelRadius.Location = new Point(6, 24);
            labelRadius.Name = "labelRadius";
            labelRadius.Size = new Size(38, 15);
            labelRadius.TabIndex = 1;
            labelRadius.Text = "label1";
            // 
            // numericUpDownRadius
            // 
            numericUpDownRadius.Location = new Point(237, 16);
            numericUpDownRadius.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            numericUpDownRadius.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownRadius.Name = "numericUpDownRadius";
            numericUpDownRadius.Size = new Size(69, 23);
            numericUpDownRadius.TabIndex = 15;
            numericUpDownRadius.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // buttonDebugInfo
            // 
            buttonDebugInfo.Location = new Point(128, 336);
            buttonDebugInfo.Name = "buttonDebugInfo";
            buttonDebugInfo.Size = new Size(108, 23);
            buttonDebugInfo.TabIndex = 18;
            buttonDebugInfo.Text = "Debug info";
            buttonDebugInfo.UseVisualStyleBackColor = true;
            buttonDebugInfo.Visible = false;
            // 
            // buttonCustomActions
            // 
            buttonCustomActions.Location = new Point(14, 336);
            buttonCustomActions.Name = "buttonCustomActions";
            buttonCustomActions.Size = new Size(108, 23);
            buttonCustomActions.TabIndex = 19;
            buttonCustomActions.Text = "Custom actions";
            buttonCustomActions.UseVisualStyleBackColor = true;
            buttonCustomActions.Click += buttonCustomActions_Click;
            // 
            // groupBoxColorScheme
            // 
            groupBoxColorScheme.Controls.Add(radioButtonLight);
            groupBoxColorScheme.Controls.Add(radioButtonDark);
            groupBoxColorScheme.Controls.Add(radioButtonSystem);
            groupBoxColorScheme.Location = new Point(380, 336);
            groupBoxColorScheme.Name = "groupBoxColorScheme";
            groupBoxColorScheme.Size = new Size(312, 57);
            groupBoxColorScheme.TabIndex = 22;
            groupBoxColorScheme.TabStop = false;
            groupBoxColorScheme.Text = "groupBox1";
            // 
            // radioButtonSystem
            // 
            radioButtonSystem.AutoSize = true;
            radioButtonSystem.Location = new Point(6, 22);
            radioButtonSystem.Name = "radioButtonSystem";
            radioButtonSystem.Size = new Size(94, 19);
            radioButtonSystem.TabIndex = 0;
            radioButtonSystem.TabStop = true;
            radioButtonSystem.Text = "radioButton1";
            radioButtonSystem.UseVisualStyleBackColor = true;
            // 
            // radioButtonDark
            // 
            radioButtonDark.AutoSize = true;
            radioButtonDark.Location = new Point(106, 22);
            radioButtonDark.Name = "radioButtonDark";
            radioButtonDark.Size = new Size(94, 19);
            radioButtonDark.TabIndex = 1;
            radioButtonDark.TabStop = true;
            radioButtonDark.Text = "radioButton1";
            radioButtonDark.UseVisualStyleBackColor = true;
            // 
            // radioButtonLight
            // 
            radioButtonLight.AutoSize = true;
            radioButtonLight.Location = new Point(206, 22);
            radioButtonLight.Name = "radioButtonLight";
            radioButtonLight.Size = new Size(94, 19);
            radioButtonLight.TabIndex = 2;
            radioButtonLight.TabStop = true;
            radioButtonLight.Text = "radioButton2";
            radioButtonLight.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            AcceptButton = buttonApply;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(706, 434);
            Controls.Add(groupBoxColorScheme);
            Controls.Add(buttonCustomActions);
            Controls.Add(buttonDebugInfo);
            Controls.Add(groupBoxAdvanced);
            Controls.Add(groupBoxMulti);
            Controls.Add(buttonCancel);
            Controls.Add(buttonApply);
            Controls.Add(groupBoxCorners);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSettings";
            StartPosition = FormStartPosition.Manual;
            Text = "Settings - HotCornersWin";
            Load += FormSettings_Load;
            KeyDown += FormSettings_KeyDown;
            contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBoxCorners.ResumeLayout(false);
            groupBoxCorners.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayLB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayRB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayRT).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDelayLT).EndInit();
            groupBoxMulti.ResumeLayout(false);
            groupBoxMulti.PerformLayout();
            groupBoxAdvanced.ResumeLayout(false);
            groupBoxAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPoll).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRadius).EndInit();
            groupBoxColorScheme.ResumeLayout(false);
            groupBoxColorScheme.PerformLayout();
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
        private GroupBox groupBoxAdvanced;
        private Label labelRadius;
        private NumericUpDown numericUpDownRadius;
        private Button buttonDebugInfo;
        private Button buttonCustomActions;
        private CheckBox checkBoxDisableOnFullscreen;
        private NumericUpDown numericUpDownPoll;
        private Label labelPollInterval;
        private Label labelDelay1;
        private NumericUpDown numericUpDownDelayLT;
        private Label labelDelay2;
        private NumericUpDown numericUpDownDelayLB;
        private Label labelDelay4;
        private NumericUpDown numericUpDownDelayRB;
        private Label labelDelay3;
        private NumericUpDown numericUpDownDelayRT;
        private GroupBox groupBoxColorScheme;
        private RadioButton radioButtonLight;
        private RadioButton radioButtonDark;
        private RadioButton radioButtonSystem;
    }
}