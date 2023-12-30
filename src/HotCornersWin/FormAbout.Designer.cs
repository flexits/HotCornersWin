namespace HotCornersWin
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            labelName = new Label();
            labelVer = new Label();
            labelDescription = new Label();
            linkLabelAuthor = new LinkLabel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            label4 = new Label();
            linkLabel2 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            label5 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            labelName.Location = new Point(12, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(152, 25);
            labelName.TabIndex = 0;
            labelName.Text = "HotCornersWin";
            // 
            // labelVer
            // 
            labelVer.AutoSize = true;
            labelVer.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            labelVer.Location = new Point(170, 9);
            labelVer.Name = "labelVer";
            labelVer.Size = new Size(59, 25);
            labelVer.TabIndex = 1;
            labelVer.Text = "v0.6.1";
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Location = new Point(12, 50);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(265, 15);
            labelDescription.TabIndex = 2;
            labelDescription.Text = "Add MacOS hot corners function to Windows 10!";
            // 
            // linkLabelAuthor
            // 
            linkLabelAuthor.AutoSize = true;
            linkLabelAuthor.Location = new Point(68, 87);
            linkLabelAuthor.Name = "linkLabelAuthor";
            linkLabelAuthor.Size = new Size(116, 15);
            linkLabelAuthor.TabIndex = 3;
            linkLabelAuthor.TabStop = true;
            linkLabelAuthor.Text = "Alexander Korostelin";
            linkLabelAuthor.LinkClicked += linkLabelAuthor_LinkClicked;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 87);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 4;
            label1.Text = "Author: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 124);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 5;
            label2.Text = "Attributions:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 143);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "Icons by";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(91, 143);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(41, 15);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Icons8";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 158);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 8;
            label4.Text = "Icons by";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(91, 158);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(50, 15);
            linkLabel2.TabIndex = 9;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "UXWing";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(34, 173);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(127, 15);
            linkLabel3.TabIndex = 10;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "MouseKeyHook library";
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 208);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 11;
            label5.Text = "License terms:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 226);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(267, 203);
            textBox1.TabIndex = 12;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 445);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(linkLabel3);
            Controls.Add(linkLabel2);
            Controls.Add(label4);
            Controls.Add(linkLabel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(linkLabelAuthor);
            Controls.Add(labelDescription);
            Controls.Add(labelVer);
            Controls.Add(labelName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAbout";
            Text = "FormAbout";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private Label labelVer;
        private Label labelDescription;
        private LinkLabel linkLabelAuthor;
        private Label label1;
        private Label label2;
        private Label label3;
        private LinkLabel linkLabel1;
        private Label label4;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel3;
        private Label label5;
        private TextBox textBox1;
    }
}