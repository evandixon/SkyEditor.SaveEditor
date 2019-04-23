using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels
{
    public class SkySaveViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public SkySaveViewModel(SkySave model)
        {
            this.Model = model;
        }

        protected SkySave Model { get; set; }

        public string FileName
        {
            get => Path.GetFileName(Model.Filename);
        }

        public string TeamName
        {
            get => Model.TeamName;
            set {
                if (Model.TeamName != value)
                {
                    Model.TeamName = value;
                    this.RaisePropertyChanged(nameof(TeamName));
                }
            }
        }
    }
}
