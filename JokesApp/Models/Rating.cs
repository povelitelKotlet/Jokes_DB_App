namespace JokesApp.Models;

public class Rating
{
    public int RatingId { get; set; }
    public int UserId { get; set; }
    public int JokeId { get; set; }
    public int Score { get; set; }

    public Rating()
    {
        
    }
}