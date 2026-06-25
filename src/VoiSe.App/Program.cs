using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System;
using System.Threading;
using WinRT;

namespace VoiSe.App;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            StartupLog.Write("Program.Main started.");
            ComWrappersSupport.InitializeComWrappers();
            StartupLog.Write("WinRT COM wrappers initialized.");

            Application.Start(p =>
            {
                try
                {
                    StartupLog.Write("Application.Start callback entered.");
                    var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                    SynchronizationContext.SetSynchronizationContext(context);
                    var app = new App();
                    StartupLog.Write("App instance created from Program.Main.");
                }
                catch (Exception ex)
                {
                    StartupLog.Write("Application.Start callback error: " + ex);
                    throw;
                }
            });

            StartupLog.Write("Program.Main finished normally.");
        }
        catch (Exception ex)
        {
            StartupLog.Write("Program.Main fatal error: " + ex);
            Console.Error.WriteLine(ex);
            Environment.ExitCode = 1;
        }
    }
}
