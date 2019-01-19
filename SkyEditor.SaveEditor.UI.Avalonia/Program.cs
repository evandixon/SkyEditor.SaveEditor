using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using SkyEditor.SaveEditor.UI.Avalonia.ViewModels;
using SkyEditor.SaveEditor.UI.Avalonia.Views;

namespace SkyEditor.SaveEditor.UI.Avalonia
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
