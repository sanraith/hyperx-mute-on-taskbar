using HyperXMuteTaskbar.Core;
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

        private void MyHidHandler_MicMuteChanged(object sender, bool isMuted)
        {
            Debug.WriteLine($"Mic is {(isMuted ? "muted" : "unmuted")}.");
        }

        private readonly HyperXHidHandler myHidHandler;
    }
}
