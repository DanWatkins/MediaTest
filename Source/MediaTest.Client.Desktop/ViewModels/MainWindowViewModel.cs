using SkiaSharp;
using System;
using System.IO;

namespace MediaTest.Client.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        private SKBitmap _PreviewImage;
        public SKBitmap PreviewImage
        {
            get
            {
                if (_PreviewImage == null)
                {
                    _PreviewImage = SKBitmap.Decode(@"C:\Users\dwatk\Desktop\test.bmp");

                    using (var resized = new SKBitmap(128, 128))
                    {
                        _PreviewImage.ScalePixels(resized, SKFilterQuality.High);
                        using (var image = SKImage.FromBitmap(resized))
                        using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 80))
                        using (var stream = File.OpenWrite($@"C:\Users\dwatk\Desktop\resized-{DateTime.UtcNow.Ticks}.jpg"))
                        {
                            data.SaveTo(stream);
                        }
                    }
                }

                return _PreviewImage;
            }
        }
    }
}
