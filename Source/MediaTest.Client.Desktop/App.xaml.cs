using Avalonia;
using Avalonia.Markup.Xaml;

namespace MediaTest.Client.Desktop
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
