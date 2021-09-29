namespace HyperXMuteTaskbar.Core
{
    internal sealed class BeepConfig
    {
        public bool IsBeeping { get; set; } = true;
        public int BeepFrequencyHz { get; set; } = 18000;
        public int BeepDurationSeconds { get; set; } = 60;
        public int BeepIntervalSeconds { get; set; } = 240;
    }
}
