using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels
{
    public class SaveFileViewModel<TSaveFile> : ViewModelBase where TSaveFile : ISaveFile
    {
        public SaveFileViewModel(TSaveFile model)
        {
            this.Model = model;
        }

        protected TSaveFile Model { get; set; }

        public string FileName
        {
            get => Path.GetFileName(Model.Filename);
        }
    }
}
