using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyperXMuteTaskbar.Core
{
    internal sealed class Beeper
    {
        public bool IsTurnedOn => myCancellationTokenSource != null;

        public Beeper()
        {
            LoadConfig();
            if (myConfig.IsBeeping)
            {
                TurnOnInternal();
            }
        }

        public void TurnOn()
        {
            if (myCancellationTokenSource != null) { return; }

            myConfig.IsBeeping = true;
            TurnOnInternal();
            SaveConfig();
        }

        public void TurnOff()
        {
            if (myCancellationTokenSource == null) { return; }

            myConfig.IsBeeping = false;
            myCancellationTokenSource.Cancel();
            myCancellationTokenSource = null;
            SaveConfig();
        }

        private void TurnOnInternal()
        {
            myCancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () => await RunForever(myCancellationTokenSource.Token));
        }

        private void SaveConfig()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                var settings = config.AppSettings;
                settings.Settings.Remove("IsBeeping");
                settings.Settings.Remove("BeepFrequencyHz");
                settings.Settings.Remove("BeepDurationSeconds");
                settings.Settings.Remove("BeepIntervalSeconds");
                settings.Settings.Add("IsBeeping", myConfig.IsBeeping.ToString());
                settings.Settings.Add("BeepFrequencyHz", myConfig.BeepFrequencyHz.ToString());
                settings.Settings.Add("BeepDurationSeconds", myConfig.BeepDurationSeconds.ToString());
                settings.Settings.Add("BeepIntervalSeconds", myConfig.BeepIntervalSeconds.ToString());
                config.Save();
            }
            catch (ConfigurationErrorsException)
            {
                // Ignore config saving errors
            }
        }

        private void LoadConfig()
        {
            var settings = ConfigurationManager.AppSettings;
            myConfig = new BeepConfig
            {
                IsBeeping = Convert.ToBoolean(settings["IsBeeping"]),
                BeepFrequencyHz = Convert.ToInt32(settings["BeepFrequencyHz"]),
                BeepDurationSeconds = Convert.ToInt32(settings["BeepDurationSeconds"]),
                BeepIntervalSeconds = Convert.ToInt32(settings["BeepIntervalSeconds"])
            };
        }

        private async Task RunForever(CancellationToken ct)
        {
            var beepFrequency = myConfig.BeepFrequencyHz;
            var beepLength = TimeSpan.FromSeconds(myConfig.BeepDurationSeconds);
            var beepInterval = TimeSpan.FromSeconds(myConfig.BeepIntervalSeconds);

            while (true)
            {
                Console.Beep(beepFrequency, (int)beepLength.TotalMilliseconds);
                try
                {
                    await Task.Delay(beepInterval, ct);
                }
                catch (TaskCanceledException)
                {
                    // Stop task because of expected cancellation.
                    return;
                }
            }
        }

        private CancellationTokenSource myCancellationTokenSource;
        private BeepConfig myConfig;
    }
}
