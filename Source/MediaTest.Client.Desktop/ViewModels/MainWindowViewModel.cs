using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace MediaTest.Client.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // TODO: Find a way to remove this default constructor
        public MainWindowViewModel() : this(new Window())
        {
        }

        public MainWindowViewModel(Window window)
        {
            this.window = window;
            this.LoadImageFromFileCommand = ReactiveCommand.CreateFromTask(LoadImageFromFileCommandExecuteAsync);
            this.SaveImageToFileCopyCommand = ReactiveCommand.CreateFromTask(SaveImageToFileCopyCommandExecuteAsync, SaveImageToFileCopyCommandCanExecute);
        }

        public string Greeting => "Welcome to Avalonia!";

        private SKImage? previewImage = null;
        public Bitmap? PreviewImage
        {
            get
            {
                if (this.previewImage == null)
                {
                    return null;
                }

                using var data = this.previewImage.Encode();

                return new Bitmap(data.AsStream());
            }
        }

        private readonly Window window;

        public ReactiveCommand<Unit, Unit> LoadImageFromFileCommand { get; }

        public ReactiveCommand<Unit, Unit> SaveImageToFileCopyCommand { get; }

        private async Task LoadImageFromFileCommandExecuteAsync()
        {
            var dialog = new OpenFileDialog
            {
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Extensions = new List<string> { "bmp", "jpg", "png" },
                        Name = "Images"
                    },
                    new FileDialogFilter
                    {
                        Extensions = new List<string> { "*" },
                        Name = "All Files"
                    }
                }
            };

            var result = await dialog.ShowAsync(this.window);

            if (result.Length > 0)
            {
                this.previewImage = SKImage.FromBitmap(SKBitmap.Decode(result[0]));
                this.RaisePropertyChanged(nameof(this.PreviewImage));
            }
        }

        private async Task SaveImageToFileCopyCommandExecuteAsync()
        {
            if (this.previewImage == null)
                throw new ArgumentNullException(nameof(this.previewImage));

            var dialog = new SaveFileDialog
            {
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Extensions = new List<string> { "jpg" },
                        Name = "JPEG"
                    },
                    new FileDialogFilter
                    {
                        Extensions = new List<string> { "png" },
                        Name = "PNG"
                    }
                }
            };

            var result = await dialog.ShowAsync(this.window);

            if (result?.Length > 0)
            {
                var encodedData = null as SKData;
                string extension = new FileInfo(result).Extension.ToLower();

                if (extension == ".jpg")
                    encodedData = this.previewImage.Encode(SKEncodedImageFormat.Jpeg, 90);
                else if (extension == ".png")
                    encodedData = this.previewImage.Encode(SKEncodedImageFormat.Png, 100);
                else
                    throw new Exception($"Unsupported file type \"{extension}\"");

                using var filestream = File.OpenWrite(result);
                await encodedData.AsStream().CopyToAsync(filestream);
            }
        }

        private IObservable<bool> SaveImageToFileCopyCommandCanExecute
        {
            get
            {
                return this.WhenAnyValue(x => x.PreviewImage, (Bitmap? pi) => pi != null);
            }
        }
    }
}
