using HyperXMuteTaskbar.Core;
using HyperXMuteTaskbar.Properties;
using SharpLib.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyperXMuteTaskbar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Visible = false;
            taskbarIcon.Icon = Resources.MicUnknown;

            myHidHandler = new HyperXHidHandler(Handle);
            myHidHandler.MicMuteChanged += MyHidHandler_MicMuteChanged;
            _ = RegisterHidHandlerDevicesAsync();
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

        private async Task RegisterHidHandlerDevicesAsync()
        {
            BeginInvoke(new InvokeDelegate(() =>
            {
                registerDevicesMenuItem.Enabled = false;
                deviceCountToolStripMenuItem.Text = $"Looking for Connected devices...";
            }));

            myHidHandler.RegisterDevices();
            while (!myHidHandler.RegisteredDevices.Any())
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                myHidHandler.RegisterDevices();
            }

            BeginInvoke(new InvokeDelegate(() =>
            {
                registerDevicesMenuItem.Enabled = true;
                deviceCountToolStripMenuItem.Text = $"Connected devices: {myHidHandler.RegisteredDevices.Count}";
            }));
        }

        private void MyHidHandler_MicMuteChanged(object sender, bool isMuted)
        {
            Debug.WriteLine($"Mic is {(isMuted ? "muted" : "unmuted")}.");
            taskbarIcon.Icon = isMuted ? Resources.MicMuted : Resources.MicActive;
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void RegisterDevicesMenuItem_Click(object sender, EventArgs e)
        {
            _ = RegisterHidHandlerDevicesAsync();
        }

        private delegate void InvokeDelegate();
        private readonly HyperXHidHandler myHidHandler;
    }
}
