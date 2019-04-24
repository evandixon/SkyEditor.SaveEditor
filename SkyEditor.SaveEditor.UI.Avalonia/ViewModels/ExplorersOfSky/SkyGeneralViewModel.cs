using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels.ExplorersOfSky
{
    public class SkyGeneralViewModel : ViewModelBase
    {
        public SkyGeneralViewModel(SkySave model)
        {
            this.Model = model;
        }

        protected SkySave Model { get; set; }

        public string TeamName
        {
            get => Model.TeamName;
            set
            {
                if (Model.TeamName != value)
                {
                    Model.TeamName = value;
                    this.RaisePropertyChanged(nameof(TeamName));
                }
            }
        }

        public int HeldMoney
        {
            get => Model.HeldMoney;
            set
            {
                if (Model.HeldMoney != value)
                {
                    Model.HeldMoney = value;
                    this.RaisePropertyChanged(nameof(HeldMoney));
                }
            }
        }

        public int SpEpisodeHeldMoney
        {
            get => Model.SpEpisodeHeldMoney;
            set
            {
                if (Model.SpEpisodeHeldMoney != value)
                {
                    Model.SpEpisodeHeldMoney = value;
                    this.RaisePropertyChanged(nameof(SpEpisodeHeldMoney));
                }
            }
        }

        public int StoredMoney
        {
            get => Model.StoredMoney;
            set
            {
                if (Model.StoredMoney != value)
                {
                    Model.StoredMoney = value;
                    this.RaisePropertyChanged(nameof(StoredMoney));
                }
            }
        }

        public int NumberOfAdventures
        {
            get => Model.NumberOfAdventures;
            set
            {
                if (Model.NumberOfAdventures != value)
                {
                    Model.NumberOfAdventures = value;
                    this.RaisePropertyChanged(nameof(NumberOfAdventures));
                }
            }
        }

        public int ExplorerRank
        {
            get => Model.ExplorerRankPoints;
            set
            {
                if (Model.ExplorerRankPoints != value)
                {
                    Model.ExplorerRankPoints = value;
                    this.RaisePropertyChanged(nameof(ExplorerRank));
                }
            }
        }
    }
}
