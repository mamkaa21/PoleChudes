using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PoleChudes
{
    /// <summary>
    /// Логика взаимодействия для WinNick.xaml
    /// </summary>
    public partial class WinNick : Window
    {
        private readonly HubConnection _connection;

        public string Nick { get; set; }
        public string Message { get; set; }
        public WinNick(Microsoft.AspNetCore.SignalR.Client.HubConnection connection, string s)
        {
            InitializeComponent();
            Message = s;
            DataContext = this;
            _connection = connection;
        }
        private async void Send(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Nick))
            {
                MessageBox.Show("Ник не может быть пустым");
                return;
            }
            await _connection.SendAsync("Nickname", Nick);
            Close();
        }
    }
}
