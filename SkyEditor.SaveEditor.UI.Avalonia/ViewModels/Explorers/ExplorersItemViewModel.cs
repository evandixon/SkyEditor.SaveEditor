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
                    this.RaisePropertyChanged(nameof(IDListItem));
                    this.RaisePropertyChanged(nameof(Name));
                    this.RaisePropertyChanged(nameof(Quantity));
                    this.RaisePropertyChanged(nameof(IsBox));
                    this.RaisePropertyChanged(nameof(IsUsedTM));
                    this.RaisePropertyChanged(nameof(IsStackableItem));
                    this.RaisePropertyChanged(nameof(CanContainItem));
                    this.RaisePropertyChanged(nameof(ItemChoices));
                    this.RaisePropertyChanged(nameof(ContainedItemChoices));
                }
            }
        }        

        public ListItem IDListItem
        {
            get => ItemChoices.Count > ID ? ItemChoices[ID] : null;
            set
            {
                if (value != null && Model.ID != value.Value)
                {
                    Model.ID = value.Value;
                    this.RaisePropertyChanged(nameof(ID));
                    this.RaisePropertyChanged(nameof(IDListItem));
                    this.RaisePropertyChanged(nameof(Name));
                    this.RaisePropertyChanged(nameof(Quantity));
                    this.RaisePropertyChanged(nameof(IsBox));
                    this.RaisePropertyChanged(nameof(IsUsedTM));
                    this.RaisePropertyChanged(nameof(IsStackableItem));
                    this.RaisePropertyChanged(nameof(CanContainItem));
                    this.RaisePropertyChanged(nameof(ItemChoices));
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
                    this.RaisePropertyChanged(nameof(ContainedItemIDListItem));
                    this.RaisePropertyChanged(nameof(ContainedItemName));
                }
            }
        }

        public ListItem ContainedItemIDListItem
        {
            get => ContainedItemChoices.Count > ContainedItemID ? ContainedItemChoices[ContainedItemID] : null;
            set
            {
                if (value != null && Model.ContainedItemID != value.Value)
                {
                    Model.ContainedItemID = value.Value;
                    this.RaisePropertyChanged(nameof(ContainedItemID));
                    this.RaisePropertyChanged(nameof(ContainedItemIDListItem));
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

        public List<ListItem> ItemChoices
        {
            get
            {
                if (Model is TDHeldItem)
                {
                    return Lists.TDItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();                    
                }
                else
                {
                    return Lists.SkyItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();                 
                }
            }
        }

        public List<ListItem> ContainedItemChoices
        {
            get
            {
                if (Model is TDHeldItem)
                {
                    if (IsBox)
                    {
                        return Lists.TDItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                    }
                    else if (IsUsedTM)
                    {
                        return Lists.TDItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                    }
                    else
                    {
                        return new List<ListItem>();
                    }
                }
                else
                {
                    if (IsBox)
                    {
                        return Lists.SkyItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                    }
                    else if (IsUsedTM)
                    {
                        return Lists.SkyItems.Select(kv => new ListItem { DisplayName = kv.Value, Value = kv.Key }).ToList();
                    }
                    else
                    {
                        return new List<ListItem>();
                    }
                }
            }
        }

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
