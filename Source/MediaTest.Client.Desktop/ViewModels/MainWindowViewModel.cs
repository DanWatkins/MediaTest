using Avalonia.Media.Imaging;
using SkiaSharp;
using System;
using System.IO;

namespace MediaTest.Client.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        private SKBitmap _Bitmap;
        public SKBitmap Bitmap
        {
            get
            {
                if (_Bitmap == null)
                {
                    this._Bitmap = SKBitmap.Decode(@"C:\Users\dwatk\Desktop\test.bmp");

                    using (var resized = new SKBitmap(128, 128))
                    {
                        _Bitmap.ScalePixels(resized, SKFilterQuality.High);
                        using (var image = SKImage.FromBitmap(resized))
                        using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 80))
                        using (var stream = File.OpenWrite($@"C:\Users\dwatk\Desktop\resized-{DateTime.UtcNow.Ticks}.jpg"))
                        {
                            data.SaveTo(stream);
                        }
                    }
                }

                return this._Bitmap;
            }
        }

        public Bitmap PreviewImage
        {
            get
            {
                using (var image = SKImage.FromBitmap(this.Bitmap))
                using (var data = image.Encode())
                {
                    return new Bitmap(data.AsStream());
                }
            }
        }
    }
}
