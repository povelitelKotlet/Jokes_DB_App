namespace JokesApp.Models;

public class Joke
{
    public int JokeId { get; set; }
    public string JokeText { get; set; }
    public int Author { get; set; }

    public Joke()
    {
        
    }
    
}