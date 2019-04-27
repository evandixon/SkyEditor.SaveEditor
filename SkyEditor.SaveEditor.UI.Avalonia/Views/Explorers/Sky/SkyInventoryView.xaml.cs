using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkyEditor.SaveEditor.UI.Avalonia.Views.Explorers.Sky
{
    public class SkyInventoryView : UserControl
    {
        public SkyInventoryView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
