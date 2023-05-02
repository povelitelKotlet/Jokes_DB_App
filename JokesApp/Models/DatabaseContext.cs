using JokesApp.Data;
using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace JokesApp.Models;

public class DatabaseContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Joke> Joke { get; set; }
    public DbSet<Rating> Rating { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=app.db");
    }
}