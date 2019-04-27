using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ReactiveUI;
using System.Reactive;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels.Explorers.Sky
{
    public class SkyInventoryViewModel : ViewModelBase
    {
        public SkyInventoryViewModel(SkySave model)
        {
            this.Model = model;
            this.AddStoredItemCommand = ReactiveCommand.Create(AddStoredItem, this.WhenAnyValue(v => v.CanAddStoredItem));
            this.AddHeldItemCommand = ReactiveCommand.Create(AddHeldItem, this.WhenAnyValue(v => v.CanAddHeldItem));
            this.AddSpEpisodeHeldItemCommand = ReactiveCommand.Create(AddSpEpisodeHeldItem, this.WhenAnyValue(v => v.CanAddSpEpisodeHeldItem));
            this.AddFriendRescueHeldItemCommand = ReactiveCommand.Create(AddFriendRescueHeldItem, this.WhenAnyValue(v => v.CanAddFriendRescueHeldItem));
        }

        private SkySave Model { get; }

        #region Stored Items
        public ObservableCollection<ExplorersItemViewModel> StoredItems { get; }

        public ExplorersItemViewModel SelectedStoredItem
        {
            get => _selectedStoredItem;
            set => this.RaiseAndSetIfChanged(ref _selectedStoredItem, value);
        }
        private ExplorersItemViewModel _selectedStoredItem;

        public ExplorersItemViewModel NewStoredItem
        {
            get => _newStoredItem;
            set => this.RaiseAndSetIfChanged(ref _newStoredItem, value);
        }
        private ExplorersItemViewModel _newStoredItem;

        public ReactiveCommand<Unit, Unit> AddStoredItemCommand { get; }

        public bool CanAddStoredItem => StoredItems.Count < 1000;
        
        private void AddStoredItem()
        {
            StoredItems.Add(NewStoredItem.Clone());
            this.RaisePropertyChanged(nameof(CanAddStoredItem));
        }
        #endregion

        #region Held Items
        public ObservableCollection<ExplorersItemViewModel> HeldItems { get; }

        public ExplorersItemViewModel SelectedHeldtem
        {
            get => _selectedHeldItem;
            set => this.RaiseAndSetIfChanged(ref _selectedHeldItem, value);
        }
        private ExplorersItemViewModel _selectedHeldItem;

        public ExplorersItemViewModel NewHeldtem
        {
            get => _newHeldItem;
            set => this.RaiseAndSetIfChanged(ref _newHeldItem, value);
        }
        private ExplorersItemViewModel _newHeldItem;

        public ReactiveCommand<Unit, Unit> AddHeldItemCommand { get; }

        public bool CanAddHeldItem => HeldItems.Count < 50;

        private void AddHeldItem()
        {
            HeldItems.Add(NewHeldtem.Clone());
            this.RaisePropertyChanged(nameof(CanAddHeldItem));
        }

        #endregion

        #region Sp Episode Held Items
        public ObservableCollection<ExplorersItemViewModel> SpEpisodeHeldItems { get; }

        public ExplorersItemViewModel SelectedSpEpisodeHeldItem
        {
            get => _selectedSpEpisodeHeldItem;
            set => this.RaiseAndSetIfChanged(ref _selectedSpEpisodeHeldItem, value);
        }
        private ExplorersItemViewModel _selectedSpEpisodeHeldItem;

        public ExplorersItemViewModel NewSpEpisodeHeldItem
        {
            get => _newSpEpisodeHeldItem;
            set => this.RaiseAndSetIfChanged(ref _newSpEpisodeHeldItem, value);
        }
        private ExplorersItemViewModel _newSpEpisodeHeldItem;

        public ReactiveCommand<Unit, Unit> AddSpEpisodeHeldItemCommand { get; }

        public bool CanAddSpEpisodeHeldItem => SpEpisodeHeldItems.Count < 50;
        
        private void AddSpEpisodeHeldItem()
        {
            SpEpisodeHeldItems.Add(NewSpEpisodeHeldItem.Clone());
            this.RaisePropertyChanged(nameof(CanAddSpEpisodeHeldItem));
        }
        #endregion

        #region Friend Rescue Held Items
        public ObservableCollection<ExplorersItemViewModel> FriendRescueHeldItems { get; }

        public ExplorersItemViewModel SelectedFriendRescueHeldItem
        {
            get => _selectedFriendRescueHeldItem;
            set => this.RaiseAndSetIfChanged(ref _selectedFriendRescueHeldItem, value);
        }
        private ExplorersItemViewModel _selectedFriendRescueHeldItem;

        public ExplorersItemViewModel NewFriendRescueHeldItem
        {
            get => _newFriendRescueHeldItem;
            set => this.RaiseAndSetIfChanged(ref _newFriendRescueHeldItem, value);
        }
        private ExplorersItemViewModel _newFriendRescueHeldItem;

        public ReactiveCommand<Unit, Unit> AddFriendRescueHeldItemCommand { get; }

        public bool CanAddFriendRescueHeldItem => FriendRescueHeldItems.Count < 50;

        private void AddFriendRescueHeldItem()
        {
            FriendRescueHeldItems.Add(NewFriendRescueHeldItem.Clone());
            this.RaisePropertyChanged(nameof(CanAddFriendRescueHeldItem));
        }
        #endregion

        public void Save()
        {
            Model.StoredItems.Clear();
            Model.StoredItems.AddRange(this.StoredItems.Select(item => item.Model));
            Model.HeldItems.Clear();
            Model.HeldItems.AddRange(this.HeldItems.Select(item => (SkyHeldItem)item.Model));
            Model.SpEpisodeHeldItems.Clear();
            Model.SpEpisodeHeldItems.AddRange(this.SpEpisodeHeldItems.Select(item => (SkyHeldItem)item.Model));
            Model.FriendRescueHeldItems.Clear();
            Model.FriendRescueHeldItems.AddRange(this.FriendRescueHeldItems.Select(item => (SkyHeldItem)item.Model));
        }
    }
}
