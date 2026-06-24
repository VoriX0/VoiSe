using NAudio.Wave;

namespace VoiSe.Audio;

internal static class AudioFormatConverter
{
    public static float[] CaptureBufferToTargetStereoFloat(byte[] buffer, int bytesRecorded, WaveFormat sourceFormat, WaveFormat targetFormat)
    {
        var sourceSamples = PcmFloatConverter.ToFloatArray(buffer, bytesRecorded, sourceFormat);
        return InterleavedToTargetDuplicatedMono(sourceSamples, sourceFormat.SampleRate, sourceFormat.Channels, targetFormat.SampleRate, targetFormat.Channels);
    }

    public static float[] InterleavedToTargetDuplicatedMono(float[] sourceSamples, int sourceSampleRate, int sourceChannels, int targetSampleRate, int targetChannels)
    {
        if (sourceChannels <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sourceChannels));
        }

        if (targetChannels <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(targetChannels));
        }

        var sourceFrameCount = sourceSamples.Length / sourceChannels;
        if (sourceFrameCount == 0)
        {
            return Array.Empty<float>();
        }

        var targetFrameCount = Math.Max(1, (int)Math.Round(sourceFrameCount * (targetSampleRate / (double)sourceSampleRate)));
        var output = new float[targetFrameCount * targetChannels];

        for (var targetFrame = 0; targetFrame < targetFrameCount; targetFrame++)
        {
            var sourcePosition = targetFrame * (sourceSampleRate / (double)targetSampleRate);
            var sourceIndex = (int)Math.Floor(sourcePosition);
            var nextIndex = Math.Min(sourceIndex + 1, sourceFrameCount - 1);
            var frac = (float)(sourcePosition - sourceIndex);

            sourceIndex = Math.Min(sourceIndex, sourceFrameCount - 1);
            var monoA = AverageFrame(sourceSamples, sourceIndex, sourceChannels);
            var monoB = AverageFrame(sourceSamples, nextIndex, sourceChannels);
            var mono = monoA + (monoB - monoA) * frac;

            var outputOffset = targetFrame * targetChannels;
            for (var ch = 0; ch < targetChannels; ch++)
            {
                output[outputOffset + ch] = mono;
            }
        }

        return output;
    }

    private static float AverageFrame(float[] samples, int frameIndex, int channels)
    {
        var offset = frameIndex * channels;
        var sum = 0.0f;
        for (var ch = 0; ch < channels; ch++)
        {
            sum += samples[offset + ch];
        }

        return sum / channels;
    }
}
