using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace VoiSe.Audio;

internal sealed class RouteMixSampleProvider : ISampleProvider
{
    private readonly AudioRoute _route;
    private readonly FloatSampleQueue _micQueue;
    private readonly SoundboardTransport _soundboard;
    private readonly object _settingsSync = new();
    private bool _limiterEnabled;
    private float _limiterCeiling;
    private float _routeGain;
    private float[] _micScratch = Array.Empty<float>();
    private float[] _soundScratch = Array.Empty<float>();

    public RouteMixSampleProvider(
        WaveFormat waveFormat,
        AudioRoute route,
        FloatSampleQueue micQueue,
        SoundboardTransport soundboard,
        EffectSettings settings)
    {
        WaveFormat = waveFormat;
        _route = route;
        _micQueue = micQueue;
        _soundboard = soundboard;
        UpdateSettings(settings);
    }

    public WaveFormat WaveFormat { get; }

    public void UpdateSettings(EffectSettings settings)
    {
        lock (_settingsSync)
        {
            _limiterEnabled = settings.LimiterEnabled;
            _limiterCeiling = settings.LimiterEnabled ? Decibels.DbToLinear(settings.LimiterCeilingDb) : 1.0f;
            _routeGain = _route == AudioRoute.VirtualMicrophone
                ? Math.Clamp(settings.VirtualOutputGain, 0.0f, 2.0f)
                : Math.Clamp(settings.MonitorOutputGain, 0.0f, 2.0f);
        }
    }

    public int Read(float[] buffer, int offset, int count)
    {
        EnsureScratch(count);

        _micQueue.Read(_micScratch, 0, count);
        _soundboard.Read(_route, _soundScratch, 0, count);

        bool limiterEnabled;
        float limiterCeiling;
        float routeGain;
        lock (_settingsSync)
        {
            limiterEnabled = _limiterEnabled;
            limiterCeiling = _limiterCeiling;
            routeGain = _routeGain;
        }

        for (var i = 0; i < count; i++)
        {
            var mixed = (_micScratch[i] + _soundScratch[i]) * routeGain;
            buffer[offset + i] = limiterEnabled
                ? Math.Clamp(mixed, -limiterCeiling, limiterCeiling)
                : Math.Clamp(mixed, -1.0f, 1.0f);
        }

        return count;
    }

    private void EnsureScratch(int count)
    {
        if (_micScratch.Length < count)
        {
            _micScratch = new float[count];
        }

        if (_soundScratch.Length < count)
        {
            _soundScratch = new float[count];
        }
    }
}
