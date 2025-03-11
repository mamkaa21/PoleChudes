using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleChudes
{
    public class Word : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string Letter {  get; set; }

        private bool opened = false;
        public bool Opened
        {
            get => opened;
            set
            {
                opened = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Opened)));
            }
        }
    }
}
