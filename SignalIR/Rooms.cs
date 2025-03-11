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

    internal void StartNewGame()
    {

    }
}