using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkyEditor.SaveEditor.UI.Avalonia.Views.Explorers
{
    public class ExplorersItemEditView : UserControl
    {
        public ExplorersItemEditView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
