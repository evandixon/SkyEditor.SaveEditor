using Avalonia.Controls;
using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            OpenFileCommand = ReactiveCommand.Create(OpenFile);
        }
        public string Greeting
        {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        }
        private string _greeting = "Welcome to Avalonia!";

        public ReactiveCommand<Unit, Task> OpenFileCommand { get; }

        public SkySaveViewModel SaveFileViewModel
        {
            get => _saveFileViewModel;
            set => this.RaiseAndSetIfChanged(ref _saveFileViewModel, value);
        }
        private SkySaveViewModel _saveFileViewModel;

        private async Task OpenFile()
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter> { new FileDialogFilter() { Extensions = new List<string>() { "sav" }, Name = "Save Files" } }
            };
            var paths = await dialog.ShowAsync(App.Current.MainWindow);
            if (paths.Any())
            {
                var save = new SkySave(paths.First());
                SaveFileViewModel = new SkySaveViewModel(save);
                Greeting = SaveFileViewModel.TeamName;
            }
        }
    }
}
