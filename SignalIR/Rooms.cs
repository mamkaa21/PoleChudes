internal class Rooms
{
    string last = string.Empty;

    internal void AddNewClient(string nick)
    {
        if (string.IsNullOrEmpty(last))
            last = nick;
        else
        {
            StartNewGame(last, nick);
            last = string.Empty;
        }
    }

    Action<string, string, Game> action;
    internal void SetStart(Action<string, string, Game> action) 
    {
        this.action = action;
    }

    Dictionary<string, Game> games = new();
    private void StartNewGame(string a, string b)
    {
        Random random = new Random();
        if (random.Next(100) > 50)
        {
            string z = b;
            b = a;
            a = z;
        }
       
        // тут надо придумать вопрос и ответ
        var game = new Game { ID = Guid.NewGuid().ToString(), P1 = a, P2 = b, Turn = "Ваш ход", Question = "Почему небо синее?", 
            Word = "потому что".Select(s=>new Word { Letter = s.ToString()}).ToList()  };
        games.Add(game.ID, game);
        action(a,b,game);
    }

    internal string GetNextPlayer(Turn turn)
    {
        string result = string.Empty;
        if (games[turn.GameID].Turn == "Ваш ход")
        {
            games[turn.GameID].Turn = "Ход другого игрока";
            result = games[turn.GameID].P2;
        }
        else 
        {
            games[turn.GameID].Turn = "Ваш ход";
            result = games[turn.GameID].P2;
        }
        return result;
    }

    internal bool CheckLetter(Turn turn)
    {
        bool result = false;
        turn.Letter.ToString();
        foreach (var w in games[turn.GameID].Word)
        {
            if (w.Letter == turn.Letter) 
            {
                // если есть совпадение буквы - мы ее открываем
                // result = true
                w.Opened = true;
                result = true;
            }    
        }
        return result;
    }

    internal string MakeTurn(Turn turn)
    {
        string result = string.Empty;
        return result;
    }
}