using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using JokesApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace JokesApp.Controllers;

public class JokeController : Controller
{
    private DatabaseContext _context = new DatabaseContext();

    private async Task<Tuple<int, List<ExtendedJoke>>> getJokeInfo(int id)
    {
        List<ExtendedJoke> all_jokes = new List<ExtendedJoke>();
        var conn = _context.Database.GetDbConnection();
        await conn.OpenAsync();
        try
        {
            using (var command = conn.CreateCommand())
            {
                string query = "select Joke.JokeId, Joke.JokeText, User.Login, r.Score " +
                               "from Joke " +
                               "left join User " +
                               "on Joke.Author = User.UserId " +
                               "left join (select JokeId, avg(Score) as Score from Rating group by JokeId) as r " +
                               "on Joke.JokeId = r.JokeId " +
                               "order by r.Score desc ";
                command.CommandText = query;
                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var score = reader.IsDBNull(3) ? 0.0 : reader.GetDouble(3);
                        var row = new ExtendedJoke
                        {
                            JokeId = reader.GetInt32(0),
                            JokeText = reader.GetString(1),
                            Author = reader.GetString(2),
                            Score = score
                        };

                        all_jokes.Add(row);
                    }
                }

                await reader.DisposeAsync();
            }
        }
        finally
        {
            await conn.CloseAsync();
        }

        var t = new Tuple<Int32, List<ExtendedJoke>>(id, all_jokes);
        return t;
    }
    
    // GET
    public async Task<IActionResult> Index(int id)
    {
        var t = await getJokeInfo(id);
        return View("Index", t);
    }

    public IActionResult Create(int id)
    {
        return View("Create", id);
    }

    public async Task<IActionResult> JokeCreated(string joke_text, int id)
    {
        var joke_id = _context.Joke.Count() + 1;

        _context.Add(new Joke
        {
            JokeId = joke_id,
            JokeText = joke_text,
            Author = id
        });
        await _context.SaveChangesAsync();
        
        var t = await getJokeInfo(id);
        return View("Index", t);
        
    }

    public async Task<IActionResult> RateJoke(int id, int jokeId, int score)
    {
        var rating_id = _context.Rating.Count() + 1;


        if (_context.Rating.Any(j => j.UserId.Equals(id) && j.JokeId.Equals(jokeId)))
        {
            var rating = _context.Rating.First(j => j.UserId.Equals(id) && j.JokeId.Equals(jokeId));
            rating.Score = score;
        }
        else
        {
            _context.Add(new Rating
            {
                RatingId = rating_id,
                JokeId = jokeId,
                UserId = id,
                Score = score
            });    
        }
        
        await _context.SaveChangesAsync();
        
        var t = await getJokeInfo(id);
        return View("Index", t);
    }
}