using ReactiveUI;
using SkyEditor.SaveEditor.MysteryDungeon.Explorers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels.Explorers.Sky
{
    public class SkySaveViewModel : SaveFileViewModel<SkySave>
    {
        public SkySaveViewModel(SkySave model) : base(model)
        {
            this.GeneralViewModel = new SkyGeneralViewModel(model);
            this.HistoryViewModel = new SkyHistoryViewModel(model);
        }

        public SkyGeneralViewModel GeneralViewModel { get; }
        public SkyHistoryViewModel HistoryViewModel { get; }
    }
}
