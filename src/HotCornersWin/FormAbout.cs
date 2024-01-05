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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _ = new Process
            {
                StartInfo = new ProcessStartInfo("https://github.com/Lisa24Jackson")
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
