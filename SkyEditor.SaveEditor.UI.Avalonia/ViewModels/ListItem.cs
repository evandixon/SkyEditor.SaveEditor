using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkyEditor.SaveEditor.UI.Avalonia.ViewModels
{
    public class ListItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
                }
            }
        }
        private string _displayName;


        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                }
            }
        }
        private int _value;

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
