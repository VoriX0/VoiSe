using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VoiSe.Audio;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace VoiSe.App;

public sealed partial class MainWindow : Window
{
    private readonly AudioDeviceCatalog _catalog = new();
    private Gate2UnifiedAudioEngine? _engine;
    private string? _soundFilePath;

    public MainWindow()
    {
        StartupLog.Write("MainWindow constructor started.");
        InitializeComponent();
        Closed += OnClosed;
        UpdateDelayLabel();
        UpdateSoundVolumeLabels();
        UpdateVoiceSettingLabels();
        AppendLog("Gate 3.2 UI started.");
        StartupLog.Write("MainWindow initialized; loading devices next.");

        // Load devices after the visual tree exists, so startup problems are visible in the UI/log.
        DispatcherQueue.TryEnqueue(() =>
        {
            try
            {
                RefreshDevices();
            }
            catch (Exception ex)
            {
                StartupLog.Write("RefreshDevices startup error: " + ex);
                AppendLog($"Device refresh error: {ex.Message}");
            }
        });
    }

    private void OnClosed(object sender, WindowEventArgs args)
    {
        StopEngine();
        _catalog.Dispose();
    }

    private void OnRefreshDevicesClick(object sender, RoutedEventArgs e)
    {
        try
        {
            RefreshDevices();
        }
        catch (Exception ex)
        {
            StartupLog.Write("RefreshDevices button error: " + ex);
            AppendLog($"Device refresh error: {ex.Message}");
        }
    }

    private void RefreshDevices()
    {
        var captureDevices = _catalog.ListCaptureDevices();
        var renderDevices = _catalog.ListRenderDevices();

        InputDeviceComboBox.ItemsSource = captureDevices;
        VirtualOutputComboBox.ItemsSource = renderDevices;
        MonitorOutputComboBox.ItemsSource = renderDevices;

        InputDeviceComboBox.SelectedItem = PickByName(captureDevices, "Fifine") ?? captureDevices.FirstOrDefault();
        VirtualOutputComboBox.SelectedItem = PickByName(renderDevices, "CABLE Input") ?? renderDevices.FirstOrDefault();
        MonitorOutputComboBox.SelectedItem = PickByName(renderDevices, "Realtek") ?? renderDevices.FirstOrDefault();

        AppendLog($"Devices refreshed: {captureDevices.Count} capture, {renderDevices.Count} render.");
    }

    private static AudioDeviceInfo? PickByName(IReadOnlyList<AudioDeviceInfo> devices, string text)
    {
        return devices.FirstOrDefault(d => d.FriendlyName.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

    private async void OnBrowseSoundClick(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Add(".wav");
        picker.FileTypeFilter.Add(".mp3");
        picker.FileTypeFilter.Add(".ogg");
        InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(this));

        var file = await picker.PickSingleFileAsync();
        if (file is null)
        {
            return;
        }

        _soundFilePath = file.Path;
        SoundFileTextBox.Text = _soundFilePath;
        AppendLog($"Sound selected: {_soundFilePath}");
    }

    private void OnStartEngineClick(object sender, RoutedEventArgs e)
    {
        if (_engine is not null)
        {
            AppendLog("Engine is already running.");
            return;
        }

        var inputInfo = InputDeviceComboBox.SelectedItem as AudioDeviceInfo;
        var virtualInfo = VirtualOutputComboBox.SelectedItem as AudioDeviceInfo;
        var monitorInfo = MonitorOutputComboBox.SelectedItem as AudioDeviceInfo;

        if (inputInfo is null || virtualInfo is null)
        {
            AppendLog("Select input microphone and virtual output first.");
            return;
        }

        var input = _catalog.FindCaptureDevice(inputInfo.Id);
        var virtualOutput = _catalog.FindRenderDevice(virtualInfo.Id);
        var monitor = monitorInfo is null ? null : _catalog.FindRenderDevice(monitorInfo.Id);

        if (input is null || virtualOutput is null)
        {
            AppendLog("Selected devices are not available anymore. Refresh devices and try again.");
            return;
        }

        var settings = new EffectSettings
        {
            VoiceGainDb = (float)VoiceGainSlider.Value,
            GateThresholdDb = (float)GateThresholdSlider.Value,
            CompressorThresholdDb = (float)CompressorThresholdSlider.Value,
            GateEnabled = true,
            CompressorEnabled = true,
            LimiterEnabled = true,
            LimiterCeilingDb = -1.0f
        };

        try
        {
            _engine = new Gate2UnifiedAudioEngine(input, virtualOutput, monitor, settings);
            _engine.Start();
            EngineStatusTextBlock.Text = "Running";
            AppendLog($"Engine started. Input: {input.FriendlyName}");
            AppendLog($"Virtual output: {virtualOutput.FriendlyName}");
            AppendLog($"Monitor: {(monitor is null ? "disabled" : monitor.FriendlyName)}");
        }
        catch (Exception ex)
        {
            _engine?.Dispose();
            _engine = null;
            EngineStatusTextBlock.Text = "Error";
            AppendLog($"Engine start error: {ex.Message}");
        }
    }

    private void OnStopEngineClick(object sender, RoutedEventArgs e)
    {
        StopEngine();
    }

    private void StopEngine()
    {
        if (_engine is null)
        {
            return;
        }

        try
        {
            _engine.Dispose();
            AppendLog("Engine stopped.");
        }
        catch (Exception ex)
        {
            AppendLog($"Engine stop error: {ex.Message}");
        }
        finally
        {
            _engine = null;
            EngineStatusTextBlock.Text = "Stopped";
        }
    }

    private void OnPlaySoundClick(object sender, RoutedEventArgs e)
    {
        if (_engine is null)
        {
            AppendLog("Start engine before playing sound.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_soundFilePath) || !File.Exists(_soundFilePath))
        {
            AppendLog("Choose an existing sound file first.");
            return;
        }

        try
        {
            var delayMs = (int)Math.Round(SoundVirtualDelaySlider.Value);
            var virtualVolume = (float)SoundVirtualVolumeSlider.Value;
            var monitorVolume = (float)SoundMonitorVolumeSlider.Value;
            _engine.PlaySound(_soundFilePath, virtualVolume, monitorVolume, delayMs);
            AppendLog($"Sound started. Virtual delay: {delayMs} ms.");
        }
        catch (Exception ex)
        {
            AppendLog($"Sound playback error: {ex.Message}");
        }
    }

    private void OnStopSoundClick(object sender, RoutedEventArgs e)
    {
        if (_engine is null)
        {
            return;
        }

        _engine.StopSound();
        AppendLog("Sound stopped.");
    }

    private void OnDelayChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateDelayLabel();
    }

    private void OnSoundVolumeChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateSoundVolumeLabels();
    }

    private void OnVoiceSettingsChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateVoiceSettingLabels();
    }

    private void UpdateDelayLabel()
    {
        if (DelayLabel is null || SoundVirtualDelaySlider is null)
        {
            return;
        }

        DelayLabel.Text = $"Virtual Mic Delay: {(int)Math.Round(SoundVirtualDelaySlider.Value)} ms";
    }

    private void UpdateSoundVolumeLabels()
    {
        if (SoundVirtualVolumeLabel is null || SoundMonitorVolumeLabel is null)
        {
            return;
        }

        SoundVirtualVolumeLabel.Text = $"Sound → Virtual Mic: {(int)Math.Round(SoundVirtualVolumeSlider.Value * 100)}%";
        SoundMonitorVolumeLabel.Text = $"Sound → Monitor: {(int)Math.Round(SoundMonitorVolumeSlider.Value * 100)}%";
    }

    private void UpdateVoiceSettingLabels()
    {
        if (VoiceGainLabel is null || GateThresholdLabel is null || CompressorThresholdLabel is null)
        {
            return;
        }

        VoiceGainLabel.Text = $"Voice Gain: {(int)Math.Round(VoiceGainSlider.Value)} dB";
        GateThresholdLabel.Text = $"Gate Threshold: {(int)Math.Round(GateThresholdSlider.Value)} dB";
        CompressorThresholdLabel.Text = $"Compressor Threshold: {(int)Math.Round(CompressorThresholdSlider.Value)} dB";
    }

    private void AppendLog(string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
        LogTextBox.Text = string.IsNullOrEmpty(LogTextBox.Text)
            ? line
            : LogTextBox.Text + Environment.NewLine + line;
    }
}
