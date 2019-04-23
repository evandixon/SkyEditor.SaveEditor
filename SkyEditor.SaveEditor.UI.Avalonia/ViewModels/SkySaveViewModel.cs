using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels
{
    public class SkySaveViewModel : INotifyPropertyChanged
    {
        public SkySaveViewModel(SkySave model)
        {
            this.Model = model;
        }

        protected SkySave Model { get; set; }

        public string TeamName
        {
            get => Model.TeamName;
            set {
                if (Model.TeamName != value)
                {
                    Model.TeamName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TeamName)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
