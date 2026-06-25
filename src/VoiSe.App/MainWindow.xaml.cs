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
    private readonly DispatcherTimer _routeRestartTimer;
    private Gate2UnifiedAudioEngine? _engine;
    private string? _soundFilePath;
    private bool _refreshingDevices;
    private bool _manualStopRequested;
    private string _pendingRestartReason = "settings changed";

    public MainWindow()
    {
        StartupLog.Write("MainWindow constructor started.");
        InitializeComponent();
        Closed += OnClosed;

        _routeRestartTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(450)
        };
        _routeRestartTimer.Tick += OnRouteRestartTimerTick;

        UpdateAllLabels();
        AppendLog("Gate 3.5 UI started.");
        StartupLog.Write("MainWindow initialized; loading devices next.");

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
        _manualStopRequested = true;
        StopEngine(log: false);
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
        _refreshingDevices = true;
        try
        {
            var captureDevices = _catalog.ListCaptureDevices();
            var renderDevices = _catalog.ListRenderDevices();

            var oldInputId = (InputDeviceComboBox.SelectedItem as AudioDeviceInfo)?.Id;
            var oldVirtualId = (VirtualOutputComboBox.SelectedItem as AudioDeviceInfo)?.Id;
            var oldMonitorId = (MonitorOutputComboBox.SelectedItem as AudioDeviceInfo)?.Id;

            InputDeviceComboBox.ItemsSource = captureDevices;
            VirtualOutputComboBox.ItemsSource = renderDevices;
            MonitorOutputComboBox.ItemsSource = renderDevices;

            InputDeviceComboBox.SelectedItem = PickById(captureDevices, oldInputId) ?? PickByName(captureDevices, "Fifine") ?? captureDevices.FirstOrDefault();
            VirtualOutputComboBox.SelectedItem = PickById(renderDevices, oldVirtualId) ?? PickByName(renderDevices, "CABLE Input") ?? renderDevices.FirstOrDefault();
            MonitorOutputComboBox.SelectedItem = PickById(renderDevices, oldMonitorId) ?? PickByName(renderDevices, "Realtek") ?? renderDevices.FirstOrDefault();

            AppendLog($"Devices refreshed: {captureDevices.Count} capture, {renderDevices.Count} render.");
        }
        finally
        {
            _refreshingDevices = false;
        }
    }

    private static AudioDeviceInfo? PickByName(IReadOnlyList<AudioDeviceInfo> devices, string text)
    {
        return devices.FirstOrDefault(d => d.FriendlyName.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

    private static AudioDeviceInfo? PickById(IReadOnlyList<AudioDeviceInfo> devices, string? id)
    {
        return id is null ? null : devices.FirstOrDefault(d => d.Id == id);
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
        _manualStopRequested = false;
        StartEngine(logAlreadyRunning: true);
    }

    private void OnStopEngineClick(object sender, RoutedEventArgs e)
    {
        _manualStopRequested = true;
        StopEngine(log: true);
    }

    private bool StartEngine(bool logAlreadyRunning)
    {
        if (_engine is not null)
        {
            if (logAlreadyRunning)
            {
                AppendLog("Engine is already running.");
            }
            return true;
        }

        var inputInfo = InputDeviceComboBox.SelectedItem as AudioDeviceInfo;
        var virtualInfo = VirtualOutputComboBox.SelectedItem as AudioDeviceInfo;
        var monitorInfo = MonitorOutputComboBox.SelectedItem as AudioDeviceInfo;

        if (inputInfo is null || virtualInfo is null)
        {
            AppendLog("Select input microphone and virtual output first.");
            return false;
        }

        var input = _catalog.FindCaptureDevice(inputInfo.Id);
        var virtualOutput = _catalog.FindRenderDevice(virtualInfo.Id);
        var monitor = monitorInfo is null ? null : _catalog.FindRenderDevice(monitorInfo.Id);

        if (input is null || virtualOutput is null)
        {
            AppendLog("Selected devices are not available anymore. Refresh devices and try again.");
            return false;
        }

        try
        {
            _engine = new Gate2UnifiedAudioEngine(input, virtualOutput, monitor, CreateEffectSettings());
            _engine.Start();
            EngineStatusTextBlock.Text = "Running";
            AppendLog($"Engine started. Input: {input.FriendlyName}");
            AppendLog($"Virtual output: {virtualOutput.FriendlyName}");
            AppendLog($"Monitor: {(monitor is null ? "disabled" : monitor.FriendlyName)}");
            return true;
        }
        catch (Exception ex)
        {
            _engine?.Dispose();
            _engine = null;
            EngineStatusTextBlock.Text = "Error";
            AppendLog($"Engine start error: {ex.Message}");
            return false;
        }
    }

    private void StopEngine(bool log)
    {
        if (_engine is null)
        {
            return;
        }

        try
        {
            _engine.Dispose();
            if (log)
            {
                AppendLog("Engine stopped.");
            }
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

    private void RestartEngine(string reason)
    {
        if (_engine is null || _manualStopRequested)
        {
            return;
        }

        AppendLog($"Engine auto-restart: {reason}.");
        StopEngine(log: false);
        StartEngine(logAlreadyRunning: false);
    }

    private void OnRouteRestartTimerTick(object? sender, object e)
    {
        _routeRestartTimer.Stop();
        RestartEngine(_pendingRestartReason);
    }

    private void ScheduleEngineRestart(string reason)
    {
        if (_engine is null || _refreshingDevices)
        {
            return;
        }

        _pendingRestartReason = reason;
        _routeRestartTimer.Stop();
        _routeRestartTimer.Start();
        EngineStatusTextBlock.Text = "Restart pending";
    }

    private void ApplyLiveSettings(string reason)
    {
        UpdateAllLabels();

        if (_engine is null)
        {
            return;
        }

        _engine.UpdateEffectSettings(CreateEffectSettings());
        _engine.UpdateSoundVolumes((float)SoundVirtualVolumeSlider.Value, (float)SoundMonitorVolumeSlider.Value);
        AppendLog($"Live settings applied: {reason}.");
    }

    private EffectSettings CreateEffectSettings()
    {
        return new EffectSettings
        {
            VoiceGainDb = (float)VoiceGainSlider.Value,
            GateThresholdDb = (float)GateThresholdSlider.Value,
            CompressorThresholdDb = (float)CompressorThresholdSlider.Value,
            GateEnabled = true,
            CompressorEnabled = true,
            LimiterEnabled = true,
            LimiterCeilingDb = -1.0f,
            VirtualOutputGain = (float)VirtualOutputVolumeSlider.Value,
            MonitorOutputGain = (float)MonitorOutputVolumeSlider.Value
        };
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

    private void OnRouteSettingChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_refreshingDevices)
        {
            return;
        }

        ScheduleEngineRestart("audio route changed");
    }

    private void OnDelayChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateDelayLabel();
    }

    private void OnSoundVolumeChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateSoundVolumeLabels();
        if (_engine is not null)
        {
            _engine.UpdateSoundVolumes((float)SoundVirtualVolumeSlider.Value, (float)SoundMonitorVolumeSlider.Value);
            AppendLog("SoundBoard route volumes applied live.");
        }
    }

    private void OnOutputVolumeChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateOutputVolumeLabels();
        ApplyLiveSettings("master output volume changed");
    }

    private void OnVoiceSettingsChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        UpdateVoiceSettingLabels();
        ApplyLiveSettings("voice setting changed");
    }

    private void UpdateAllLabels()
    {
        UpdateDelayLabel();
        UpdateSoundVolumeLabels();
        UpdateOutputVolumeLabels();
        UpdateVoiceSettingLabels();
    }

    private void UpdateDelayLabel()
    {
        if (DelayLabel is null || SoundVirtualDelaySlider is null)
        {
            return;
        }

        DelayLabel.Text = $"SoundBoard Virtual Mic Delay: {(int)Math.Round(SoundVirtualDelaySlider.Value)} ms";
    }

    private void UpdateSoundVolumeLabels()
    {
        if (SoundVirtualVolumeLabel is null || SoundMonitorVolumeLabel is null)
        {
            return;
        }

        SoundVirtualVolumeLabel.Text = $"SoundBoard → Virtual Mic: {(int)Math.Round(SoundVirtualVolumeSlider.Value * 100)}%";
        SoundMonitorVolumeLabel.Text = $"SoundBoard → Monitor: {(int)Math.Round(SoundMonitorVolumeSlider.Value * 100)}%";
    }

    private void UpdateOutputVolumeLabels()
    {
        if (VirtualOutputVolumeLabel is null || MonitorOutputVolumeLabel is null)
        {
            return;
        }

        VirtualOutputVolumeLabel.Text = $"Virtual Mic Master: {(int)Math.Round(VirtualOutputVolumeSlider.Value * 100)}%";
        MonitorOutputVolumeLabel.Text = $"Monitor Master: {(int)Math.Round(MonitorOutputVolumeSlider.Value * 100)}%";
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
