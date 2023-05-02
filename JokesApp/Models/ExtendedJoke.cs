namespace JokesApp.Models;

public class ExtendedJoke
{
    public int JokeId { get; set; }
    public string JokeText { get; set; }
    public string Author { get; set; }
    public double Score { get; set; }

    public ExtendedJoke()
    {
        
    }
}