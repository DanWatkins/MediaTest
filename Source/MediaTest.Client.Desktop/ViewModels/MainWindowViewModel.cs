using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

namespace MediaTest.Client.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // TODO: Find a way to remove this default constructor
        public MainWindowViewModel()
        {
            this.window = new Window();
            this.LoadImageFromFileCommand = ReactiveCommand.CreateFromTask(LoadImageFromFileCommandExecuteAsync);
        }

        public MainWindowViewModel(Window window)
        {
            this.window = window;
            this.LoadImageFromFileCommand = ReactiveCommand.CreateFromTask(LoadImageFromFileCommandExecuteAsync);
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
                this.LoadImageFromFile(result[0]);
            }
        }

        private void LoadImageFromFile(string filepath)
        {
            this.previewImage = SKImage.FromBitmap(SKBitmap.Decode(filepath));
            this.RaisePropertyChanged(nameof(this.PreviewImage));
        }
    }
}
