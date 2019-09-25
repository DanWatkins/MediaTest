using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using MediaTest.Client.Desktop.ViewModels;

namespace MediaTest.Client.Desktop
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName?.Replace("ViewModel", "View");

            if (name == null)
                throw new ArgumentException("Data does not have a type name.", nameof(data));

            var type = Type.GetType(name);

            if (type != null)
            {
                var control = Activator.CreateInstance(type);

                if (control == null)
                    throw new ArgumentException("Could not create a Control for the supplied data.", nameof(data));

                return (IControl)control;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}