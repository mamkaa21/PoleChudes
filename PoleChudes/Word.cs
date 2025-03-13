using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoleChudes
{
    public class Game
    {
        public string ID { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P4 { get; set; }
        public string Turn { get; set; }
        public string Question { get; set; }
        public List<Word> Word { get; set; } = new();
    }
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
