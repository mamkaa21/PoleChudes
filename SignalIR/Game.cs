
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

public class Word
{
    public string Letter { get; set; }
    public bool Opened { get; set; } = false;
}