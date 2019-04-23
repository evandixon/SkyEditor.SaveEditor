﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SkyEditor.SaveEditor.UI.Avalonia.Views
{
    public class SkySaveView : UserControl
    {
        public SkySaveView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
