﻿using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private string opponent;
        public string Opponent
        {
            get => opponent;
            set
            {
                opponent = value;
                Signal();
            }
        }
        private bool myTurn;
        public bool MyTurn
        {
            get => myTurn;
            set
            {
                myTurn = value;
                Signal();
            }
        }
        string myChar = string.Empty;

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

        public string Question { get => question; set
            {
                question = value;
                Signal();
            }
        }

        public List<Word> Word { get => word; set => word = value; }
        public string Answer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CreateConnection();
            HubMethods();
            DataContext = this;
        }


        string gameId = string.Empty;
        private string question;
        private List<Word> word;

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
            connection.On<string, Game>("opponent", (s, id) =>
            {
                gameId = id.ID;
                Opponent = s;
                Question = id.Question;
                Word = id.Word;
            });
            connection.On<string>("maketurn", s =>
            {
                myChar = s;
                MyTurn = true;
            });
            connection.On<Turn>("opponent_turn", s =>
            {
                Dispatcher.Invoke(() =>
                {
                    //var but = FindName(s.Button) as Button;
                    //but.Content = s.Char;
                });
            });
            connection.On<string>("result", async s =>
            {
                if (s == "win")
                {
                    MessageBox.Show("Поздравляю с победой");
                }
                else
                {
                    MessageBox.Show($"Победил игрок { Nick }");
                }
                string nextgame = "no";
                if (MessageBox.Show("Еще раз?", " " , MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
                {
                    nextgame = "yes";
                    Dispatcher.Invoke(() =>
                    {
                        foreach (ListBox lb in listbox.Items)
                        {
                            lb.Items.Clear();
                        }
                    });
                    MyTurn = false;
                }
                await connection.SendAsync("NextGame", nextgame, Nick);
                if (nextgame == "no")
                {
                    await connection.StopAsync();
                    Dispatcher.Invoke(() =>
                    {
                        Close();
                    });
                }
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

        private async void MakeTurn(object sender, RoutedEventArgs e)
        {
            var lb = sender as ListBox;
            if (string.IsNullOrEmpty(Answer) || Answer.Length > 1)
                return;
            string test = Answer.ToUpper();
            await connection.SendAsync("MakeTurn",
                    new Turn
                    {
                        GameID = gameId,
                        Letter = test
                    });
            MyTurn = false;


            //if (lb.ItemTemplate.HasContent)
            //{
            //    foreach (var wordChar in Word)
            //    {
            //        if (!wordChar.Opened && wordChar.Letter == test)
            //        {

            //            //var anim = FindResource("Storyboard1") as Storyboard;
            //            //anim.Begin();

            //            Task.Delay(1000).ContinueWith(s =>
            //            {
            //                wordChar.Opened = true;
            //            });
            //        }
            //    }
            //}
        }
    }

   
}