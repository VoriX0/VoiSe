using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VoiSe.App;

public sealed class VoiSeScene
{
    public int SchemaVersion { get; set; } = 1;
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "Scene";
    public string Icon { get; set; } = "🎬";

    public string? VoicePresetName { get; set; }
    public Dictionary<string, double> VoiceSliders { get; set; } = new();
    public bool VoiceMonitorEnabled { get; set; }

    public string? SoundCategoryId { get; set; }
    public string? SoundCategoryName { get; set; }
    public string? BackgroundSoundId { get; set; }
    public string? BackgroundSoundName { get; set; }
    public List<string> OneShotSoundIds { get; set; } = new();

    public double VirtualMicMasterVolume { get; set; } = 1.0;
    public double SoundBoardVirtualMicVolume { get; set; } = 1.0;
    public double SoundBoardHeadphonesVolume { get; set; } = 1.0;
    public double SoundBoardVirtualMicDelayMs { get; set; } = 85.0;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public string? FilePath { get; set; }
}
