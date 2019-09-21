using Avalonia.Media.Imaging;
using SkiaSharp;

namespace MediaTest.Client.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        private SKImage GenerateSnapshotImage()
        {
            var info = new SKImageInfo(512, 512);
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Green);
            canvas.Flush();

            return surface.Snapshot();
        }

        public Bitmap PreviewImage
        {
            get
            {
                using var image = this.GenerateSnapshotImage();
                using var data = image.Encode();

                return new Bitmap(data.AsStream());
            }
        }
    }
}
