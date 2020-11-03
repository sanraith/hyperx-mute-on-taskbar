using SharpLib.Hid;
using SharpLib.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HyperXMuteTaskbar.Core
{
    internal sealed class HyperXHidHandler : IDisposable
    {
        public IReadOnlyList<Device> RegisteredDevices => myRegisteredDevices;

        public event EventHandler<bool> MicMuteChanged;

        public HyperXHidHandler(IntPtr windowHandle)
        {
            myWindowHandle = windowHandle;
        }

        public void ProcessInput(ref Message message)
        {
            myHidHandler.ProcessInput(ref message);
        }

        public void RegisterDevices()
        {
            DisposeHandlers();

            var devices = GetHyperXDevices();
            myRegisteredDevices.Clear();
            myRegisteredDevices.AddRange(devices.Values);
            if (devices.Count == 0)
            {
                //No device to register for, nothing to do here
                return;
            }

            uint flags = 0x100; /// RIDEV_INPUTSINK
            var rids = new RAWINPUTDEVICE[devices.Count];
            foreach (var (i, device) in devices.Values.Select((x, i) => (i, x)))
            {
                rids[i].usUsagePage = device.UsagePage;
                rids[i].usUsage = device.UsageCollection;
                rids[i].dwFlags = (RawInputDeviceFlags)flags;
                rids[i].hwndTarget = myWindowHandle;
            }

            myHidHandler = new Handler(rids);
            if (!myHidHandler.IsRegistered)
            {
                Debug.WriteLine("Failed to register raw input devices: " + Marshal.GetLastWin32Error().ToString());
            }

            myHidHandler.OnHidEvent += Handler_OnHidEvent;
        }

        public void Dispose()
        {
            DisposeHandlers();
        }

        private void DisposeHandlers()
        {
            if (myHidHandler != null)
            {
                myHidHandler.OnHidEvent -= Handler_OnHidEvent;
                myHidHandler.Dispose();
                myHidHandler = null;
            }
        }

        private void Handler_OnHidEvent(object sender, Event hidEvent)
        {
            Debug.Write($"Report: {ConvertBytesToString(hidEvent.InputReport)}");
            if (IsMuteReport(hidEvent))
            {
                Debug.WriteLine(" Mute report detected!");
                MicMuteChanged?.Invoke(this, IsMuted(hidEvent));
            }
            else
            {
                Debug.WriteLine("");
            }
        }

        private static Dictionary<uint, Device> GetHyperXDevices()
        {
            var treeViewDevices = new TreeView();
            RawInput.PopulateDeviceList(treeViewDevices);
            var devices = new Dictionary<uint, Device>();
            foreach (TreeNode node in treeViewDevices.Nodes)
            {
                Device device = (Device)node.Tag;
                if (IsHyperXMuteInputDevice(device))
                {
                    if (devices.ContainsKey(device.UsageId))
                    {
                        Debug.WriteLine("Similar device already registered!");
                    }
                    else
                    {
                        devices.Add(device.UsageId, device);
                    }
                }
            }

            return devices;
        }

        private static bool IsHyperXMuteInputDevice(Device device)
        {
            return device != null && device.Product != null
                && device.Product.Contains("HyperX")
                && device.UsagePage == 0xFFC0 && device.UsageCollection == 0x0001;
        }

        private static bool IsMuteReport(Event hidEvent)
        {
            // TODO check other properties of the event.
            byte[] r = hidEvent.InputReport;
            return r.Length == 2 && r[0] == 0x65 && (r[1] == 0x00 || r[1] == 0x04);
        }

        private static bool IsMuted(Event hidEvent) => hidEvent.InputReport[1] == 0x04;

        private static string ConvertBytesToString(byte[] bytes) => string.Join(" ", bytes.Select(x => $"{x:x2}").ToList());

        private Handler myHidHandler;
        private readonly List<Device> myRegisteredDevices = new List<Device>();
        private readonly IntPtr myWindowHandle;
    }
}
