using Avalonia;
using Avalonia.Markup.Xaml;

namespace MediaTest.App.Desktop.Windows
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
