using HyperXMuteTaskbar.Core;
using HyperXMuteTaskbar.Properties;
using SharpLib.Win32;
using System.Diagnostics;
using System.Windows.Forms;

namespace HyperXMuteTaskbar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Visible = false;
            myHidHandler = new HyperXHidHandler(Handle);
            myHidHandler.MicMuteChanged += MyHidHandler_MicMuteChanged;
            myHidHandler.RegisterDevices();
        }

        /// <summary>
        /// Route input messages to HidHandler.
        /// </summary>
        protected override void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case Const.WM_INPUT:
                    myHidHandler.ProcessInput(ref message);
                    break;
            }
            base.WndProc(ref message);
        }

        /// <summary>
        /// Do not show the main window unless needed.
        /// </summary>
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(Visible);
        }

        private void MyHidHandler_MicMuteChanged(object sender, bool isMuted)
        {
            Debug.WriteLine($"Mic is {(isMuted ? "muted" : "unmuted")}.");
            taskbarIcon.Icon = isMuted ? Resources.MicMuted : Resources.MicActive;
        }

        private void TaskbarIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private readonly HyperXHidHandler myHidHandler;
    }
}
