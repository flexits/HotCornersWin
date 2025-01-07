using System.Diagnostics;
using System.Reflection;

namespace HotCornersWin
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            if (version is not null)
            {
                labelVer.Text = $"v.{version.Major}.{version.Minor}.{version.Build}";
            }
            else
            {
                labelVer.Text = string.Empty;
            }
#if PORTABLE
            labelPortable.Visible = true;
#else
            labelPortable.Visible = false;
#endif
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
