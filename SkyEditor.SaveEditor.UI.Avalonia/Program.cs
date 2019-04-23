using System;
using System.Threading;
using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.Threading;
using ReactiveUI;
using SkyEditor.SaveEditor.UI.Avalonia.ViewModels;
using SkyEditor.SaveEditor.UI.Avalonia.Views;

namespace SkyEditor.SaveEditor.UI.Avalonia
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) {
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
            BuildAvaloniaApp()
                .Start<MainWindow>(() => new MainWindowViewModel());
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();
    }
}
