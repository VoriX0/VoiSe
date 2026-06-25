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
                return _active is not null && !_active.IsPaused;
            }
        }
    }

    public bool IsActive
    {
        get
        {
            lock (_sync)
            {
                return _active is not null;
            }
        }
    }

    public bool IsPaused
    {
        get
        {
            lock (_sync)
            {
                return _active?.IsPaused ?? false;
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
                _format.SampleRate,
                _format.Channels,
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

    public bool TogglePause()
    {
        lock (_sync)
        {
            if (_active is null)
            {
                return false;
            }

            _active.TogglePause();
            return _active.IsPaused;
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

    public SoundboardStatus GetStatus()
    {
        lock (_sync)
        {
            return _active?.GetStatus() ?? SoundboardStatus.Empty;
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
        private readonly int _sampleRate;
        private readonly int _channels;
        private float _virtualVolume;
        private float _monitorVolume;
        private int _virtualPosition;
        private int _monitorPosition;
        private int _remainingVirtualDelaySamples;
        private bool _virtualFinished;
        private bool _monitorFinished;

        public ActiveSound(float[] samples, int sampleRate, int channels, float virtualVolume, float monitorVolume, int virtualDelaySamples)
        {
            _samples = samples;
            _sampleRate = sampleRate;
            _channels = channels;
            _virtualVolume = virtualVolume;
            _monitorVolume = monitorVolume;
            _remainingVirtualDelaySamples = virtualDelaySamples;
        }

        public bool IsFinished => _virtualFinished && _monitorFinished;
        public bool IsPaused { get; private set; }

        public void TogglePause()
        {
            IsPaused = !IsPaused;
        }

        public void UpdateVolumes(float virtualVolume, float monitorVolume)
        {
            _virtualVolume = virtualVolume;
            _monitorVolume = monitorVolume;
        }

        public SoundboardStatus GetStatus()
        {
            var durationSeconds = _samples.Length / (double)_channels / _sampleRate;
            var currentSeconds = Math.Min(durationSeconds, _monitorPosition / (double)_channels / _sampleRate);
            return new SoundboardStatus(true, IsPaused, currentSeconds, durationSeconds);
        }

        public int Read(AudioRoute route, float[] buffer, int offset, int count)
        {
            if (IsPaused)
            {
                Array.Clear(buffer, offset, count);
                return count;
            }

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

public readonly record struct SoundboardStatus(bool IsActive, bool IsPaused, double CurrentSeconds, double DurationSeconds)
{
    public static SoundboardStatus Empty { get; } = new(false, false, 0, 0);
}
