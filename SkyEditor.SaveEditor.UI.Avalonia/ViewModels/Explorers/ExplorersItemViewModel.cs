using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels.Explorers
{
    public class ExplorersItemViewModel : ViewModelBase
    {
        public ExplorersItemViewModel() : this(new ExplorersItem())
        {
        }

        public ExplorersItemViewModel(ExplorersItem model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            if (model is TDHeldItem)
            {
                if (IsBox)
                {
                    ContainedItemChoices = Lists.TDItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                }
                else if (IsUsedTM)
                {
                    ContainedItemChoices = Lists.TDItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                }
                else
                {
                    ContainedItemChoices = new List<ListItem>();
                }
            }
            else
            {
                if (IsBox)
                {
                    ContainedItemChoices = Lists.SkyItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                }
                else if (IsUsedTM)
                {
                    ContainedItemChoices = Lists.SkyItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                }
                else
                {
                    ContainedItemChoices = new List<ListItem>();
                }
            }
        }

        public ExplorersItem Model { get; }

        public int ID
        {
            get => Model.ID;
            set
            {
                if (Model.ID != value)
                {
                    Model.ID = value;
                    this.RaisePropertyChanged(nameof(ID));
                    this.RaisePropertyChanged(nameof(Name));
                    this.RaisePropertyChanged(nameof(Quantity));
                    this.RaisePropertyChanged(nameof(IsBox));
                    this.RaisePropertyChanged(nameof(IsUsedTM));
                    this.RaisePropertyChanged(nameof(IsStackableItem));
                    this.RaisePropertyChanged(nameof(CanContainItem));
                    this.RaisePropertyChanged(nameof(ContainedItemChoices));
                }
            }
        }

        public string Name => Model.Name;

        public int ContainedItemID
        {
            get => Model.ContainedItemID;
            set
            {
                if (Model.ContainedItemID != value)
                {
                    Model.ContainedItemID = value;
                    this.RaisePropertyChanged(nameof(ContainedItemID));
                    this.RaisePropertyChanged(nameof(ContainedItemName));
                }
            }
        }

        public string ContainedItemName => Model.ContainedItemName;

        public int Quantity
        {
            get => Model.Quantity;
            set
            {
                if (Model.Quantity != value)
                {
                    Model.Quantity = value;
                    this.RaisePropertyChanged(nameof(Quantity));
                }
            }
        }

        public bool IsBox => Model.IsBox;

        public bool IsUsedTM => Model.IsUsedTM;

        public bool IsStackableItem => Model.IsStackableItem;

        public bool CanContainItem => IsBox || IsUsedTM;

        public List<ListItem> ContainedItemChoices { get; }

        public ExplorersItemViewModel Clone()
        {
            return new ExplorersItemViewModel(Model.Clone());
        }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
