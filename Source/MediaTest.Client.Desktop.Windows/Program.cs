using Avalonia;
using Avalonia.Logging.Serilog;
using MediaTest.Client.Desktop.ViewModels;
using MediaTest.Client.Desktop.Views;

namespace MediaTest.Client.Desktop.Windows
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args) => BuildAvaloniaApp().Start(AppMain, args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseWin32()
                .UseSkia()
                .LogToDebug()
                .UseReactiveUI();

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] args)
        {
            var window = new MainWindow();
            window.DataContext = new MainWindowViewModel(window);

            app.Run(window);
        }
    }
}
