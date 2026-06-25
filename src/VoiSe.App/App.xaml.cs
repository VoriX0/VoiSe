using Microsoft.UI.Xaml;
using System;
using System.IO;

namespace VoiSe.App;

public partial class App : Application
{
    private Window? _window;

    public App()
    {
        try
        {
            InitializeComponent();
            UnhandledException += OnUnhandledException;
            StartupLog.Write("App initialized.");
        }
        catch (Exception ex)
        {
            StartupLog.Write("App constructor error: " + ex);
            throw;
        }
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            StartupLog.Write("OnLaunched started.");
            _window = new MainWindow();
            _window.Activate();
            StartupLog.Write("MainWindow activated.");
        }
        catch (Exception ex)
        {
            StartupLog.Write("OnLaunched error: " + ex);
            throw;
        }
    }

    private static void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        StartupLog.Write("UnhandledException: " + e.Exception);
    }
}

internal static class StartupLog
{
    private static readonly string LogPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "VoiSe",
        "gate3-startup.log");

    public static void Write(string message)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(LogPath)!);
            File.AppendAllText(LogPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}{Environment.NewLine}");
        }
        catch
        {
            // Best-effort diagnostic logging only.
        }
    }
}
