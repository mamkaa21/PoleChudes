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

    Action<string, string, string> action;
    internal void SetStart(Action<string, string, string> action) 
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

        var game = new Game { ID = Guid.NewGuid().ToString(), P1 = a, P2 = b, Turn = "Ваш ход" };
        games.Add(game.ID, game);
        action(a,b,game.ID);
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

    //internal string MakeTurn(Turn turn) 
    //{
    //    string result = string.Empty;

    //}
}