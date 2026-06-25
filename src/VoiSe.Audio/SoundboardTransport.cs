using NAudio.Wave;

namespace VoiSe.Audio;

public sealed class SoundboardTransport
{
    private readonly WaveFormat _format;
    private readonly object _sync = new();
    private ActiveSound? _active;

    public SoundboardTransport(WaveFormat format)
    {
        _format = format;
    }

    public bool IsPlaying
    {
        get
        {
            lock (_sync)
            {
                return _active is not null;
            }
        }
    }

    public void Play(string filePath, float virtualVolume, float monitorVolume, int virtualDelayMs)
    {
        var data = SoundFileLoader.LoadToFormat(filePath, _format);
        var delaySamples = Math.Max(0, (int)Math.Round(_format.SampleRate * (virtualDelayMs / 1000.0)) * _format.Channels);

        lock (_sync)
        {
            _active = new ActiveSound(
                data,
                Math.Clamp(virtualVolume, 0.0f, 2.0f),
                Math.Clamp(monitorVolume, 0.0f, 2.0f),
                delaySamples);
        }
    }

    public void Stop()
    {
        lock (_sync)
        {
            _active = null;
        }
    }

    public void UpdateVolumes(float virtualVolume, float monitorVolume)
    {
        lock (_sync)
        {
            _active?.UpdateVolumes(
                Math.Clamp(virtualVolume, 0.0f, 2.0f),
                Math.Clamp(monitorVolume, 0.0f, 2.0f));
        }
    }

    public int Read(AudioRoute route, float[] buffer, int offset, int count)
    {
        lock (_sync)
        {
            if (_active is null)
            {
                Array.Clear(buffer, offset, count);
                return 0;
            }

            var written = _active.Read(route, buffer, offset, count);
            if (_active.IsFinished)
            {
                _active = null;
            }

            return written;
        }
    }

    private sealed class ActiveSound
    {
        private readonly float[] _samples;
        private float _virtualVolume;
        private float _monitorVolume;
        private int _virtualPosition;
        private int _monitorPosition;
        private int _remainingVirtualDelaySamples;
        private bool _virtualFinished;
        private bool _monitorFinished;

        public ActiveSound(float[] samples, float virtualVolume, float monitorVolume, int virtualDelaySamples)
        {
            _samples = samples;
            _virtualVolume = virtualVolume;
            _monitorVolume = monitorVolume;
            _remainingVirtualDelaySamples = virtualDelaySamples;
        }

        public bool IsFinished => _virtualFinished && _monitorFinished;

        public void UpdateVolumes(float virtualVolume, float monitorVolume)
        {
            _virtualVolume = virtualVolume;
            _monitorVolume = monitorVolume;
        }

        public int Read(AudioRoute route, float[] buffer, int offset, int count)
        {
            var written = 0;

            if (route == AudioRoute.VirtualMicrophone && _remainingVirtualDelaySamples > 0)
            {
                var silence = Math.Min(count, _remainingVirtualDelaySamples);
                Array.Clear(buffer, offset, silence);
                _remainingVirtualDelaySamples -= silence;
                offset += silence;
                count -= silence;
                written += silence;
            }

            if (count <= 0)
            {
                return written;
            }

            var position = route == AudioRoute.VirtualMicrophone ? _virtualPosition : _monitorPosition;
            var remaining = Math.Max(0, _samples.Length - position);
            var toCopy = Math.Min(count, remaining);
            var volume = route == AudioRoute.VirtualMicrophone ? _virtualVolume : _monitorVolume;

            for (var i = 0; i < toCopy; i++)
            {
                buffer[offset + i] = _samples[position + i] * volume;
            }

            if (toCopy < count)
            {
                Array.Clear(buffer, offset + toCopy, count - toCopy);
            }

            if (route == AudioRoute.VirtualMicrophone)
            {
                _virtualPosition += toCopy;
                _virtualFinished = _virtualPosition >= _samples.Length && _remainingVirtualDelaySamples == 0;
            }
            else
            {
                _monitorPosition += toCopy;
                _monitorFinished = _monitorPosition >= _samples.Length;
            }

            return written + toCopy;
        }
    }
}
