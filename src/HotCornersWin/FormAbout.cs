using System.Diagnostics;

namespace HotCornersWin
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
        }

        private void linkLabelAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = new Process
            {
                StartInfo = new ProcessStartInfo("https://github.com/flexits/")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = new Process
            {
                StartInfo = new ProcessStartInfo("https://uxwing.com/")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = new Process
            {
                StartInfo = new ProcessStartInfo("https://icons8.com/icon/3pKFQN9sPxow/layout")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = new Process
            {
                StartInfo = new ProcessStartInfo("https://github.com/gmamaladze/globalmousekeyhook")
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
