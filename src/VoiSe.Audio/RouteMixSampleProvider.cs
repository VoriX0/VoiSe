using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace VoiSe.Audio;

internal sealed class RouteMixSampleProvider : ISampleProvider
{
    private readonly AudioRoute _route;
    private readonly FloatSampleQueue _micQueue;
    private readonly SoundboardTransport _soundboard;
    private readonly float _limiterCeiling;
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
        _limiterCeiling = settings.LimiterEnabled ? Decibels.DbToLinear(settings.LimiterCeilingDb) : 1.0f;
    }

    public WaveFormat WaveFormat { get; }

    public int Read(float[] buffer, int offset, int count)
    {
        EnsureScratch(count);

        _micQueue.Read(_micScratch, 0, count);
        _soundboard.Read(_route, _soundScratch, 0, count);

        for (var i = 0; i < count; i++)
        {
            var mixed = _micScratch[i] + _soundScratch[i];
            buffer[offset + i] = Math.Clamp(mixed, -_limiterCeiling, _limiterCeiling);
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
