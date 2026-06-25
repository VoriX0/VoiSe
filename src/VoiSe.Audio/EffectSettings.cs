namespace VoiSe.Audio;

public sealed class EffectSettings
{
    public float InputGainDb { get; set; } = 0.0f;
    public bool GateEnabled { get; set; } = true;
    public float GateThresholdDb { get; set; } = -45.0f;
    public bool CompressorEnabled { get; set; } = true;
    public float CompressorThresholdDb { get; set; } = -18.0f;
    public float CompressorRatio { get; set; } = 3.0f;
    public float VoiceGainDb { get; set; } = 0.0f;
    public bool LimiterEnabled { get; set; } = true;
    public float LimiterCeilingDb { get; set; } = -1.0f;

    // Master output gain for the final virtual microphone mix.
    public float VirtualOutputGain { get; set; } = 1.0f;

    // Voice monitoring is independent from SoundBoard monitoring.
    // 0 = do not hear own processed voice in headphones, 1 = full voice monitor.
    public float VoiceMonitorGain { get; set; } = 0.0f;
}
