namespace SpaceInvaders.Models;

public class User
{
    private string name;
    private int score;

    public User(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string Name
    {
        get { return name; }
    }
    public int Score
    {
        get { return score; }
    }
}
