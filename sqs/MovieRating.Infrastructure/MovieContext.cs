using Microsoft.EntityFrameworkCore;
using MovieRating.Core.Models;

namespace MovieRating.Infrastructure;

public class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public MovieContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasMany(movie => movie.Ratings)
            .WithOne(rating => rating.Movie)
            .OnDelete(DeleteBehavior.Cascade);
    }
}