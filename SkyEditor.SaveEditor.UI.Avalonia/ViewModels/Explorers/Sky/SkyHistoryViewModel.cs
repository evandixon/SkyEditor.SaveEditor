using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels.Explorers.Sky
{
    public class SkyHistoryViewModel : ViewModelBase
    {
        public SkyHistoryViewModel(SkySave model)
        {
            this.Model = model;
            this.ExplorersPokemon = Lists.ExplorersPokemon.Select(kv => new ListItem { DisplayName = $"{kv.Key:D3} {kv.Value}", Value = kv.Key }).ToList();
        }

        protected SkySave Model { get; set; }

        public ListItem OriginalPlayerPokemonItem
        {
            get => ExplorersPokemon[Model.OriginalPlayerPokemon.ID];
            set
            {
                if (Model.OriginalPlayerPokemon.ID != value.Value)
                {
                    Model.OriginalPlayerPokemon.ID = value.Value;
                    this.RaisePropertyChanged(nameof(OriginalPlayerPokemonItem));
                }
            }
        }

        public bool OriginalPlayerIsFemale
        {
            get => Model.OriginalPlayerPokemon.IsFemale;
            set
            {
                if (Model.OriginalPlayerPokemon.IsFemale != value)
                {
                    Model.OriginalPlayerPokemon.IsFemale = value;
                    this.RaisePropertyChanged(nameof(OriginalPlayerIsFemale));
                }
            }
        }

        public int OriginalPartnerID
        {
            get => Model.OriginalPartnerPokemon.ID;
            set
            {
                if (Model.OriginalPartnerPokemon.ID != value)
                {
                    Model.OriginalPartnerPokemon.ID = value;
                    this.RaisePropertyChanged(nameof(OriginalPartnerID));
                }
            }
        }

        public ListItem OriginalPartnerPokemonItem
        {
            get => ExplorersPokemon[Model.OriginalPartnerPokemon.ID];
            set
            {
                if (Model.OriginalPartnerPokemon.ID != value.Value)
                {
                    Model.OriginalPartnerPokemon.ID = value.Value;
                    this.RaisePropertyChanged(nameof(OriginalPartnerPokemonItem));
                }
            }
        }

        public bool OriginalPartnerIsFemale
        {
            get => Model.OriginalPartnerPokemon.IsFemale;
            set
            {
                if (Model.OriginalPartnerPokemon.IsFemale != value)
                {
                    Model.OriginalPartnerPokemon.IsFemale = value;
                    this.RaisePropertyChanged(nameof(OriginalPartnerIsFemale));
                }
            }
        }

        public string OriginalPlayerName
        {
            get => Model.OriginalPlayerName;
            set
            {
                if (Model.OriginalPlayerName != value)
                {
                    Model.OriginalPlayerName = value;
                    this.RaisePropertyChanged(nameof(OriginalPlayerName));
                }
            }
        }

        public string OriginalPartnerName
        {
            get => Model.OriginalPartnerName;
            set
            {
                if (Model.OriginalPartnerName != value)
                {
                    Model.OriginalPartnerName = value;
                    this.RaisePropertyChanged(nameof(OriginalPartnerName));
                }
            }
        }

        public List<ListItem> ExplorersPokemon { get; }       
    }
}
