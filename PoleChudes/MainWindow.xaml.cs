using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;

namespace PoleChudes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        HubConnection connection;
        private string nick;
        public string Nick
        {
            get => nick;
            set
            {
                nick = value;
                Signal();
            }
        }
        public List<Word> Word {  get; set; }
        public string Answer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CreateConnection();
            HubMethods();
            DataContext = this;

            Word = new List<Word>()
            {

            };
        }
        private void HubMethods()
        {
            connection.On<string>("hello", s =>
            {
                Dispatcher.Invoke(() =>
                {
                    var win = new WinNick(connection, s);
                    win.ShowDialog();
                    Nick = win.Nick;
                });
            });
        }

        private void CreateConnection()
        {
            var win = new WinOptions();
            string address = win.Address;
           connection = new HubConnectionBuilder().WithUrl(address + "/polechudes").Build();
            connection.StartAsync();
            Unloaded += async (s, e) => await connection.StopAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}